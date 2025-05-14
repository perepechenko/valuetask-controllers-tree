using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Playtika.Controllers
{
    public interface IControllerWithResult<TResult> : IController
    {
        internal UniTask FlowAsync(CancellationToken cancellationToken);
        internal UniTask<TResult> GetResult(CancellationToken token);
        internal void FailInternal(Exception exception);
    }
}
