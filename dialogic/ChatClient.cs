using System;

namespace Dialogic
{
    public abstract class ChatClient
    {
        public delegate void UnityEventHandler(EventArgs e);
        public event UnityEventHandler UnityEvents;

        public void Subscribe(ChatRuntime cs)
        {
            cs.ChatEvents += new ChatRuntime.ChatEventHandler(OnChatEvent);
        }

        protected abstract void OnChatEvent(ChatRuntime cm, ChatEvent e);
    }
}