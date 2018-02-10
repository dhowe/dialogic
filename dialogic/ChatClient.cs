using System;
using System.Threading;

namespace Dialogic
{
    public abstract class ChatClient // Client superclass
    {
        public delegate void UnityEventHandler(EventArgs e);
        public event UnityEventHandler UnityEvents;

        protected abstract void OnChatEvent(ChatRuntime cm, ChatEvent e);

        public void Subscribe(ChatRuntime cs)
        {
            cs.ChatEvents += new ChatRuntime.ChatEventHandler(OnChatEvent);
        }

        protected void Fire(EventArgs e)
        {
            if (UnityEvents != null) UnityEvents.Invoke(e);
        }
    }
}