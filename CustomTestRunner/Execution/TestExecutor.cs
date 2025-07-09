using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using CustomTestRunner.Models;
using CustomTestRunner.Attributes;


namespace CustomTestRunner.Execution
{
    public class TestExecutor
    {
        public List<TestResult> ExecuteTests(IEnumerable<Type> testClasses)
        {
            var results = new List<TestResult>();

            foreach (var type in testClasses)
            {
                object instance;
                try { instance = Activator.CreateInstance(type); }
                catch { continue; }

                var setupMethods = type.GetMethods().Where(m => m.GetCustomAttribute(typeof(SetupAttribute)) != null && m.GetParameters().Length == 0);
                var teardownMethods = type.GetMethods().Where(m => m.GetCustomAttribute(typeof(TeardownAttribute)) != null && m.GetParameters().Length == 0);

                foreach (var method in type.GetMethods().Where(m => m.GetCustomAttribute(typeof(MyTestAttribute)) != null && m.GetParameters().Length == 0))
                {
                    var result = new TestResult { TestName = $"{type.Name}.{method.Name}" };
                    var stopwatch = Stopwatch.StartNew();

                    try
                    {
                        foreach (var setup in setupMethods)
                            setup.Invoke(instance, null);
                        // אתחול פרמטרים של המתודה אם קיימים
                        var parameters = method.GetParameters();
                        var parameterValues = parameters.Select(p => GetDefaultValue(p.ParameterType)).ToArray();

                        method.Invoke(instance, null);

                        foreach (var teardown in teardownMethods)
                            teardown.Invoke(instance, null);

                        result.Passed = true;
                    }
                    catch (Exception ex)
                    {
                        var inner = ex.InnerException ?? ex;
                        result.Passed = false;
                        result.ErrorMessage = inner.Message;
                        result.ExceptionType = inner.GetType().Name;
                    }

                    stopwatch.Stop();
                    result.DurationMilliseconds = stopwatch.ElapsedMilliseconds;

                    results.Add(result);
                }
            }

            return results;
        }
        private object GetDefaultValue(Type type)
        {
            if (type == typeof(int)) return 0;
            if (type == typeof(string)) return string.Empty;
            if (type == typeof(bool)) return false;
            if (type == typeof(double)) return 0.0;
            if (type == typeof(float)) return 0f;
            if (type == typeof(DateTime)) return DateTime.MinValue;
            if (type.IsEnum) return Enum.GetValues(type).GetValue(0);
            if (type.IsValueType) return Activator.CreateInstance(type);
            return null;
        }

    }
}

