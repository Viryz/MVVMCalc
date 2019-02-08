using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCalc.ViewModels
{
    public class CalcViewModel : INotifyPropertyChanged
    {
        public Models.CalcModel Calc { get; set; }

        public CalcViewModel()
        {
            Calc = new Models.CalcModel { Line = "dwadwadaw", Operation = null };
            Calc.PropertyChanged += Calc_PropertyChanged;
        }

        private Commands.RelayCommand inputNumberCommand;
        public Commands.RelayCommand InputNumberCommand
        {
            get
            {
                return inputNumberCommand ?? (inputNumberCommand = new Commands.RelayCommand(obj =>
                {
                    string str = obj as string;
                    if (str != null)
                    {
                        Calc.Line += str;
                    }
                }));
            }
        }

        private Commands.RelayCommand cancelCommand;
        public Commands.RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new Commands.RelayCommand(obj =>
                {
                    Calc.Line = "";
                    Calc.Operation = null;
                }
                ,(obj) => Calc.Line != "" ));
            }
        }

        private Commands.RelayCommand addOperationCommand;
        public Commands.RelayCommand AddOperationCommand
        {
            get
            {
                return addOperationCommand ?? (addOperationCommand = new Commands.RelayCommand(obj =>
                {
                    Models.Operation? operation = obj as Models.Operation?;
                    if (operation != null)
                    {
                        Calc.Operation = operation;
                        Calc.Result = Calc.Line;
                        Calc.Line = "";
                    }
                }, obj => Calc.Operation == null && Calc.Line != ""));
            }
        }

        private Commands.RelayCommand getResultCommand;
        public Commands.RelayCommand GetResultCommand
        {
            get
            {
                return getResultCommand ?? (getResultCommand = new Commands.RelayCommand(obj =>
                {
                    float result = Convert.ToSingle(Calc.Result);
                    float operand = Convert.ToSingle(Calc.Line);
                    switch (Calc.Operation)
                    {
                        case Models.Operation.Add:
                            result += operand;
                            break;

                        case Models.Operation.Sub:
                            result -= operand;
                            break;

                        case Models.Operation.Product:
                            result *= operand;
                            break;

                        case Models.Operation.Div:
                            result /= operand;
                            break;

                        default:
                            break;
                    }
                    Calc.Line = result.ToString();
                    Calc.Operation = null;
                }, obj => Calc.Operation != null && Calc.Line != ""));
            }
        }

        private Commands.RelayCommand inputDotCommand;
        public Commands.RelayCommand InputDotCommand
        {
            get
            {
                return inputDotCommand ?? (inputDotCommand = new Commands.RelayCommand(obj =>
                {
                    inputNumberCommand.Execute(",");
                }, obj => !Calc.Line.Contains(".") && Calc.Line != ""));
            }
        }

        private void Calc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
