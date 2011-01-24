using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BibleOnTwitter.Infrastructure.Services;

namespace BibleOnTwitter.Web.Controllers
{
    public class IndexController : Controller
    {
        private readonly IIndexService _IndexService;

        public IndexController(IIndexService IndexService)
        {
            _IndexService = IndexService;
        }

        public ActionResult Index()
        {
            var Model = _IndexService.GetIndexView();
            return View(Model);
        }
    }
}
