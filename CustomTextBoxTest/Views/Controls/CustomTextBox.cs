using CustomTextBoxTest.Utilities.Converters.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CustomTextBoxTest.Views.Controls
{
    public class CustomTextBox : TextBox
    {
        public static readonly DependencyProperty ConverterProperty = DependencyProperty.Register(
            nameof(Converter), typeof(IConverter), typeof(CustomTextBox), new PropertyMetadata(
                default(IConverter), new PropertyChangedCallback(OnControllerChanged)));

        private static void OnControllerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomTextBox customTextBox)
                customTextBox.ConvertValue();
        }

        public IConverter Converter
        {
            get => (IConverter)GetValue(ConverterProperty);
            set => SetValue(ConverterProperty, value);
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(double), typeof(CustomTextBox), new FrameworkPropertyMetadata(
                default(double), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnValueChanged)));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomTextBox customTextBox)
                customTextBox.ConvertValue();
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
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

            Value = parsedValue.Value;
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
