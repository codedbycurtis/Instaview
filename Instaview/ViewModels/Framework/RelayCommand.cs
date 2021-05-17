﻿using System;
using System.Windows.Input;

namespace Instaview.ViewModels.Framework
{
    /// <summary>
    /// A simple command that performs an action.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private Members

        /// <summary>
        /// The action to perform.
        /// </summary>
        private Action _action;

        #endregion

        #region Public Members

        /// <summary>
        /// Invoked when the <see cref="CanExecute(object)"/> property changes.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Whether or not the command can execute.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>True; A RelayCommand can always execute.</returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Executes the <see cref="_action"/>
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) => _action();

        /// <summary>
        /// Initializes a new instance of <see cref="RelayCommand"/> with the specified <paramref name="action"/>.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public RelayCommand(Action action) => _action = action;

        #endregion
    }

    /// <summary>
    /// A simple command that performs an action with a given parameter.
    /// </summary>
    public class RelayCommand<T> : ICommand
    {
        #region Private Members

        /// <summary>
        /// The action to perform.
        /// </summary>
        private Action<T> _action;

        #endregion

        #region Public Members

        /// <summary>
        /// Invoked when the <see cref="CanExecute(object)"/> property changes.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Whether or not the command can execute.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>True; A RelayCommand can always execute.</returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Executes the <see cref="_action"/>
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) => _action((T)parameter);

        /// <summary>
        /// Initializes a new instance of <see cref="RelayCommand{T}"/> with the specified <paramref name="action"/>.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public RelayCommand(Action<T> action) => _action = action;

        #endregion
    }
}
