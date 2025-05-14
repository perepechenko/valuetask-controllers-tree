using System;

namespace UnitTests.Controllers
{
    public class TestControllersException : Exception
    {
        public readonly string ThrowerCallerName;

        public TestControllersException(string throwerCallerName)
        {
            ThrowerCallerName = throwerCallerName;
        }
    }
}
