using System;

public class MainViewModel
{
    public MainViewModel()
    {

        SomeCommand =

    }

    private string _example;

    public string Example
    {
        get => _example;
        set
        {
            if (_example == value) return;
            _example = value;

            OnPropertyChanged();
            OnPropertyChanged(nameof(FullName));
        }
    }

}
