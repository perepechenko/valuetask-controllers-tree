using System.Threading;
using Playtika.Controllers;
using Cysharp.Threading.Tasks;

namespace UnitTests.Controllers
{
    internal class ActionModelTestControllerWithResult_ThrowAfterOnFlowAsync : ActionModelTestControllerWithResult
    {
        internal ActionModelTestControllerWithResult_ThrowAfterOnFlowAsync(
            IControllerFactory controllerFactory,
            TestControllersActionModel testControllersActionModel)
            : base(controllerFactory, testControllersActionModel)
        {
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            await base.OnFlowAsync(cancellationToken);
            ThrowAfterFlowAsync(cancellationToken).Forget();
        }

        private async UniTask ThrowAfterFlowAsync(CancellationToken cancellationToken)
        {
            await UniTask.Delay(10, cancellationToken: cancellationToken);
            throw new TestControllersException(TestControllersMethodsNamesConsts.CompleteMethodName);
        }
    }
}
