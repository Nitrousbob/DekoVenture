using System;
using System.Collections.Generic;
using System.Text;

namespace DekoVenture
{
    public interface IPettable
    {
        string Name { get; }
        string GetPetResponse();
    }
}
