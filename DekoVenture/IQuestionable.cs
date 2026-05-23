using System;
using System.Collections.Generic;
using System.Text;

namespace DekoVenture
{
    public interface IQuestionable
    {
        string Name { get; }
        string GetQuestionResponse();
    }
}
