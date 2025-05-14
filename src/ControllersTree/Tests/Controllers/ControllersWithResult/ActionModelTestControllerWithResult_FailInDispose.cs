using Playtika.Controllers;

namespace UnitTests.Controllers
{
    internal class ActionModelTestControllerWithResult_FailInDispose : ActionModelTestControllerWithResult
    {
        internal ActionModelTestControllerWithResult_FailInDispose(
            IControllerFactory controllerFactory,
            TestControllersActionModel testControllersControllersModel)
            : base(controllerFactory, testControllersControllersModel)
        {
        }

        protected override void OnStart()
        {
            AddDisposable(new DisposableToken(() =>
            {
                Fail(new TestControllersException(TestControllersMethodsNamesConsts.DisposeMethodName));
            }));
            TestControllersActionModel.Args = Args.InputString;
            TestControllersActionModel.TriggerStart();

            Complete(new TestEmptyControllerResult(Args.InputString));
        }
    }
}
