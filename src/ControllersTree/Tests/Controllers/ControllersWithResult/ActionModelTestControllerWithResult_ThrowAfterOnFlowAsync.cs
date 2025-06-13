using System.Threading;
using System.Threading.Tasks;
using Playtika.Controllers;
using UnityEngine;

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

        protected override async ValueTask OnFlowAsync(CancellationToken cancellationToken)
        {
            await base.OnFlowAsync(cancellationToken);
            _ = ThrowAfterFlowAsync(cancellationToken);
        }

        private async ValueTask ThrowAfterFlowAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(10, cancellationToken: cancellationToken);
            throw new TestControllersException(TestControllersMethodsNamesConsts.CompleteMethodName);
        }
    }
}
