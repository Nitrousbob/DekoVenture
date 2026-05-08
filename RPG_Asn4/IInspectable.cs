using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Asn4
{
    public interface IInspectable
    {
        string Name { get; }
        string GetDescription();
    }
}
