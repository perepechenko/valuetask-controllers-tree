using System.Threading;
using System.Threading.Tasks;
using Playtika.Controllers;
using UnityEngine;

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

        protected override async ValueTask OnFlowAsync(CancellationToken cancellationToken)
        {
            await base.OnFlowAsync(cancellationToken);
            Complete(new TestEmptyControllerResult(Args.InputString));
        }
    }
}
