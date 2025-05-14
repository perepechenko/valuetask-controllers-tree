using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace UnitTests.Controllers
{
    public partial class ControllersWithResultBaseTests
    {
        [Test]
        public async Task ControllerWithResultBase_CompletedFlowAsync_OnFlowAsync()
        {
            // Arrange
            var startTriggered = false;
            var flowTriggered = false;
            var stopTriggered = false;
            var disposeTriggered = false;

            var testGuid = TestControllersUtils.GenerateTestGuid();
            var args = new TestControllerArgs(testGuid);
            _testControllersActionModel.StartTriggered += () => startTriggered = true;
            _testControllersActionModel.FlowTriggered += () => flowTriggered = true;
            _testControllersActionModel.StopTriggered += () => stopTriggered = true;
            _testControllersActionModel.DisposeTriggered += () => disposeTriggered = true;

            // Act
            var result = await TestRootControllerLauncher.LaunchAsync
                             <ActionModelTestControllerWithResult_CompleteOnFlowAsync, TestControllerArgs, TestEmptyControllerResult>(
                                 args, _controllerFactory, CancellationToken);

            // Assert
            Assert.True(startTriggered, "OnStart is not triggered");
            Assert.True(flowTriggered, "OnFlowAsync is not triggered");
            Assert.True(stopTriggered, "OnStop is not triggered");
            Assert.True(disposeTriggered, "Dispose is not triggered");
            Assert.IsNotNull(result, "Result shouldn't be null");
            var resultCorrect = string.Equals(args.InputString, result.ResultString);
            Assert.True(resultCorrect, "Result is incorrect");
        }

        [Test]
        public async Task ControllerWithResultBase_Exception_OnFlowAsync()
        {
            // Arrange
            var startTriggered = false;
            var stopTriggered = false;
            var disposeTriggered = false;

            var testGuid = TestControllersUtils.GenerateTestGuid();
            var args = new TestControllerArgs(testGuid);
            _testControllersActionModel.StartTriggered += () => startTriggered = true;
            _testControllersActionModel.FlowTriggered += () => throw new TestControllersException(TestControllersMethodsNamesConsts.OnFlowAsyncMethodName);
            _testControllersActionModel.StopTriggered += () => stopTriggered = true;
            _testControllersActionModel.DisposeTriggered += () => disposeTriggered = true;
            var exceptionThrown = false;

            // Act
            try
            {
                _ = await TestRootControllerLauncher
                        .LaunchAsync<ActionModelTestControllerWithResult_CompleteOnFlowAsync, TestControllerArgs, TestEmptyControllerResult>(
                            args, _controllerFactory, CancellationToken);
            }
            catch (TestControllersException)
            {
                exceptionThrown = true;
            }

            // Assert
            Assert.True(startTriggered, "OnStart is not triggered");
            Assert.True(stopTriggered, "OnStop is not triggered");
            Assert.True(disposeTriggered, "Dispose is not triggered");
            Assert.IsTrue(exceptionThrown, "TestControllersException expected");
        }

        [Test]
        public async Task ControllerWithResultBase_CompletedAfterFlowAsync_OnFlowAsync()
        {
            // Arrange
            var startTriggered = false;
            var flowTriggered = false;
            var stopTriggered = false;
            var disposeTriggered = false;

            var testGuid = TestControllersUtils.GenerateTestGuid();
            var args = new TestControllerArgs(testGuid);
            _testControllersActionModel.StartTriggered += () => startTriggered = true;
            _testControllersActionModel.FlowTriggered += () => flowTriggered = true;
            _testControllersActionModel.StopTriggered += () => stopTriggered = true;
            _testControllersActionModel.DisposeTriggered += () => disposeTriggered = true;

            // Act
            var result = await TestRootControllerLauncher.LaunchAsync
                             <ActionModelTestControllerWithResult_CompleteAfterOnFlowAsync, TestControllerArgs, TestEmptyControllerResult>(
                                 args, _controllerFactory, CancellationToken);

            // Assert
            Assert.True(startTriggered, "OnStart is not triggered");
            Assert.True(flowTriggered, "OnFlowAsync is not triggered");
            Assert.True(stopTriggered, "OnStop is not triggered");
            Assert.True(disposeTriggered, "Dispose is not triggered");
            Assert.IsNotNull(result, "Result shouldn't be null");
            var resultCorrect = string.Equals(args.InputString, result.ResultString);
            Assert.True(resultCorrect, "Result is incorrect");
        }

        [Test]
        public async Task ControllerWithResultBase_Fail_OnFlowAsync()
        {
            // Arrange
            var startTriggered = false;
            var flowTriggered = false;
            var stopTriggered = false;
            var disposeTriggered = false;

            var testGuid = TestControllersUtils.GenerateTestGuid();
            var args = new TestControllerArgs(testGuid);
            _testControllersActionModel.StartTriggered += () => startTriggered = true;
            _testControllersActionModel.FlowTriggered += () => flowTriggered = true;
            _testControllersActionModel.StopTriggered += () => stopTriggered = true;
            _testControllersActionModel.DisposeTriggered += () => disposeTriggered = true;
            var exceptionThrown = false;

            // Act
            try
            {
                _ = await TestRootControllerLauncher
                        .LaunchAsync<ActionModelTestControllerWithResult_FailOnFlowAsync, TestControllerArgs, TestEmptyControllerResult>(
                            args, _controllerFactory, CancellationToken);
            }
            catch (TestControllersException)
            {
                exceptionThrown = true;
            }

            // Assert
            Assert.True(startTriggered, "OnStart is not triggered");
            Assert.False(flowTriggered, "OnFlowAsync has been triggered");
            Assert.True(stopTriggered, "OnStop is not triggered");
            Assert.True(disposeTriggered, "Dispose is not triggered");
            Assert.IsTrue(exceptionThrown, "TestControllersException expected");
        }

        [Test]
        public async Task ControllerWithResultBase_CancellationToken_OnFlowAsync()
        {
            // Arrange
            var startTriggered = false;
            var stopTriggered = false;
            var disposeTriggered = false;

            var testGuid = TestControllersUtils.GenerateTestGuid();
            var args = new TestControllerArgs(testGuid);

            _testControllersActionModel.StartTriggered += () => startTriggered = true;
            _testControllersActionModel.FlowTriggered += () => _cancellationTokenSource.Cancel();
            _testControllersActionModel.StopTriggered += () => stopTriggered = true;
            _testControllersActionModel.DisposeTriggered += () => disposeTriggered = true;
            var exceptionThrown = false;

            //Act
            try
            {
                _ = await TestRootControllerLauncher
                        .LaunchAsync<ActionModelTestControllerWithResult_CompleteOnFlowAsync, TestControllerArgs, TestEmptyControllerResult>(
                            args, _controllerFactory, CancellationToken);
            }
            catch (OperationCanceledException)
            {
                exceptionThrown = true;
            }

            Assert.True(startTriggered, "OnStart is not triggered");
            Assert.True(stopTriggered, "OnStop is not triggered");
            Assert.True(disposeTriggered, "Dispose is not triggered");
            Assert.IsTrue(exceptionThrown, "TestControllersException expected");
            Assert.True(CancellationToken.IsCancellationRequested, "Cancellation token should be cancelled");
        }
    }
}
