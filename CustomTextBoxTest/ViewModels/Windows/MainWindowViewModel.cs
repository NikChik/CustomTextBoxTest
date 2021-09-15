using CustomTextBoxTest.Core;
using CustomTextBoxTest.Utilities.Converters;
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
        private double _frequency;
        public double Frequency
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

        public MainWindowViewModel()
        {
            Frequency = 1000;
            FrequencyConverter = new FrequencyConverter();
        }
    }
}
