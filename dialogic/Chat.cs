using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dialogic
{
    public class Find : Cond
    {
        public override ChatEvent Fire(ChatRuntime cr)
        {
            ChatEvent ce = base.Fire(cr);
            cr.Run(cr.Find(((Find)ce.Command).conditions));
            return ce;
        }
    }
        
    public class Cond : Command
    {
        public Dictionary<string, string> conditions;

        public Cond()
        {
            conditions = new Dictionary<string, string>();
        }

        public override void Init(params string[] args)
        {
            if (args.Length < 1) throw BadArgs(args, 1);

            for (int i = 0; i < args.Length; i++)
            {
                string[] parts = args[i].Split('=');
                if (parts.Length != 2) throw new Exception
                    ("Expected 2 parts, found " + parts.Length + ": " + parts);
                AddCondition(parts[0].Trim(), parts[1].Trim());
            }
        }

        /* Note: new keys will overwrite old keys with same name */
        public void AddCondition(string key, string val)
        {
            conditions[key] = val;
        }

        public void AddConditions(Cond cd)
        {
            foreach (var key in cd.conditions.Keys)
            {
                AddCondition(key, cd.conditions[key]);
            }
        }

        public virtual string ConditionsStr()
        {
            string s = "";
            if (conditions.Count > 0)
            {
                s += "(";
                foreach (var key in conditions.Keys)
                {
                    s += key + ":" + conditions[key] + ",";
                }
                s = s.Substring(0, s.Length - 1) + ")";
            }
            return s;
        }

        public override string ToString()
        {
            return base.ToString() + ConditionsStr();
        }
    }

    public class Chat : Cond
    {
        public List<Command> commands = new List<Command>(); // not copied

        public Chat() : this("C" + Math.Abs(Environment.TickCount)) { }

        public Chat(string name)
        {
            this.Text = name;
        }

        public void AddCommand(Command c)
        {
            this.commands.Add(c);
        }

        public override void Init(params string[] args)
        {
            if (args.Length < 1)
            {
                throw BadArgs(args, 1);
            }

            this.Text = args[0];

            if (Regex.IsMatch(Text, @"\s+"))
            {
                throw BadArg("CHAT name '" + Text + "' contains spaces!");
            }
        }

        public string ToTree()
        {
            string s = "[" + TypeName().ToUpper() + "] " + Text + "\n";
            if (conditions.Count > 0) s += "  [COND] "+ConditionsStr()+"\n";
            commands.ForEach(c => s += "  " + c + "\n");
            return s;
        }
    }
}
