using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Asn4
{
    public delegate void Action(List<Token> tokens);
    internal class LookupTable : Dictionary<string, Action>
    {
        public LookupTable()
        {
            Command c = new Command();
            Add("pet", c.Pet);
            Add("look", c.Look);
            Add("help", c.Help);
            Add("exit", c.Exit);
            Add("quit", c.Exit);
            Add("bye", c.Exit);
        }
    }
}
