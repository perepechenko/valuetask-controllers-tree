using System.Threading;
using Playtika.Controllers;
using Cysharp.Threading.Tasks;
using Playtika.Controllers.Substitute;

namespace UnitTests.Controllers
{
    public static class TestRootControllerLauncher
    {
        public static TestRootController Launch(IControllerFactory controllerFactory,
                                                CancellationToken cancellationToken)
        {
            var testRootController = new TestRootController(controllerFactory);
            testRootController.LaunchTree(cancellationToken);

            return testRootController;
        }

        public static void Launch<T, TArg>(
            TArg arg,
            IControllerFactory controllerFactory,
            CancellationToken cancellationToken)
            where T : class, IController, IController<TArg>
        {
            var testRootController = new TestRootController(controllerFactory);
            testRootController.LaunchTree(cancellationToken);

            testRootController.Execute<T, TArg>(arg);
        }

        public static UniTask LaunchAsync<T>(
            IControllerFactory controllerFactory,
            CancellationToken cancellationToken)
            where T : class, IControllerWithResult<EmptyControllerResult>, IController<EmptyControllerArg>
        {
            var testRootController = new TestRootController(controllerFactory);
            testRootController.LaunchTree(cancellationToken);

            return testRootController.ExecuteAndWaitResultAsync<T>(cancellationToken);
        }

        public static void Launch<T, TArg>(
            IControllerFactory controllerFactory,
            TArg arg,
            CancellationToken cancellationToken)
            where T : class, IController, IController<TArg>
        {
            var testRootController = new TestRootController(controllerFactory);
            testRootController.LaunchTree(cancellationToken);

            testRootController.Execute<T, TArg>(arg);
        }

        public static UniTask LaunchAsync<T, TArg>(
            TArg arg,
            IControllerFactory controllerFactory,
            CancellationToken cancellationToken)
            where T : class, IControllerWithResult<EmptyControllerResult>, IController<TArg>
        {
            var testRootController = new TestRootController(controllerFactory);
            testRootController.LaunchTree(cancellationToken);

            return testRootController.ExecuteAndWaitResultAsync<T, TArg>(arg, cancellationToken);
        }

        public static UniTask<TResult> LaunchAsync<T, TArg, TResult>(
            TArg arg,
            IControllerFactory controllerFactory,
            CancellationToken cancellationToken)
            where T : class, IControllerWithResult<TResult>, IController<TArg>
        {
            var testRootController = new TestRootController(controllerFactory);
            testRootController.LaunchTree(cancellationToken);

            return testRootController.ExecuteAndWaitResultAsync<T, TArg, TResult>(arg, cancellationToken);
        }
    }
}