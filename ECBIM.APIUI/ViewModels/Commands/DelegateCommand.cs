using System;

namespace ECBIM.APIUI
{
    public class DelegateCommand : DelegateCommandBase
    {
        /// <summary>
        /// Creates a new instance of DelegateCommand with the Action to invoke on execution.
        /// </summary>
        /// <param name="executeMethod">The Action to invoke when ICommand.Execute is called.</param>
        public DelegateCommand(Action executeMethod) : this(executeMethod, () => true)
        {
        }

        /// <summary>
        /// Creates a new instance of DelegateCommand with the Action to invoke on execution
        /// and a Func to query for determining if the command can execute.
        /// </summary>
        /// <param name="executeMethod">The Action to invoke when ICommand.Execute is called.</param>
        /// <param name="canExecuteMethod">The Func{TResult} to invoke when ICommand.CanExecute is called</param>
        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod) : base((o) => executeMethod(), (o) => canExecuteMethod())
        {
            if (executeMethod == null || canExecuteMethod == null)
            {
                throw new ArgumentNullException();
            }
        }

        ///<summary>
        /// Executes the command.
        ///</summary>
        public void Execute()
        {
            Execute(null);
        }

        /// <summary>
        /// Determines if the command can be executed.
        /// </summary>
        /// <returns>Returns true if the command can execute,otherwise returns false.</returns>
        public bool CanExecute()
        {
            return CanExecute(null);
        }
    }
}
