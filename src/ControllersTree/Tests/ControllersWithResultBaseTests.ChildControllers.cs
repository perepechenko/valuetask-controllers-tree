using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace UnitTests.Controllers
{
    public partial class ControllersWithResultBaseTests
    {
        [Test]
        public async Task ControllerWithResultBase_Complete_Parent()
        {
            // Arrange
            var testGuid = TestControllersUtils.GenerateTestGuid();
            var childControllersGuids = Enumerable.Range(0, 5)
                                                  .Select( number => TestControllersUtils.GenerateTestGuid())
                                                  .ToList();

            var args = new TestChildControllerArgs(testGuid, childControllersGuids);

            var startTriggeredCallers = new List<string>();
            var flowTriggeredCallers = new List<string>();
            var stopTriggeredCallers = new List<string>();
            var disposeTriggeredCallers = new List<string>();

            _testChildControllersActionModel.StartTriggered += caller => startTriggeredCallers.Add(caller);
            _testChildControllersActionModel.FlowTriggered += caller => flowTriggeredCallers.Add(caller);
            _testChildControllersActionModel.StopTriggered += caller => stopTriggeredCallers.Add(caller);
            _testChildControllersActionModel.DisposeTriggered += caller => disposeTriggeredCallers.Add(caller);

            var result = await TestRootControllerLauncher
                             .LaunchAsync<ActionModelTestControllerWithResultAndChildControllers, TestChildControllerArgs, TestEmptyControllerResult>(
                                 args, _controllerFactory, CancellationToken);

            // Assert
            AssertTriggerCallers(startTriggeredCallers, childControllersGuids, "Expected callers doesn't contain Start caller id");
            AssertTriggerCallers(flowTriggeredCallers, childControllersGuids, "Expected callers doesn't contain Flow caller id");
            AssertTriggerCallers(stopTriggeredCallers, childControllersGuids, "Expected callers doesn't contain Stop caller id");
            AssertTriggerCallers(disposeTriggeredCallers, childControllersGuids, "Expected callers doesn't contain Dispose caller id");
            Assert.IsNotNull(result, "Result shouldn't be null");
            var resultCorrect = string.Equals(args.InputString, result.ResultString);
            Assert.True(resultCorrect, "Result is incorrect");
        }

        private void AssertTriggerCallers(
            List<string> callers,
            List<string> expectedCallers,
            string errorMessage)
        {
            Assert.IsNotEmpty(callers, "Controllers methods callers collection shouldn't be empty");
            Assert.AreEqual(expectedCallers.Count, callers.Count, "Controllers methods callers collection size is incorrect");
            foreach (var caller in callers)
            {
                Assert.Contains(caller, expectedCallers, errorMessage, errorMessage);
            }
        }
    }
}
