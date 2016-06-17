using AutoMapper;
using EvaluatingTool.BLL.DTO;
using EvaluatingTool.BLL.Infrastructure;
using EvaluatingTool.BLL.Interfaces;
using EvaluatingTool.WEB.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EvaluatingTool.WEB.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        ITestService testService;
        private static IEnumerable<PageViewModel> pages;
        public HomeController(ITestService service)
        {
            testService = service;
        }

        public ActionResult TestResult(int? id)
        {
            try
            {
                MapperConfiguration config = new MapperConfiguration(map => map.CreateMap<PageDTO, PageViewModel>());
                var mapper = config.CreateMapper();
                pages = mapper.Map<IEnumerable<PageDTO>, IEnumerable<PageViewModel>>(testService.GetTestPages(id));

                TestDTO test = testService.GetTest(id);
                ViewBag.HostURL = test.HostURL;
                ViewBag.Date = test.TestDate;

                ProgressHub.SendMessage("receiving data", 99);
                return View(pages);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }           
        }

        public JsonResult Timeline(int? id)
        {
            int i = 1;
            var pagesjson = pages.Select(p => new ArrayList { i++.ToString(), p.URL, 0, p.ResponseTime });
            return Json(pagesjson, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BarTimeline(int? id)
        {
            var pagesjson = pages.Select(p => new ArrayList { p.URL, p.ResponseTime });
            return Json(pagesjson, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(SiteMapModel siteMap)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProgressHub.SendMessage("", 1);
                    await testService.MakeTestAsync(siteMap.Host);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View(siteMap);
                }

                return RedirectToAction("TestResult");
            }
            return View(siteMap);
        }

        public ActionResult History()
        {
            MapperConfiguration config = new MapperConfiguration(map => map.CreateMap<TestDTO, TestViewModel>());
            var mapper = config.CreateMapper();
            var tests = mapper.Map<IEnumerable<TestDTO>, IEnumerable<TestViewModel>>(testService.GetAllTests());
            return View(tests);
        }
       
        protected override void Dispose(bool disposing)
        {
            testService.Dispose();
            base.Dispose(disposing);
        }
    }
}