using System;
using System.Windows.Input;

namespace Fasetto.Word
{
    public class RelayCommand : ICommand
    {
        #region Private Members
        /// <summary>
        /// Action to run
        /// </summary>
        private Action mAction;

        #endregion

        #region Public Events
        /// <summary>
        /// The event thats fired when the <see cref="CanExecute(object)"/> value is changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor
        /// <summary>
        /// Defaul Constructor
        /// </summary>
        /// <param name="action"></param>
        public RelayCommand(Action action)
        {
            mAction = action;
        }
        #endregion

        #region Command Methods
        /// <summary>
        /// A Relay Command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            mAction?.Invoke();
        }
        #endregion
    }
}
