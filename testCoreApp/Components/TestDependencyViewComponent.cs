using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IViewComponentResult Invoke()
        {
            return Content($"from viewComponent {testEntity.EntityGuid}");
        }
    }
}
