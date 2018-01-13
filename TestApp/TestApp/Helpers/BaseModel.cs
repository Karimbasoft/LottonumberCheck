using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TestApp.Services;

namespace TestApp.Helpers
{
    public class BaseModel : ObservableObject, IMessage, INotifyPropertyChanged
    {
        private string _id;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Id
        {
            get { return _id; }
            set
            {
                if (_id == value) return;
                _id = value;
                NotifyPropertyChanged();
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (SetProperty(ref _name, value, "Name"))
                {
                    NotifyPropertyChanged();
                }
            }
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
