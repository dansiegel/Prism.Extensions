using System;
using System.Threading.Tasks;

namespace Prism.Commands
{
    /// <summary>
    /// Delegate async command.
    /// </summary>
    public class DelegateAsyncCommand<T> : DelegateCommandBase
    {
        Func<T, Task> _commandTask { get; }
        Func<T, bool> _canExecute { get; }
        bool _allowMultipleExecution { get; }
        Action<Exception> _exceptionHandler { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AP.MobileToolkit.Commands.DelegateAsyncCommand`1"/> class.
        /// </summary>
        /// <param name="commandTask">Command task.</param>
        /// <param name="canExecute">Can execute.</param>
        /// <param name="allowMultipleExecution">If set to <c>true</c> allow multiple execution.</param>
        /// <param name="exceptionHandler">Exception handler.</param>
        public DelegateAsyncCommand(Func<T, Task> commandTask, Func<T, bool> canExecute = null, bool allowMultipleExecution = false,
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

        /// <summary>
        /// Cans the execute.
        /// </summary>
        /// <returns><c>true</c>, if execute was caned, <c>false</c> otherwise.</returns>
        /// <param name="parameter">Parameter.</param>
        public bool CanExecute(T parameter) =>
        _canExecute?.Invoke(parameter) ?? true
                    && CanExecuteAgain();

        /// <summary>
        /// Executes the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="parameter">Parameter.</param>
        public async Task ExecuteAsync(T parameter) =>
            await _commandTask.Invoke(parameter);

        /// <inheritDoc />
        protected override bool CanExecute(object parameter) =>
            CanExecute((T)parameter);

        /// <inheritDoc />
        protected override void Execute(object parameter)
        {
            // Sanity Check
            if(!_allowMultipleExecution && IsExecuting) return;
            IsExecuting = true;

            try
            {
                ExecuteAsync((T)parameter).ContinueWith((p) => { });
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
