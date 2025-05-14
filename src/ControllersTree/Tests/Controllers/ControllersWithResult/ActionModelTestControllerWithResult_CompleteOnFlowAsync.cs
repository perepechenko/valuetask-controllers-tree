using System.Threading;
using Playtika.Controllers;
using Cysharp.Threading.Tasks;

namespace UnitTests.Controllers
{
    internal class ActionModelTestControllerWithResult_CompleteOnFlowAsync : ActionModelTestControllerWithResult
    {
        public ActionModelTestControllerWithResult_CompleteOnFlowAsync(
            IControllerFactory controllerFactory,
            TestControllersActionModel testControllersActionModel)
            : base(controllerFactory, testControllersActionModel)
        {
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            await base.OnFlowAsync(cancellationToken);
            Complete(new TestEmptyControllerResult(Args.InputString));
        }
    }
}
