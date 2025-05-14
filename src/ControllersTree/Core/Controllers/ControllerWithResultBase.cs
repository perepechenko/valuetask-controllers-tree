namespace Playtika.Controllers
{
    public abstract class ControllerWithResultBase : ControllerWithResultBase<EmptyControllerArg, EmptyControllerResult>
    {
        protected ControllerWithResultBase(IControllerFactory controllerFactory)
            : base(controllerFactory)
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
