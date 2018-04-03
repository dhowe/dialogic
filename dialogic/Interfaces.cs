using System;
using System.Collections.Generic;

namespace Dialogic
{
    public interface IActor
    {
        string Name();
        bool IsDefault();
        CommandDef[] Commands();
        Func<Command, bool> Validator();
    }

    ////////////////////////////// Commands ///////////////////////////////////
     
    /// <summary>
    /// Tagging interface denoting Commands should be dispatched to clients
    /// </summary>
    public interface ISendable { }

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


    /**
     * Tells Dialogic that the User has performed a specific action, repesented by
     * the eventType.
     */
    public interface IUserEvent
    {
        string GetEventType();
    }

    /**
     * Tells Dialogic that some configuration change need to be made
     */
    public interface IConfigEvent
    {
        Constraint FindByCriteria();
        string PropertyName();
        string PropertyValue();
    }

    /**
     * Tells Dialogic to run the specifid action on one or more Chats
     */
    public interface IChatUpdate
    {
        string FindByCriteria();
        Action<Chat> GetAction();
    }

    /**
     * Tells Dialogic to restart the current Chat after a indefinite WAIT cmd, 
     * or an ISuspend event, optionally with a specific chat, specified with its 
     * Label, or with a Find, specified via its metadata constraints.
     */
    public interface IResume
    {
        string ResumeWith();
    }

    /**
     * Suspends the current Chat. The current chat (or a new chat) can be resumed by 
     * sending a ResumeEvent.
     */
    public interface ISuspend
    {
    }

    /**
     * Tells Dialogic to clear its stack of past chats, leaving none to be resumed
     */
    public interface IClear
    {
    }

    /**
     * Sent by Dialogic whenever an IEmittable Command (e.g., Say, Ask, Do, Wait) 
     * is invoked so that it may be handled by the application
     */
    public interface IUpdateEvent
    {
        string Text();
        string Type();
        string Get(string name, string def = null);
        void RemoveKeys(params string[] keys);
        IDictionary<string, object> Data();

        int GetInt(string name, int def = -1);
        bool GetBool(string key, bool def = false);
        double GetDouble(string key, double def = -1);
        float GetFloat(string key, float def = -1);
    }

}
