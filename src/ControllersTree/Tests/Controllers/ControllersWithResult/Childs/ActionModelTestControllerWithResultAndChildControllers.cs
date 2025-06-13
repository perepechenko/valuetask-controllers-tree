using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Playtika.Controllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace UnitTests.Controllers
{
    internal class ActionModelTestControllerWithResultAndChildControllers : ControllerWithResultBase<TestChildControllerArgs, TestEmptyControllerResult>
    {
        protected readonly TestControllersActionModel TestControllersActionModel;

        internal ActionModelTestControllerWithResultAndChildControllers(
            IControllerFactory controllerFactory,
            TestControllersActionModel testControllersActionModel)
            : base(controllerFactory)
        {
            TestControllersActionModel = testControllersActionModel;
        }

        protected override void OnStart()
        {
            base.OnStart();
            AddDisposable(new DisposableToken(() =>
            {
                TestControllersActionModel.TriggerDispose();
            }));
            TestControllersActionModel.Args = Args.InputString;
            TestControllersActionModel.TriggerStart();
        }

        protected override async ValueTask OnFlowAsync(CancellationToken cancellationToken)
        {
            TestControllersActionModel.TriggerFlow();

            await StartChildControllersAsync(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            Complete(new TestEmptyControllerResult(Args.InputString));
        }

        protected async ValueTask StartChildControllersAsync(CancellationToken cancellationToken)
        {
            var launchers = GetAsyncLaunchers();
            Assert.AreEqual(Args.ChildControllersGuids.Count, launchers.Count);
            var i = 0;
            foreach (var childControllerGuid in Args.ChildControllersGuids)
            {
                var launcher = launchers[i];
                await launcher(childControllerGuid, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                i++;
            }
        }

        protected override void OnStop()
        {
            TestControllersActionModel.TriggerStop();
        }

        private List<Func<string, CancellationToken, ValueTask>> GetAsyncLaunchers()
        {
            return new List<Func<string, CancellationToken, ValueTask>>
            {
                StartChildControllersAsync1,
                StartChildControllersAsync2,
                StartChildControllersAsync3,
                StartChildControllersAsync4,
                StartChildControllersAsync5,
            };
        }

        private async ValueTask StartChildControllersAsync1(string childControllerGuid, CancellationToken cancellationToken)
        {
            await ExecuteAndWaitResultAsync<ActionModelTestChildControllerWithResult1, string>(childControllerGuid, cancellationToken);
        }

        private async ValueTask StartChildControllersAsync2(string childControllerGuid, CancellationToken cancellationToken)
        {
            await ExecuteAndWaitResultAsync<ActionModelTestChildControllerWithResult2, string>(childControllerGuid, cancellationToken);
        }

        private async ValueTask StartChildControllersAsync3(string childControllerGuid, CancellationToken cancellationToken)
        {
            await ExecuteAndWaitResultAsync<ActionModelTestChildControllerWithResult3, string>(childControllerGuid, cancellationToken);
        }

        private async ValueTask StartChildControllersAsync4(string childControllerGuid, CancellationToken cancellationToken)
        {
            await ExecuteAndWaitResultAsync<ActionModelTestChildControllerWithResult4, string>(childControllerGuid, cancellationToken);
        }

        private async ValueTask StartChildControllersAsync5(string childControllerGuid, CancellationToken cancellationToken)
        {
            await ExecuteAndWaitResultAsync<ActionModelTestChildControllerWithResult5, string>(childControllerGuid, cancellationToken);
        }
    }
}
