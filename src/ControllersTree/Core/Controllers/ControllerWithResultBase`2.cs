using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Playtika.Controllers
{
    public abstract partial class ControllerWithResultBase<TArg, TResult> : ControllerBase<TArg>, IControllerWithResult<TResult>
    {
        private readonly UniTaskCompletionSource<TResult> _resultSource = new UniTaskCompletionSource<TResult>();
        private ControllerWithResultState _withResultState;

        protected ControllerWithResultBase(IControllerFactory controllerFactory)
            : base(controllerFactory)
        {
        }

        async UniTask IControllerWithResult<TResult>.FlowAsync(CancellationToken cancellationToken)
        {
            switch (_withResultState)
            {
                case ControllerWithResultState.None:
                {
                    await OnFlowAsync(cancellationToken);
#if UNITY_EDITOR
                    _inFlowAsyncCompleted = true;
#endif
                    break;
                }
                case ControllerWithResultState.Completed:
                case ControllerWithResultState.Failed:
                    break;
                default:
                    throw new InvalidOperationException(
                        $"{Name} Flow async called from incorrect state. Current state: {_withResultState}");
            }
        }

        async UniTask<TResult> IControllerWithResult<TResult>.GetResult(CancellationToken token)
        {
            switch (_withResultState)
            {
                case ControllerWithResultState.None:
                    break;
                case ControllerWithResultState.WaitForResultAsync:
                    throw new InvalidOperationException(
                        $"{Name} ControllerWithResult awaited from incorrect state: {_withResultState}");
                case ControllerWithResultState.Completed:
                case ControllerWithResultState.Failed:
                    break;
            }

            _withResultState = ControllerWithResultState.WaitForResultAsync;
            await using (token.Register(Cancel, true))
            {
                return await _resultSource.Task;
            }
        }

        void IControllerWithResult<TResult>.FailInternal(Exception exception)
        {
            Fail(exception);
        }

        /// <summary>
        /// Override this for controller async flow.
        /// Started automatically after execute controller.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        protected virtual UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        /// Complete controller with result.
        /// </summary>
        /// <param name="result">Controller result.</param>
        protected void Complete(TResult result)
        {
            _withResultState = ControllerWithResultState.Completed;
            _resultSource.TrySetResult(result);
        }

        /// <summary>
        /// Complete controller with result.
        /// </summary>
        /// <param name="exception">Exception that caused the controller to stop operating.</param>
        protected void Fail(Exception exception)
        {
            _withResultState = ControllerWithResultState.Failed;
            _resultSource.TrySetException(exception);
        }

        private void Cancel()
        {
            _withResultState = ControllerWithResultState.Failed;
            _resultSource.TrySetCanceled();
        }

        public override string ToString()
        {
            return _withResultState switch
            {
                ControllerWithResultState.WaitForResultAsync => $"{Name} : {_withResultState}",
                _ => base.ToString()
            };
        }
    }
}
