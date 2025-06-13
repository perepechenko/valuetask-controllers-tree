using System.Threading;
using System.Threading.Tasks;
using Playtika.Controllers;
using UnityEngine;

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

        protected override ValueTask OnFlowAsync(CancellationToken cancellationToken)
        {
            Fail(new TestControllersException(TestControllersMethodsNamesConsts.OnFlowAsyncMethodName));
            return default;
        }
    }
}
