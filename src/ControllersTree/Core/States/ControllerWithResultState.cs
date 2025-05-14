namespace Playtika.Controllers
{
    internal enum ControllerWithResultState
    {
        None,
        WaitForResultAsync,
        Completed,
        Failed
    }
}
