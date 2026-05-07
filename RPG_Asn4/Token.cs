using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Asn4
{
    public enum TokenType
    {
        verb,
        subject
    }

    public class Token
    {
        public TokenType Name;
        public string Value;

        public Token(TokenType name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
