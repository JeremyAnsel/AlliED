using System.Windows.Input;

namespace AlliED.Controls;

internal sealed class CustomCommand : ICommand
{
    private readonly Action<object> _executeAction;
    private readonly Func<object, bool> _canExecuteAction;

    public CustomCommand(Action<object> executeAction, Func<object, bool> canExecute)
    {
        _executeAction = executeAction;
        _canExecuteAction = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return _canExecuteAction(parameter);
    }

    public void Execute(object parameter)
    {
        _executeAction(parameter);
    }

    public static MouseBinding CreateMouseBinding(object parameter, MouseAction mouseAction, Action<object> execute, Func<object, bool> canExecute)
    {
        var binding = new MouseBinding
        {
            MouseAction = mouseAction,
            Command = new CustomCommand(execute, canExecute),
            CommandParameter = parameter
        };
        return binding;
    }
}
