using System;
using System.Collections.Generic;

namespace Dialogic
{
    public interface ISpeaker
    {
        string Name();
        bool IsDefault();
        Func<Command, bool>[] Validators();
        IDictionary<string, Type> Commands();
    }

    public class Speaker : ISpeaker
    {
        readonly string name;

        bool isDefault;
        Func<Command, bool>[] validators;
        IDictionary<string, Type> commandsTypes;

        public Speaker(string label, IDictionary<string, Type> commands = null,
            params Func<Command, bool>[] validators)
            : this(label, false, commands, validators)
        {
        }

        public Speaker(string label, bool isDefault = false, IDictionary<string, Type>
            commands = null, params Func<Command, bool>[] validators)
        {
            this.name = label;
            this.isDefault = isDefault;
            this.commandsTypes = commands;
            this.validators = validators;
        }

        public string Name()
        {
            return name;
        }

        public bool IsDefault()
        {
            return isDefault;
        }

        public Func<Command, bool>[] Validators()
        {
            return validators;
        }

        public IDictionary<string, Type> Commands()
        {
            return commandsTypes;
        }
    }
}
