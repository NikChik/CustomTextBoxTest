using CustomTextBoxTest.Core;
using CustomTextBoxTest.Utilities.Converters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomTextBoxTest.ViewModels.Windows
{
    public class MainWindowViewModel : ReactiveObject
    {
        private long _frequency;
        public long Frequency
        {
            get => _frequency;
            set => SetAndRaise(ref _frequency, value);
        }

        private IConverter _frequencyConverter;
        public IConverter FrequencyConverter
        {
            get => _frequencyConverter;
            set => SetAndRaise(ref _frequencyConverter, value);
        }
    }
}
