using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Playtika.Controllers.Substitute.Tests
{
    [TestFixture]
    public class TestRootControllerWithSubstituteFactoryTests
    {
        private interface ITestInstanceController : IController
        {
        }
        
        private class TestInstanceController : ControllerBase, ITestInstanceController
        {
            public bool StartWasCalled;

            public TestInstanceController()
                : base(null)
            {
            }

            protected override void OnStart()
            {
                base.OnStart();
                StartWasCalled = true;
            }
        }

        [Test]
        public void ExecuteByInterface_NotThrowException()
        {
            using var cts = new CancellationTokenSource();
            var factory = new SubstituteControllerFactory();
            var rootController = new TestRootController(factory);
            rootController.LaunchTree(cts.Token);

            rootController.Execute<TypesForTest.ITestController>();
        }

        [Test]
        public void ExecuteByClass_NotThrowException()
        {
            using var cts = new CancellationTokenSource();
            var factory = new SubstituteControllerFactory();
            var rootController = new TestRootController(factory);
            rootController.LaunchTree(cts.Token);

            rootController.Execute<TypesForTest.TestController>();
        }

        [Test]
        public async Task ExecuteAndWaitResultByInterface_NoThrowException()
        {
            using var cts = new CancellationTokenSource();
            var factory = new SubstituteControllerFactory();
            var rootController = new TestRootController(factory);
            rootController.LaunchTree(cts.Token);

            await rootController.ExecuteAndWaitResultAsync<TypesForTest.ITestControllerWithResult>(cts.Token);
        }

        [Test]
        public async Task ExecuteAndWaitResultByClass_NoThrowException()
        {
            using var cts = new CancellationTokenSource();
            var factory = new SubstituteControllerFactory();
            var rootController = new TestRootController(factory);
            rootController.LaunchTree(cts.Token);

            await rootController.ExecuteAndWaitResultAsync<TypesForTest.TestControllerWithResult>(cts.Token);
        }

        [Test]
        public async Task ExecuteAndWaitResultGenericWithArgGenericByInterface_DoNotThrowException()
        {
            using var cts = new CancellationTokenSource();
            var factory = new SubstituteControllerFactory();
            var rootController = new TestRootController(factory);
            rootController.LaunchTree(cts.Token);
            await rootController.ExecuteAndWaitResultAsync<TypesForTest.ITestControllerWithResultGenericWithArgsGeneric<string, float>, string, float>(
                string.Empty, cts.Token);
        }

        [Test]
        public async Task CreateAndWaitResultGenericWithArgGenericByClass_DoNotThrowException()
        {
            using var cts = new CancellationTokenSource();
            var factory = new SubstituteControllerFactory();
            var rootController = new TestRootController(factory);
            rootController.LaunchTree(cts.Token);
            await rootController.ExecuteAndWaitResultAsync<TypesForTest.TestControllerWithResultGenericWithArgsGeneric<string, float>, string, float>(
                string.Empty, cts.Token);
        }

        [Test]
        public void AddInstance_InstanceWasStarted()
        {
            using var cts = new CancellationTokenSource();

            var controllerInstance = new TestInstanceController();

            var factory = new SubstituteControllerFactory()
                .AddInstance(controllerInstance);

            var rootController = new TestRootController(factory);
            rootController.LaunchTree(cts.Token);

            rootController.Execute<TestInstanceController>();

            Assert.That(controllerInstance.StartWasCalled, Is.True);
        }
        
        [Test]
        public void AddInstanceAsInterface_InstanceWasStarted()
        {
            using var cts = new CancellationTokenSource();

            var controllerInstance = new TestInstanceController();

            var factory = new SubstituteControllerFactory()
                .AddInstance<ITestInstanceController>(controllerInstance);

            var rootController = new TestRootController(factory);
            rootController.LaunchTree(cts.Token);

            rootController.Execute<ITestInstanceController>();

            Assert.That(controllerInstance.StartWasCalled, Is.True);
        }

        [Test]
        public async Task AddResultFor_ResultIsCorrect()
        {
            using var cts = new CancellationTokenSource();
            var factory = new SubstituteControllerFactory()
                .AddResultFor<TypesForTest.ITestControllerWithResultGeneric<float>>(45);

            var rootController = new TestRootController(factory);
            rootController.LaunchTree(cts.Token);
            var result = await rootController.ExecuteAndWaitResultAsync<TypesForTest.ITestControllerWithResultGeneric<float>, float>(cts.Token);

            Assert.That(result, Is.EqualTo(45));
        }

        [Test]
        public Task ExecuteOneController_ReceivedReturn1()
        {
            using var cts = new CancellationTokenSource();
            var factory = new SubstituteControllerFactory();

            var rootController = new TestRootController(factory);
            rootController.LaunchTree(cts.Token);
            rootController.Execute<TypesForTest.TestController>();

            Assert.That(factory.Received<TypesForTest.TestController>(), Is.EqualTo(1));
            return Task.CompletedTask;
        }
        
        [Test]
        public Task ExecuteControllerTwice_ReceivedReturn2()
        {
            using var cts = new CancellationTokenSource();
            var factory = new SubstituteControllerFactory();

            var rootController = new TestRootController(factory);
            rootController.LaunchTree(cts.Token);
            rootController.Execute<TypesForTest.TestController>();
            rootController.Execute<TypesForTest.TestController>();

            Assert.That(factory.Received<TypesForTest.TestController>(), Is.EqualTo(2));
            return Task.CompletedTask;
        }
        
        [Test]
        public Task ExecuteTwoController_Received1PerEachController()
        {
            using var cts = new CancellationTokenSource();
            var factory = new SubstituteControllerFactory();

            var rootController = new TestRootController(factory);
            rootController.LaunchTree(cts.Token);
            rootController.Execute<TypesForTest.TestController>();
            rootController.ExecuteAndWaitResultAsync<TypesForTest.TestControllerWithResult>(cts.Token);

            Assert.That(factory.Received<TypesForTest.TestController>(), Is.EqualTo(1));
            Assert.That(factory.Received<TypesForTest.TestControllerWithResult>(), Is.EqualTo(1));
            return Task.CompletedTask;
        }
    }
}