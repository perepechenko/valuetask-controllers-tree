using System;

namespace UnitTests.Controllers
{
    public class TestControllersActionModel
    {
        public string Args;
        public event Action StartTriggered;
        public event Action FlowTriggered;
        public event Action StopTriggered;
        public event Action DisposeTriggered;

        public void TriggerStart() => StartTriggered?.Invoke();
        public void TriggerStop() => StopTriggered?.Invoke();
        public void TriggerFlow() => FlowTriggered?.Invoke();
        public void TriggerDispose() => DisposeTriggered?.Invoke();
    }
}
