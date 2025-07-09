using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CustomTestRunner.Discovery
{
    public class TestDiscoverer
    {
        /// <summary>
        /// Uses reflection to discover all instantiable test classes in an assembly.
        /// </summary>
        /// <param name="assembly">Assembly to scan</param>
        /// <returns>Enumerable of test class types</returns>
        public IEnumerable<Type> DiscoverTestClasses(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract);
        }
    }
}
