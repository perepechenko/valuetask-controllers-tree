using System.Threading;
using Playtika.Controllers;
using Cysharp.Threading.Tasks;

namespace UnitTests.Controllers
{
    internal class ActionModelTestControllerWithResult_FailOnFlowAsync : ActionModelTestControllerWithResult
    {
        internal ActionModelTestControllerWithResult_FailOnFlowAsync(
            IControllerFactory controllerFactory,
            TestControllersActionModel testControllersControllersModel)
            : base(controllerFactory, testControllersControllersModel)
        {
        }

        protected override UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            Fail(new TestControllersException(TestControllersMethodsNamesConsts.OnFlowAsyncMethodName));
            return UniTask.CompletedTask;
        }
    }
}
