using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Playtika.Controllers.Substitute
{
    /// <summary>
    /// Controller With Result Stub for tests with ability to define fake behaviour.
    /// </summary>
    /// <typeparam name="TArg"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class SubstituteControllerWithResult<TArg, TResult> : ControllerWithResultBase<TArg, TResult>
    {
        private readonly ControllerBehaviour _behaviour;
        private readonly TResult _result;
        private readonly Exception _exception;

        public SubstituteControllerWithResult(TResult result, ControllerBehaviour behaviour, Exception exception)
            : base(null)
        {
            _result = result;
            _behaviour = behaviour;
            _exception = exception;
        }

        protected override void OnStart()
        {
            base.OnStart();
            switch (_behaviour)
            {
                case ControllerBehaviour.CompleteOnStart:
                    Complete(_result);
                    break;
                case ControllerBehaviour.FailOnStart:
                    throw _exception;
            }
        }
        
        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            await base.OnFlowAsync(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            await UniTask.Yield(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            switch (_behaviour)
            {
                case ControllerBehaviour.CompleteOnFlowAsync:
                    Complete(_result);
                    break;
                case ControllerBehaviour.FailOnFlowAsync:
                    throw _exception;
            }
        }
    }
}