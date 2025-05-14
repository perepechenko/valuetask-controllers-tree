namespace Playtika.Controllers
{
    public abstract class ControllerWithResultBase<TResult> : ControllerWithResultBase<EmptyControllerArg, TResult>, IControllerWithResult<TResult>
    {
        protected ControllerWithResultBase(IControllerFactory controllerFactory)
            : base(controllerFactory)
        {
        }
    }
}