using System;
using System.Collections.Generic;
using System.Text;

//adding a command is easy, just add an entry in the lookup table and make sure the method exists in Command.cs.
//The method signature should be void MethodName(List<Token> tokens, ComContext context) and the method should
//be public.  The command name is the string key in the lookup table, and the method is the value.
//The command name can be whatever you want, but it should be something that makes sense for the action it performs.
//For example, "pet" is a good command name for the Pet method, and "look" is a good command name for the Look method.
//You can also add aliases for commands by adding multiple entries with different keys that point to the same method,
//like "exit", "quit", "bye", and "leave" all point to the Exit method.

namespace RPG_Asn4
{
    // original public delegate void Action(List<Token> tokens);
    public delegate void Action(List<Token> tokens, ComContext context);
    internal class LookupTable : Dictionary<string, Action>
    {
        public LookupTable()
        {
            Command c = new Command();
            Add("ask", c.Ask);
            Add("talk", c.Talk);
            Add("pet", c.Pet);
            Add("slap", c.Slap);
            Add("hit", c.Slap);
            Add("look", c.Look);
            Add("help", c.Help);
            Add("exit", c.Exit);
            Add("quit", c.Exit);
            Add("bye", c.Exit);
            Add("leave", c.Exit);
        }
    }
}
