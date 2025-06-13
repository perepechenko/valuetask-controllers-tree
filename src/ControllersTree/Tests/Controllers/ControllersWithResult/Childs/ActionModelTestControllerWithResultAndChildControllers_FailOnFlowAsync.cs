using System.Threading;
using System.Threading.Tasks;
using Playtika.Controllers;
using UnityEngine;

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

        protected override async ValueTask OnFlowAsync(CancellationToken cancellationToken)
        {
            TestControllersActionModel.TriggerFlow();

            await StartChildControllersAsync(cancellationToken);
            throw new TestControllersException(TestControllersMethodsNamesConsts.OnFlowAsyncMethodName);
        }
    }
}
