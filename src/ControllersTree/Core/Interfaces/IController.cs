using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Playtika.Controllers
{
    public interface IController : IDisposable
    {
        string Name { get; }
        internal CancellationToken CancellationToken { get; }
        void AddDisposable(IDisposable disposable);
        void AddDisposables(IEnumerable<IDisposable> collection);
        internal void Initialize(CancellationToken externalCancellationToken, CancellationToken parentCancellationToken);
        internal void Start();
        internal void Stop();
        internal void Stop(Exception rootCauseException);
        internal void ScanTree(List<string> controllersTree, string prefix);
    }

    public interface IController<in TArg>
    {
        internal void SetArgInternal(TArg arg);
    }
}
