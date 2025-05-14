namespace Playtika.Controllers
{
    public interface IControllerFactory
    {
        IController Create<T>() where T : class, IController;
    }
}