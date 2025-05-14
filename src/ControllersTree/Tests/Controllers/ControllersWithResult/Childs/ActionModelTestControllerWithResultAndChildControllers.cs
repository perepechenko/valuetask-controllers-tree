using System;
using System.Collections.Generic;
using System.Threading;
using Playtika.Controllers;
using Cysharp.Threading.Tasks;
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

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            TestControllersActionModel.TriggerFlow();

            await StartChildControllersAsync(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            Complete(new TestEmptyControllerResult(Args.InputString));
        }

        protected async UniTask StartChildControllersAsync(CancellationToken cancellationToken)
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

        private List<Func<string, CancellationToken, UniTask>> GetAsyncLaunchers()
        {
            return new List<Func<string, CancellationToken, UniTask>>
            {
                StartChildControllersAsync1,
                StartChildControllersAsync2,
                StartChildControllersAsync3,
                StartChildControllersAsync4,
                StartChildControllersAsync5,
            };
        }

        private async UniTask StartChildControllersAsync1(string childControllerGuid, CancellationToken cancellationToken)
        {
            await ExecuteAndWaitResultAsync<ActionModelTestChildControllerWithResult1, string>(childControllerGuid, cancellationToken);
        }

        private async UniTask StartChildControllersAsync2(string childControllerGuid, CancellationToken cancellationToken)
        {
            await ExecuteAndWaitResultAsync<ActionModelTestChildControllerWithResult2, string>(childControllerGuid, cancellationToken);
        }

        private async UniTask StartChildControllersAsync3(string childControllerGuid, CancellationToken cancellationToken)
        {
            await ExecuteAndWaitResultAsync<ActionModelTestChildControllerWithResult3, string>(childControllerGuid, cancellationToken);
        }

        private async UniTask StartChildControllersAsync4(string childControllerGuid, CancellationToken cancellationToken)
        {
            await ExecuteAndWaitResultAsync<ActionModelTestChildControllerWithResult4, string>(childControllerGuid, cancellationToken);
        }

        private async UniTask StartChildControllersAsync5(string childControllerGuid, CancellationToken cancellationToken)
        {
            await ExecuteAndWaitResultAsync<ActionModelTestChildControllerWithResult5, string>(childControllerGuid, cancellationToken);
        }
    }
}
