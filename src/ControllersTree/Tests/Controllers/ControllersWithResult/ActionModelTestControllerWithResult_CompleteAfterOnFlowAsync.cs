using System.Threading;
using System.Threading.Tasks;
using Playtika.Controllers;
using UnityEngine;

namespace UnitTests.Controllers
{
    internal class ActionModelTestControllerWithResult_CompleteAfterOnFlowAsync : ActionModelTestControllerWithResult
    {
        internal ActionModelTestControllerWithResult_CompleteAfterOnFlowAsync(
            IControllerFactory controllerFactory,
            TestControllersActionModel testControllersActionModel)
            : base(controllerFactory, testControllersActionModel)
        {
        }

        protected override async ValueTask OnFlowAsync(CancellationToken cancellationToken)
        {
            await base.OnFlowAsync(cancellationToken);
            _ = AfterFlowAsync(cancellationToken);
        }

        private async ValueTask AfterFlowAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(10, cancellationToken: cancellationToken);
            Complete(new TestEmptyControllerResult(Args.InputString));
        }
    }
}
