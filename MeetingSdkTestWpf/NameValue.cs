using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MeetingSdkTestWpf
{
    public class NameValue : PropertyChangedBase
    {
        public NameValue() { }
        public NameValue(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                this.NotifyOfPropertyChange(()=>this.Name);
            }
        }

        private string _value;

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                this.NotifyOfPropertyChange(()=>this.Value);
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                this.NotifyOfPropertyChange(()=>this.IsSelected);
            }
        }
    }
}
