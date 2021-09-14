using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomTextBoxTest.Utilities.Converters.Interfaces
{
    public interface IConverter
    {
        object ParseFromString(string value);
        string ConvertToString(object value);
    }
}
