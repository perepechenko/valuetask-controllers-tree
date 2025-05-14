using System;
using System.Collections.Generic;
using System.Linq;

namespace Playtika.Controllers.Substitute
{
    /// <summary>
    /// Variants of fake controller behavior.
    /// </summary>
    public enum ControllerBehaviour
    {
        CompleteOnStart,
        CompleteOnFlowAsync,
        NeverComplete,
        FailOnStart,
        FailOnFlowAsync,
    }

    /// <summary>
    /// Override controller factory for testing.
    /// </summary>
    public class SubstituteControllerFactory : IControllerFactory
    {
        private readonly Dictionary<Type, IController> _instances = new();
        private readonly Dictionary<Type, object> _results = new();
        private readonly Dictionary<Type, int> _received = new();
        private readonly Dictionary<Type, ControllerBehaviour> _behaviours = new();
        private readonly Dictionary<Type, Exception> _exceptions = new();

        public IController Create<T>() where T : class, IController
        {
            var type = typeof(T);
            if (_instances.TryGetValue(type, out var instance))
            {
                WriteReceived<T>();
                return instance;
            }

            if (TryCreateStubControllerWithResult<T>(type, out var controllerWithResult))
            {
                return controllerWithResult;
            }
            
            if (TryCreateStubControllerBase<T>(type, out var controllerBase))
            {
                return controllerBase;
            }
            
            throw new Exception($"Unhandled type {type.Name}");
        }

        public SubstituteControllerFactory AddInstance<T>(T instance)
            where T : IController
        {
            var type = typeof(T);
            _instances[type] = instance;
            return this;
        }

        public SubstituteControllerFactory AddResultFor<T>(object result)
            where T : IController
        {
            var type = typeof(T);
            _results[type] = result;
            _behaviours[type] = ControllerBehaviour.CompleteOnStart;
            return this;
        }

        public SubstituteControllerFactory AddExceptionFor<T>(Exception exception)
            where T : IController
        {
            var type = typeof(T);
            _exceptions[type] = exception;
            _behaviours[type] = ControllerBehaviour.FailOnStart;
            return this;
        }

        public SubstituteControllerFactory SetBehaviourFor<T>(ControllerBehaviour behaviour)
        {
            var type = typeof(T);
            _behaviours[type] = behaviour;
            return this;
        }

        public int Received<T>()
        {
            _received.TryGetValue(typeof(T), out var count);
            return count;
        }

        private bool TryCreateStubControllerWithResult<T>(Type type, out IController controller)
            where T : class, IController
        {
            controller = null;
            var interfaces = type.GetInterfaces();
            var interfaceType = interfaces.FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IControllerWithResult<>));

            if (interfaceType != null)
            {
                var argumentType = typeof(EmptyControllerArg);
                var interfaceForArgument = interfaces.FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IController<>));

                if (interfaceForArgument != null)
                {
                    argumentType = interfaceForArgument.GetGenericArguments()[0];
                }

                var resultType = interfaceType.GetGenericArguments()[0];
                var substituteType = typeof(SubstituteControllerWithResult<,>).MakeGenericType(argumentType, resultType);

                WriteReceived<T>();

                var obj = Activator.CreateInstance(substituteType, GetResult<T>(), GetBehaviour<T>(), GetException<T>());
                controller = obj as IController;
                return true;
            }

            return false;
        }
        
        private bool TryCreateStubControllerBase<T>(Type type, out IController controller)
            where T : class, IController
        {
            controller = null;
            var interfaces = type.GetInterfaces();
            var interfaceType = interfaces.FirstOrDefault(x => x == typeof(IController));

            if (interfaceType != null)
            {
                WriteReceived<T>();

                var interfaceForArgument = interfaces.FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IController<>));

                if (interfaceForArgument == null)
                {
                    controller = new SubstituteController();
                    return true;
                }

                var argumentType = interfaceForArgument.GetGenericArguments()[0];
                var substituteType = typeof(SubstituteController<>).MakeGenericType(argumentType);

                var obj = Activator.CreateInstance(substituteType, GetBehaviour<T>(), GetException<T>());
                controller = obj as IController;
                return true;
            }

            return false;
        }

        private object GetResult<T>()
        {
            _results.TryGetValue(typeof(T), out var result);
            return result;
        }

        private ControllerBehaviour GetBehaviour<T>()
        {
            return _behaviours.GetValueOrDefault(typeof(T), ControllerBehaviour.CompleteOnStart);
        }

        private Exception GetException<T>()
        {
            return _exceptions.GetValueOrDefault(typeof(T), new Exception("Mock controller fail"));
        }

        private void WriteReceived<T>()
        {
            var type = typeof(T);
            _received.TryAdd(type, 0);
            _received[type]++;
        }
    }
}