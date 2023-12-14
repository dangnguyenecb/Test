using System;
using System.Windows.Input;

namespace ECBIM.APIUI
{
    public abstract class DelegateCommandBase : ICommand, IActiveAware
    {
        private readonly Action<object> executeMethod;
        private readonly Func<object, bool> canExecuteMethod;
        private bool _isActive;

        /// <summary>
        /// Createse a new instance of a DelegateCommandBase, specifying both the execute action and the can execute function.
        /// </summary>
        /// <param name="executeMethod">The Action to execute when ICommand.Executeis invoked.</param>
        /// <param name="canExecuteMethod">The Func{Object,Bool} to invoked when ICommand.CanExecute is invoked.</param>
        protected DelegateCommandBase(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
        {
            if (executeMethod == null || canExecuteMethod == null)
            {
                throw new ArgumentNullException();
            }

            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the object is active.
        /// </summary>
        /// <value>True if the object is active; otherwise False.</value>
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnIsActiveChanged();
                }
            }
        }

        /// <summary>
        /// Raises ICommand.CanExecuteChanged on the UI thread so every command invoker 
        /// can requery ICommand.CanExecute to check if the CompositeCommand can execute.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            var handlers = CanExecuteChanged;
            if (handlers != null)
            {
                handlers(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises DelegateCommandBase.CanExecuteChanged on the UI thread so every command invoker
        /// can requery to check if the command can execute.
        /// <remarks>Note that this will trigger the execution of DelegateCommandBase.CanExecute once for each invoker.</remarks>
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        /// <summary>
        /// Fired if the IsActive property changes.
        /// </summary>
        public virtual event EventHandler IsActiveChanged;

        /// <summary>
        /// This raises the DelegateCommandBase.IsActiveChanged event.
        /// </summary>
        protected virtual void OnIsActiveChanged()
        {
            EventHandler isActiveChangedHandler = IsActiveChanged;
            if (isActiveChangedHandler != null) isActiveChangedHandler(this, EventArgs.Empty);
        }

        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        /// <summary>
        /// Executes the command with the provided parameter by invoking the Action{Object} supplied during construction.
        /// </summary>
        /// <param name="parameter"></param>
        protected void Execute(object parameter)
        {
            executeMethod(parameter);
        }

        /// <summary>
        /// Determines if the command can execute with the provided parameter by invoing the Func{Object,Bool} supplied during construction.
        /// </summary>
        /// <param name="parameter">The parameter to use when determining if this command can execute.</param>
        /// <returns>Returns true if the command can execute. False otherwise.</returns>
        protected bool CanExecute(object parameter)
        {
            return canExecuteMethod == null || canExecuteMethod(parameter);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;
    }
}

