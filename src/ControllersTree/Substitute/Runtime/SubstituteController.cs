using System;

namespace Playtika.Controllers.Substitute
{
    /// <summary>
    /// Controller Stub for tests.
    /// </summary>
    public class SubstituteController : ControllerBase
    {
        public SubstituteController()
            : base(null)
        {
        }
    }

    /// <summary>
    /// Generic Controller Stub for tests with ability to define fake behaviour.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SubstituteController<T> : ControllerBase<T>
    {
        private readonly ControllerBehaviour _behaviour;
        private readonly Exception _exception;
        public SubstituteController(ControllerBehaviour behaviour, Exception exception)
            : base(null)
        {
            _behaviour = behaviour;
            _exception = exception;
        }

        protected override void OnStart()
        {
            base.OnStart();
            if (_behaviour == ControllerBehaviour.FailOnStart)
            {
                throw _exception;
            }
        }
    }
}