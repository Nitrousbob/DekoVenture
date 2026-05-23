using System;
using System.Collections.Generic;
using System.Text;

namespace DekoVenture
{
    public enum TokenType
    {
        verb,
        subject,
        preposition,
        target
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
