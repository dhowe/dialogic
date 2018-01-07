namespace Dialogic
{
    public interface IChatListener
    {
        void onChatEvent(ChatManager cm, Command c);
    }
}