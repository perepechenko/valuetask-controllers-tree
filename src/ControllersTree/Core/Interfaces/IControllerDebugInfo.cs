#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Playtika.Controllers
{
    internal interface IControllerDebugInfo : IEnumerable<IControllerDebugInfo>
    {
        internal string ControllerType { get; }
        internal string StateName { get; }
        internal Color StateColor { get; }
        internal string ScopeName { get; }
        void AddDisposable(IDisposable disposable);
    }
}
#endif
