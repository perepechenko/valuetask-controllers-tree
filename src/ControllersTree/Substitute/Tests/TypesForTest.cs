namespace Playtika.Controllers.Substitute.Tests
{
    internal class TypesForTest
    {
        public interface ITestController : IController
        {
        }

        public interface ITestControllerWithArgs : IController, IController<int>
        {
        }

        public interface ITestControllerWithResult : IControllerWithResult<EmptyControllerResult>, IController<EmptyControllerArg>
        {
        }
        
        public interface ITestControllerWithResultWithArgs : IControllerWithResult<EmptyControllerResult>, IController<int>
        {
            
        }
        
        public interface ITestControllerWithResultGeneric<T> : IControllerWithResult<T>, IController<EmptyControllerArg>
        {
            
        }
        
        public interface ITestControllerWithResultTWithArgs : IControllerWithResult<bool>, IController<int>
        {
            
        }
        
        public interface ITestControllerWithResultGenericWithArgsGeneric<TArg, TResult> : IControllerWithResult<TResult>, IController<TArg>
        {
            
        }

        public interface ITestControllerWithResultT : IControllerWithResult<bool>
        {
        }

        public class TestController : ControllerBase
        {
            public TestController(IControllerFactory controllerFactory)
                : base(controllerFactory)
            {
            }
        }

        public class TestControllerWithArgs : ControllerBase<int>
        {
            public TestControllerWithArgs(IControllerFactory controllerFactory)
                : base(controllerFactory)
            {
            }
        }

        public class TestControllerWithResult : ControllerWithResultBase
        {
            public TestControllerWithResult(IControllerFactory controllerFactory)
                : base(controllerFactory)
            {
            }
        }

        public class TestControllerWithResultT : ControllerWithResultBase<bool>
        {
            public TestControllerWithResultT(IControllerFactory controllerFactory)
                : base(controllerFactory)
            {
            }
        }
        
        public class TestControllerWithResultGeneric<T> : ControllerWithResultBase<T>
        {
            public TestControllerWithResultGeneric(IControllerFactory controllerFactory)
                : base(controllerFactory)
            {
            }
        }
        
        public class TestControllerWithResultWithArg : ControllerWithResultBase<int, bool>
        {
            public TestControllerWithResultWithArg(IControllerFactory controllerFactory)
                : base(controllerFactory)
            {
            }
        }
        
        public class TestControllerWithResultGenericWithArgsGeneric<TArg, TResult> : ControllerWithResultBase<TArg, TResult>
        {
            public TestControllerWithResultGenericWithArgsGeneric(IControllerFactory controllerFactory)
                : base(controllerFactory)
            {
            }
        }
    }
}