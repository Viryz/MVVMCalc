using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCalc.Models
{
    public enum Operation
    {
        Add,
        Sub,
        Div,
        Product
    }

    public class CalcModel : INotifyPropertyChanged
    {
        private string line;
        public string Line
        {
            get { return line; }
            set
            {
                line = value;
                OnPropertyChanged("Line");
            }
        }

        private string result;
        public string Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged("Result");
            }
        }

        private Operation? operation;
        public Operation? Operation
        {
            get { return operation; }
            set
            {
                operation = value;
                OnPropertyChanged("Operation");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
