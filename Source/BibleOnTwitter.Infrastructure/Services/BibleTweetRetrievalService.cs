using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibleOnTwitter.Infrastructure.DataAccess;
using BibleOnTwitter.Infrastructure.Model.Data;
using NHibernate.Linq;
using LinqToTwitter;
using NHibernate;

namespace BibleOnTwitter.Infrastructure.Services
{
    public class BibleTweetRetrievalService : IBibleTweetRetrievalService
    {
        private readonly ITwitterService _TwitterService;
        private readonly INHibernateSessionProvider _SessionProvider;

        public BibleTweetRetrievalService(ITwitterService TwitterService, INHibernateSessionProvider SessionProvider)
        {
            _TwitterService = TwitterService;
            _SessionProvider = SessionProvider;
        }

        public void GetAndSaveNewTweets()
        {
            _SessionProvider.Transactional(Session =>
                {
                    var LastTweetTime =
                        (from tweet in Session.Query<Tweet>()
                         orderby tweet.PublishedTime descending
                         select tweet.PublishedTime).FirstOrDefault();

                    var Entries = _TwitterService.GetBibleTweets(LastTweetTime)
                        .ToDictionary(e => long.Parse(e.ID.Split(':').Last()));

                    Session.QueryOver<Tweet>()
                        .WhereRestrictionOn(t => t.Id).IsIn(Entries.Keys.ToArray())
                        .Select(t => t.Id)
                        .List<long>()
                        .ForEach(id => Entries.Remove(id));

                    var AuthorInfos = Entries.Values.Select(e => new
                    {
                        Name = Author.GetProfileNameFromUrl(e.Author.URI),
                        Url = e.Author.URI,
                        ImageUrl = e.Image
                    });
                    var Authors = Session.QueryOver<Author>()
                        .WhereRestrictionOn(x => x.Name).IsIn(AuthorInfos.Select(ai => ai.Name).ToArray())
                        .List()
                        .ToDictionary(a => a.Name);

                    var TweetEntries = Entries.Values.Select(e => new { 
                        Tweet = new Tweet
                        {
                            Id = long.Parse(e.ID.Split(':').Last()),
                            PublishedTime = e.Published,
                            Content = e.Title
                        },
                        Entry = e });

                    var ReferenceNames = TweetEntries.SelectMany(t => t.Tweet.ParseReferenceNames());
                    var Referneces = Session.QueryOver<Reference>()
                        .WhereRestrictionOn(x => x.Name).IsIn(ReferenceNames.ToArray())
                        .List()
                        .ToDictionary(r => r.Name);

                    foreach (var TweetEntry in TweetEntries)
                    {
                        var AuthorName = Author.GetProfileNameFromUrl(TweetEntry.Entry.Author.URI);

                        TweetEntry.Tweet.BibleVerseReferences = TweetEntry.Tweet.ParseBibleVerses().ToList();
                        TweetEntry.Tweet.References = TweetEntry.Tweet.ParseReferenceNames()
                            .Select(rn => Referneces.AssuredGet(rn, () => new Reference
                                {
                                    Name = rn,
                                    Type = rn.StartsWith("#") ? ReferenceType.HashTag : ReferenceType.Person
                                })).ToList();
                        TweetEntry.Tweet.Author = Authors.AssuredGet(AuthorName, () => new Author
                            {
                                Name = AuthorName,
                                ProfileName = TweetEntry.Entry.Author.Name,
                                ProfileUrl = TweetEntry.Entry.Author.URI,
                                ImageUrl = TweetEntry.Entry.Image
                            });

                        Session.SaveOrUpdate(TweetEntry.Tweet);
                    }
                });
        }
    }
}
