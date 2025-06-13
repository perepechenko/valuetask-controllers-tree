namespace Playtika.Controllers
{
    public abstract class ControllerWithResultBase : ControllerWithResultBase<EmptyControllerArg, EmptyControllerResult>
    {
        protected ControllerWithResultBase(IControllerFactory controllerEnvironment)
            : base(controllerEnvironment)
        {
        }

        /// <summary>
        /// Complete controller with default result.
        /// </summary>
        protected void Complete()
        {
            Complete(default);
        }
    }
}
