using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomTextBoxTest.Utilities.Converters.Interfaces
{
    public interface IConverter<T>
    {
        T ParseFromString(string value);
        string ConvertToString(T value);
    }
}
