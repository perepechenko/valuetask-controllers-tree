using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Pool;

namespace Playtika.Controllers
{
    /// <summary>
    /// Component of controller that keeps related disposable object that must be disposed together with the running controller.
    /// </summary>
    public class ControllerCompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> _disposables = ListPool<IDisposable>.Get();

        /// <summary>
        /// Adds a disposable object to the internal list of disposables.
        /// </summary>
        /// <param name="disposable">The disposable object to add to the list.</param>
        public void Add(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        /// <summary>
        /// Adds a collection of disposable objects to the internal list of disposables.
        /// </summary>
        /// <param name="collection">The collection of disposable objects to add to the list.</param>
        public void AddRange(IEnumerable<IDisposable> collection)
        {
            _disposables.AddRange(collection);
        }

        public void Dispose()
        {
            using var pooledObject = ListPool<Exception>.Get(out var exceptionList);

            foreach (var disposable in _disposables)
            {
                try
                {
                    disposable.Dispose();
                }
                catch (Exception e)
                {
                    exceptionList.Add(e);
                }
            }

            _disposables.Clear();

            ListPool<IDisposable>.Release(_disposables);

            if (exceptionList.Any())
            {
                throw new AggregateException(exceptionList);
            }
        }
    }
}
