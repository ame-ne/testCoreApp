using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testCoreApp.Models
{
    public class TestDependencyEntity
    {
        public TestDependencyEntity()
        {
            EntityGuid = Guid.NewGuid();
        }

        public Guid EntityGuid { get; }
    }
}
