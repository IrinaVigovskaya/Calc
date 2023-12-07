using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;

namespace Calc
{
    public struct Result_operarion
    {
        public string Error;
        public string Result;
    }
    public class MainViewModel
    {
        public MainViewModel()
        {

            AddExampleCommand = new RelayCommand<string>((x) => AddElementToExample(x));
            DeleteOneExampleCommand = new RelayCommand(DeleteOneExample, () => string.IsNullOrWhiteSpace(Example) == false);
            DeleteExampleCommand = new RelayCommand(DeleteExample, () => string.IsNullOrWhiteSpace(Example) == false);
            SolutionExampleCommand = new RelayCommand(SolutionExample, () => string.IsNullOrWhiteSpace(Example) == false);
            ClearHistoryCommand = new RelayCommand(ClearHistory, () => string.IsNullOrWhiteSpace(History) == false);
        }

        private string _example;
        private string _history;
        private string _error;
        public virtual RelayCommand<string> AddExampleCommand { get; }
        public virtual RelayCommand DeleteOneExampleCommand { get; }
        public virtual RelayCommand DeleteExampleCommand { get; }
        public virtual RelayCommand SolutionExampleCommand { get; }
        public virtual RelayCommand ClearHistoryCommand { get; }

        private readonly Calcul _primer = new Calcul();


        Result_operarion ans = new Result_operarion();

        public string Example
        {
            get => _example;
            set
            {
                if (_example == value) return;
                _example = value;

                OnPropertyChanged(Example);
            }
        }

        public string History
        {
            get => _history;
            set
            {
                if (_history == value) return;
                _history = value;
                OnPropertyChanged(History);
            }
        }

        interface IExample
        {
            string History_ { get; }
        }

        public string Error
        {
            get => _error;
            set
            {
                if (_error == value) return;
                _error = value;
                OnPropertyChanged(Error);
            }
        }

        private void AddElementToExample(string element)
        {
            Error = null;
            if (!string.IsNullOrEmpty(Example))
            {
                if (element == "+" || element == "-" || element == "*" || element == "/")
                {
                    char lastChar = Example[Example.Length - 1];
                    if (lastChar == '+' || lastChar == '-' || lastChar == '*' || lastChar == '/')
                    {
                        Example = Example.Remove(Example.Length - 1);
                    }
                }
            }
                Example += element;
        }

        private void DeleteOneExample()
        {
            if (Example.Length > 0)
            {
                Error = null;
                Example = Example.Remove(1);
            }
        }

        private void DeleteExample()
        {
            Error = null;
            Example = null;
        }

        private void SolutionExample()
        {
            ans.Result = _primer.CalculWork(Example, ref ans).ToString();
            Example = null;
            if (ans.Error == null)
            {
                History += Example + "=" + ans.Result + "\n";
                Example = ans.Result;
            }
            else
            {
                Error = ans.Error;
            }
        }

        private void ClearHistory()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}