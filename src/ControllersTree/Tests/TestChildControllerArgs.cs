using System.Collections.Generic;

namespace UnitTests.Controllers
{
    internal class TestChildControllerArgs : TestControllerArgs
    {
        public readonly IReadOnlyCollection<string> ChildControllersGuids;

        public TestChildControllerArgs(
            string inputString,
            IReadOnlyCollection<string> childControllersGuids)
            : base(inputString)
        {
            ChildControllersGuids = childControllersGuids;
        }
    }
}
