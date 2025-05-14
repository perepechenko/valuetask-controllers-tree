using Playtika.Controllers;

namespace UnitTests.Controllers
{
    internal class ActionModelTestControllerWithResult_FailOnStop : ActionModelTestControllerWithResult
    {
        internal ActionModelTestControllerWithResult_FailOnStop(
            IControllerFactory controllerFactory,
            TestControllersActionModel testControllersControllersModel)
            : base(controllerFactory, testControllersControllersModel)
        {
        }

        protected override void OnStart()
        {
            base.OnStart();
            Complete(new TestEmptyControllerResult(Args.InputString));
        }

        protected override void OnStop()
        {
            base.OnStop();
            Fail(new TestControllersException(TestControllersMethodsNamesConsts.FailMethodName));
        }
    }
}
