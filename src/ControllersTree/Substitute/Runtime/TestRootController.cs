using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Playtika.Controllers.Substitute
{
    /// <summary>
    /// Root controller for testing.
    /// </summary>
    public sealed class TestRootController : RootController
    {
        public TestRootController(IControllerFactory controllerFactory)
            : base(controllerFactory)
        {
        }
        
        public new void Execute<T>()
            where T : class, IController
        {
            base.Execute<T>();
        }

        public new void Execute<T, TArg>(TArg arg)
            where T : class, IController, IController<TArg>
        {
            base.Execute<T, TArg>(arg);
        }

        public new ValueTask ExecuteAndWaitResultAsync<T>(
            CancellationToken cancellationToken)
            where T : class, IControllerWithResult<EmptyControllerResult>, IController<EmptyControllerArg>
        {
            return base.ExecuteAndWaitResultAsync<T>(cancellationToken);
        }

        public new ValueTask<TResult> ExecuteAndWaitResultAsync<T, TResult>(
            CancellationToken cancellationToken)
            where T : class, IControllerWithResult<TResult>, IController<EmptyControllerArg>
        {
            return base.ExecuteAndWaitResultAsync<T, TResult>(cancellationToken);
        }

        public new ValueTask ExecuteAndWaitResultAsync<T, TArg>(
            TArg arg,
            CancellationToken cancellationToken)
            where T : class, IControllerWithResult<EmptyControllerResult>, IController<TArg>
        {
            return base.ExecuteAndWaitResultAsync<T, TArg>(
                arg,
                cancellationToken);
        }

        public new ValueTask<TResult> ExecuteAndWaitResultAsync<T, TArg, TResult>(
            TArg arg,
            CancellationToken cancellationToken)
            where T : class, IControllerWithResult<TResult>, IController<TArg>
        {
            return base.ExecuteAndWaitResultAsync<T, TArg, TResult>(
                arg,
                cancellationToken);
        }
    }
}