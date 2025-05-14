using Playtika.Controllers;

namespace UnitTests.Controllers
{
    public class ActionModelTestControllerBase : ControllerBase<TestControllerArgs>
    {
        private readonly TestControllersActionModel _testControllersActionModel;

        internal ActionModelTestControllerBase(
            IControllerFactory controllerFactory,
            TestControllersActionModel testControllersControllersModel)
            : base(controllerFactory)
        {
            _testControllersActionModel = testControllersControllersModel;
        }

        protected override void OnStart()
        {
            base.OnStart();
            AddDisposable(new DisposableToken(() =>
            {
                _testControllersActionModel.TriggerDispose();
            }));
            _testControllersActionModel.Args = Args.InputString;
            _testControllersActionModel.TriggerStart();
        }

        protected override void OnStop()
        {
            base.OnStop();
            _testControllersActionModel.TriggerStop();
        }
    }
}