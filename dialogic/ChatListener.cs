namespace Dialogic
{
    public interface IChatListener
    {
        void onChatEvent(ChatScheduler cs, Command c);
    }
}