using System.Threading;
using Playtika.Controllers;
using Cysharp.Threading.Tasks;

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

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            await base.OnFlowAsync(cancellationToken);
            AfterFlowAsync(cancellationToken).Forget();
        }

        private async UniTask AfterFlowAsync(CancellationToken cancellationToken)
        {
            await UniTask.Delay(10, cancellationToken: cancellationToken);
            Complete(new TestEmptyControllerResult(Args.InputString));
        }
    }
}
