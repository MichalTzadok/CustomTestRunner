using System;

namespace CustomTestRunner.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TeardownAttribute : Attribute { }
}