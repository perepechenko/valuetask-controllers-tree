using System.Threading;
using Playtika.Controllers;
using Cysharp.Threading.Tasks;

namespace UnitTests.Controllers
{
    internal class ActionModelTestControllerWithResultAndChildControllers_FailOnFlowAsync : ActionModelTestControllerWithResultAndChildControllers
    {
        internal ActionModelTestControllerWithResultAndChildControllers_FailOnFlowAsync(
            IControllerFactory controllerFactory,
            TestControllersActionModel testControllersActionModel)
            : base(controllerFactory, testControllersActionModel)
        {
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            TestControllersActionModel.TriggerFlow();

            await StartChildControllersAsync(cancellationToken);
            throw new TestControllersException(TestControllersMethodsNamesConsts.OnFlowAsyncMethodName);
        }
    }
}
