using System;

namespace Dialogic
{
    public class MockUnityEvent : EventArgs
    {
        protected string message;

        public MockUnityEvent() : this("User-Taps-Glass") { }

        public MockUnityEvent(string s)
        {
            this.message = s;
        }

        public string Message
        {
            get => message;
        }
    }
}