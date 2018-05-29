using System;
using System.Collections.Generic;
using System.IO;

namespace Dialogic
{
    /// <summary>
    /// To be implemented by classes handling serializing and deserializing
    /// </summary>
    public interface ISerializer
    {
        byte[] ToBytes(ChatRuntime rt);
        void FromBytes(ChatRuntime rt, byte[] b);
        string ToJSON(ChatRuntime rt);
        //byte[] ToBytes(List<Chat> l);
    }

    /// <summary>
    /// An Actor to which IAssignable Commands can be assigned; may also include custom Commands and validators specific to the client application
    /// </summary>
    public interface IActor
    {
        string Name();
        bool IsDefault();
        CommandDef[] Commands();
        Func<Command, bool> Validator();
    }

    /// <summary>
    /// To be implemented by clients for specific configurations
    /// </summary>
    public interface IAppConfig
    {
        List<IActor> GetActors();
        List<CommandDef> GetCommands();
        List<Func<Command, bool>> GetValidators();
        IDictionary<string, Func<string, string>> GetTransforms();
    }

    /// <summary>
    /// To be subclassed by clients for specific configurations
    /// </summary>
    public class ConfigAdaptor
    {
        List<IActor> GetActors() { return null; }
        List<CommandDef> GetCommands() { return null; }
        List<Func<Command, bool>> GetValidators() { return null; }
        IDictionary<string, Func<string, string>> GetTransforms() { return null; }
    }


    ////////////////////////////// Commands ///////////////////////////////////


    /// <summary>
    /// Tagging interface denoting Commands should be dispatched to clients
    /// </summary>
    public interface ISendable
    {

        IDictionary<string, object> Resolved();
    }

    /// <summary>
    /// Tagging interface denoting Commands that can be assigned an Actor
    /// </summary>
    public interface IAssignable : ISendable { }


    /////////////////////////////// Events ////////////////////////////////////


    /// <summary>
    /// Tells Dialogic that the User has made a specific choice in response to a prompt
    /// </summary>
    public interface IChoice
    {
        int GetChoiceIndex();
    }

    /// <summary>
    /// Tells Dialogic that the user has performed a specific action, repesented by the eventType.
    /// </summary>
    public interface IUserEvent
    {
        string GetEventType();
    }

    /// <summary>
    /// Tells Dialogic that some configuration change need to be made
    /// </summary>
    public interface IConfigEvent
    {
        Constraint FindByCriteria();
        string PropertyName();
        string PropertyValue();
    }

    /// <summary>
    /// Tells Dialogic to run the specifid action on one or more Chats
    /// </summary>
    public interface IChatUpdate
    {
        string FindByCriteria();
        Action<Chat> GetAction();
    }


    /// <summary>
    ///  Tells Dialogic to restart the current Chat after a indefinite WAIT cmd, or an ISuspend event, optionally with a specific chat, specified with its Label, or with a Find, specified via its metadata constraints.
    /// </summary>
    public interface IResume
    {
        string ResumeWith();
    }

    /// <summary>
    /// Suspends the current Chat. The current chat (or a new chat) can be resumed by  sending a ResumeEvent.
    /// </summary>
    public interface ISuspend
    {
    }

    /// <summary>
    /// Tells Dialogic to clear its stack of past chats, leaving none to be resumed
    /// </summary>
    public interface IClear
    {
    }

    /// <summary>
    /// Tells Dialogic to serialize the current start of the game
    /// </summary>
    public interface ISaveEvent
    {
        ISerializer GetSerializer();
        FileInfo GetFile();
    }

    /// <summary>
    /// Add a new set of Chats to the current runtime, potentially overwriting one or more of those already loaded
    /// </summary>
    public interface ILoadEvent
    {
        List<Chat> GetChats();
    }

    /// <summary>
    /// Sent by Dialogic whenever an ISendable Command (e.g., Say, Ask, Do, Wait) is invoked so that it may be handled by the client application
    /// </summary>
    public interface IUpdateEvent
    {
        string Text();
        string Type();
        string Actor();

        string Get(string name, string def = null);
        void RemoveKeys(params string[] keys);
        IDictionary<string, object> Data();

        int GetInt(string name, int def = -1);
        bool GetBool(string key, bool def = false);
        double GetDouble(string key, double def = -1);
        float GetFloat(string key, float def = -1);
    }

    /////////////////////////////// Other ////////////////////////////////////

    internal interface IResolvable { }// Choice and Symbol
}
