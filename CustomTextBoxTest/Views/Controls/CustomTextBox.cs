using CustomTextBoxTest.Utilities.Converters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomTextBoxTest.Views.Controls
{
    public class CustomTextBox : TextBox
    {
        public static readonly DependencyProperty ConverterProperty = DependencyProperty.Register(
            nameof(Converter), typeof(IConverter), typeof(CustomTextBox), new PropertyMetadata(default));

        public IConverter Converter
        {
            get => (IConverter)GetValue(ConverterProperty);
            set => SetValue(ConverterProperty, value);
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(object), typeof(CustomTextBox), new FrameworkPropertyMetadata(
                default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnValueChanged)));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomTextBox customTextBox)
                customTextBox.ConvertValue();
        }

        public object Value
        {
            get => (object)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        static CustomTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTextBox), new FrameworkPropertyMetadata(typeof(CustomTextBox)));
        }

        public CustomTextBox()
        {
            this.KeyUp += OnKeyUp;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ParseValue();
        }

        private void ParseValue()
        {
            if (Converter is null)
                return;

            var parsedValue = Converter.ParseFromString(Text);

            if (parsedValue is null)
            {
                MessageBox.Show("Некорректный ввод.");
                return;
            }

            Value = parsedValue;
        }

        private void ConvertValue()
        {
            if (Converter is null)
            {
                Text = Value.ToString();
                return;
            }

            var convertedValue = Converter.ConvertToString(Value);

            if (convertedValue is null)
            {
                MessageBox.Show("Не удалось преобразовать значение.");
                return;
            }

            Text = convertedValue;
        }
    }
}
