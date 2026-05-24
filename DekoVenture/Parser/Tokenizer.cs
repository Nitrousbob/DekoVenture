namespace DekoVenture
{
    internal class Tokenizer
    {
        public List<Token>? Tokenize(string s)
        {
            List<Token> list = new List<Token>();

            var parts = s.Split(" ");

            //takes the first token a makes it a verb to lookup
            list.Add(new Token(TokenType.verb, parts[0]));

            //this takes the remaining words and adds them to the list as either a subject or an object depending on what comes before it
            for (int i = 1; i < parts.Length; i++)
            {
                if (parts[i] == "to" || parts[i] == "with" || parts[i] == "about" || parts[i] == "at")
                {
                    list.Add(new Token(TokenType.preposition, parts[i]));
                }
                else if (i > 0 && (parts[i - 1] == "to" || parts[i - 1] == "with" || parts[i - 1] == "about" || parts[i - 1] == "at"))
                {
                    list.Add(new Token(TokenType.target, parts[i]));
                }
                else
                {
                    list.Add(new Token(TokenType.subject, parts[i]));
                }
            }
            return list.Count > 0 ? list : null;
        }
    }
}
