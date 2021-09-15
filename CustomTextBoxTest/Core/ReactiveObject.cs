using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomTextBoxTest.Core
{
    public class ReactiveObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetAndRaise<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (Equals(oldValue, newValue)) return;
            oldValue = newValue;
            OnPropertyChanged(propertyName);
        }
    }
}
