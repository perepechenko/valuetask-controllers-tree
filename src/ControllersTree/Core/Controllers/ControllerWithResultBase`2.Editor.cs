#if UNITY_EDITOR
using UnityEngine;

namespace Playtika.Controllers
{
    public partial class ControllerWithResultBase<TArg, TResult> : ControllerBase<TArg>, IControllerDebugInfo
    {
        private bool _inFlowAsyncCompleted;

        protected override string ControllerTypeInternal() =>
            nameof(ControllerWithResultBase);

        protected override string StateNameInternal()
        {
            switch (_withResultState)
            {
                case ControllerWithResultState.WaitForResultAsync:
                    return _inFlowAsyncCompleted
                               ? ControllerWithResultState.WaitForResultAsync.ToString()
                               : "await OnFlowAsync()";
                default:
                    return base.StateNameInternal();
            }
        }

        protected override Color StateColorInternal()
        {
            switch (_withResultState)
            {
                case ControllerWithResultState.None:
                    return Color.white;
                case ControllerWithResultState.WaitForResultAsync:
                    return _inFlowAsyncCompleted
                               ? new Color(0.25f, 0.65f, 0.75f, 1.0f)
                               : new Color(1.0f, 0.647f, 0.0f, 1.0f);
                case ControllerWithResultState.Completed:
                    return new Color(0.13f, 0.9f, 0f);
                case ControllerWithResultState.Failed:
                    return Color.red;
                default:
                    return Color.magenta;
            }
        }
    }
}
#endif
