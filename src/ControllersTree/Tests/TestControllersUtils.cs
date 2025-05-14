using System;

namespace UnitTests.Controllers
{
    public static class TestControllersUtils
    {
        public static string GenerateTestGuid()
        {
            return Guid.NewGuid()
                       .ToString();
        }
    }
}