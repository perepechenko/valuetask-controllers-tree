namespace Playtika.Controllers
{
    public class ControllerBase<TArg> : ControllerBase, IController<TArg>
    {
        protected TArg Args { get; private set; } = default;

        protected ControllerBase(IControllerFactory controllerEnvironment)
            : base(controllerEnvironment)
        {
        }

        void IController<TArg>.SetArgInternal(TArg arg)
        {
            Args = arg;
        }
    }
}