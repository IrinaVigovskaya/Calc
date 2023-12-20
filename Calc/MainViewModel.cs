﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;
using System.Data.SQLite;

namespace Calc
{

    public struct Result_operarion
    {
        public string Error;
        public string Result;
    }

    public class MainViewModel: INotifyPropertyChanged
    {
        public MainViewModel(IHistory History)
        {

            _history = History;
            AddExampleCommand = new RelayCommand<string>((x) => AddElementToExample(x));
            DeleteOneExampleCommand = new RelayCommand(DeleteOneExample);
            DeleteExampleCommand = new RelayCommand(DeleteExample);
            SolutionExampleCommand = new RelayCommand(SolutionExample);
            ClearHistoryCommand = new RelayCommand(ClearHistory);
        }


        IHistory _history;

        private string _example;
        private string _error;
        private string history_;
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

                OnPropertyChanged();
            }
        }


        public string History
        {
            get => _history.GetHistoryData();
            set
            {
                if (history_ == value) return;
                history_ = value;
                OnPropertyChanged();
            }
        }
    
        public string Error
        {
            get => _error;
            set
            {
                if (_error == value) return;
                _error = value;
                OnPropertyChanged();
            }
        }

        public void GetHistoryData()
        {
            History = _history.GetHistoryData();
        }

        private void AddElementToExample(string element)
        {
            ans.Error = null;
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
                ans.Error = null;
                Error = null;
                Example = Example.Remove(Example.Length - 1);

            }
        }

        private void ClearHistory()
        {
            History = _history.ClearHistory(History);
        }

        private void DeleteExample()
        {
            Error = null;
            ans.Error = null;
            Example = null;
        }

        private void SolutionExample()
        {
            ans.Error = null;
            ans.Result = _primer.CalculWork(Example, ref ans).ToString();
            if (ans.Error == null)
            {
                UpdateHistoryData(Example + "=" + ans.Result + "\n");
                Example = ans.Result;
            }
            else
            {
                Error = ans.Error;
            }
        }

        public void UpdateHistoryData(string data)
        {
            History = _history.UpdateHistoryData(History, data);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}