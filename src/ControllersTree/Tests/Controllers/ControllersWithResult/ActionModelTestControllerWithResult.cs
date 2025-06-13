using System.Threading;
using System.Threading.Tasks;
using Playtika.Controllers;
using UnityEngine;

namespace UnitTests.Controllers
{
    internal class ActionModelTestControllerWithResult : ControllerWithResultBase<TestControllerArgs, TestEmptyControllerResult>
    {
        protected readonly TestControllersActionModel TestControllersActionModel;

        internal ActionModelTestControllerWithResult(
            IControllerFactory controllerFactory,
            TestControllersActionModel testControllersActionModel)
            : base(controllerFactory)
        {
            TestControllersActionModel = testControllersActionModel;
        }

        protected override void OnStart()
        {
            base.OnStart();
            AddDisposable(new DisposableToken(() =>
            {
                TestControllersActionModel.TriggerDispose();
            }));
            TestControllersActionModel.Args = Args.InputString;
            TestControllersActionModel.TriggerStart();
        }

        protected override async ValueTask OnFlowAsync(CancellationToken cancellationToken)
        {
            base.OnFlowAsync(cancellationToken);

            await Task.Delay(10, cancellationToken: cancellationToken);
            TestControllersActionModel.TriggerFlow();
            await Task.Delay(10, cancellationToken: cancellationToken);
        }

        protected override void OnStop()
        {
            TestControllersActionModel.TriggerStop();
        }
    }
}
