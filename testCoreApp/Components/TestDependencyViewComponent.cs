using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using testCoreApp.Models;

namespace testCoreApp.Components
{
    [ViewComponent(Name = "TestDependencyNamedComponent")]
    public class TestDependencyViewComponent : ViewComponent
    {
        private TestDependencyEntity testEntity;

        public TestDependencyViewComponent(TestDependencyEntity testEnt)
        {
            testEntity = testEnt;            
        }

        //public IViewComponentResult Invoke()
        //{
        //    return new HtmlContentViewComponentResult(new HtmlString($"<b>from viewComponent {testEntity.EntityGuid}</b>"));
        //    //return View();
        //}

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://rutracker.org/");
                return Content(response.IsSuccessStatusCode ? response.Content.Headers.ContentLength.Value.ToString() : "none");
            }
            catch
            {
                return Content("none");
            }
        }
    }
}
