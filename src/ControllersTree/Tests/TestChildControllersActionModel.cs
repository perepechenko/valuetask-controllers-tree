using System;

namespace UnitTests.Controllers
{
    internal class TestChildControllersActionModel
    {
        public string Args;

        public event Action<string> StartTriggered;
        public event Action<string> FlowTriggered;
        public event Action<string> StopTriggered;
        public event Action<string> DisposeTriggered;

        public void TriggerChildStart(string childControllerName)
        {
            StartTriggered?.Invoke(childControllerName);
        }

        public void TriggerChildFlow(string childControllerName)
        {
            FlowTriggered?.Invoke(childControllerName);
        }

        public void TriggerChildStop(string childControllerName)
        {
            StopTriggered?.Invoke(childControllerName);
        }

        public void TriggerChildDispose(string childControllerName)
        {
            DisposeTriggered?.Invoke(childControllerName);
        }
    }
}
