using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Asn4
{
    internal class Tokenizer
    {
        public List<Token>? Tokenize(string s)
        {
            List<Token> list = new List<Token>();

            var parts = s.Split(" ");
            list.Add(new Token(TokenType.verb, parts[0]));

            for (int i = 0; i < parts.Length; i++)
            {
                //verb, subject, object, preposition, indirect object
                list.Add(new Token(TokenType.subject, parts[i]));
            }

            return list.Count > 0 ? list : null;
        }
    }
}
