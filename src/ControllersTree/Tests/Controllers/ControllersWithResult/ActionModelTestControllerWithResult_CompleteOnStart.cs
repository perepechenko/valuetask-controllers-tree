using Playtika.Controllers;

namespace UnitTests.Controllers
{
    internal class ActionModelTestControllerWithResult_CompleteOnStart : ActionModelTestControllerWithResult
    {
        internal ActionModelTestControllerWithResult_CompleteOnStart(
            IControllerFactory controllerFactory,
            TestControllersActionModel testControllersActionModel)
            : base(controllerFactory, testControllersActionModel)
        {
        }

        protected override void OnStart()
        {
            base.OnStart();
            Complete(new TestEmptyControllerResult(Args.InputString));
        }
    }
}
