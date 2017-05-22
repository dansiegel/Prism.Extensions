using System;
using System.Threading.Tasks;

namespace Prism.Commands
{
    /// <summary>
    /// Delegate async command.
    /// </summary>
    public class DelegateAsyncCommand : DelegateCommandBase
    {
        Func<Task> _commandTask { get; }
        Func<bool> _canExecute { get; }
        bool _allowMultipleExecution { get; }
        Action<Exception> _exceptionHandler { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AP.MobileToolkit.Commands.DelegateAsyncCommand"/> class.
        /// </summary>
        /// <param name="commandTask">Command task.</param>
        /// <param name="canExecute">Can execute.</param>
        /// <param name="allowMultipleExecution">If set to <c>true</c> allow multiple execution.</param>
        /// <param name="exceptionHandler">Exception handler.</param>
        public DelegateAsyncCommand(Func<Task> commandTask, Func<bool> canExecute = null, bool allowMultipleExecution = false,
                                    Action<Exception> exceptionHandler = null)
        {
            _commandTask = commandTask;
            _canExecute = canExecute;
            _allowMultipleExecution = allowMultipleExecution;
            _exceptionHandler = exceptionHandler;
        }

        private bool IsExecuting { get; set; }

        private bool CanExecuteAgain() =>
            _allowMultipleExecution ?
                true :
                !IsExecuting;

        private async Task ExecuteAsync() =>
            await _commandTask();

        /// <inheritDoc />
        protected override bool CanExecute(object parameter) =>
            _canExecute?.Invoke() ?? true
                        && CanExecuteAgain();

        /// <inheritDoc />
        protected override void Execute(object parameter)
        {
            // Sanity Check
            if(!_allowMultipleExecution && IsExecuting) return;
            IsExecuting = true;

            try
            {
                ExecuteAsync().ContinueWith((o) => { });
            }
            catch(Exception ex)
            {
                _exceptionHandler?.Invoke(ex);
            }
            finally
            {
                IsExecuting = false;
                RaiseCanExecuteChanged();
            }
        }
    }
}
