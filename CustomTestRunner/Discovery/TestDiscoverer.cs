using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CustomTestRunner.Discovery
{
    public class TestDiscoverer
    {
        public IEnumerable<Type> DiscoverTestClasses(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract);
        }
    }
}
