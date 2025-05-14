using Playtika.Controllers;

namespace UnitTests.Controllers
{
    internal class ActionModelTestControllerWithResult_FailOnStart : ActionModelTestControllerWithResult
    {
        internal ActionModelTestControllerWithResult_FailOnStart(
            IControllerFactory controllerFactory,
            TestControllersActionModel testControllersControllersModel)
            : base(controllerFactory, testControllersControllersModel)
        {
        }

        protected override void OnStart()
        {
            base.OnStart();
            Fail(new TestControllersException(TestControllersMethodsNamesConsts.FailMethodName));
        }
    }
}
