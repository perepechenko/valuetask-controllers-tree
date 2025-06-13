using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Playtika.Controllers
{
    public interface IControllerWithResult<TResult> : IController
    {
        internal ValueTask FlowAsync(CancellationToken cancellationToken);
        internal ValueTask<TResult> GetResult(CancellationToken token);
        internal void FailInternal(Exception exception);
    }
}
