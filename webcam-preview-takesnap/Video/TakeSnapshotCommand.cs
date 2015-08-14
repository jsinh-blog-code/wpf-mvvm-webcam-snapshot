namespace TakeSnapsWithWebcamUsingWpfMvvm.Video
{
    #region Namespace

    using System;
    using System.Windows.Input;
    
    #endregion

    /// <summary>
    /// Represents take snapshot command for video source device.
    /// </summary>
    public class TakeSnapshotCommand : ICommand
    {
        #region Variable declaration

        /// <summary>
        /// Instance of take snapshot action.
        /// </summary>
        private readonly Action takeSnapshotAction;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TakeSnapshotCommand"/> class.
        /// </summary>
        /// <param name="takeSnapshotAction">Instance of action that takes snapshot.</param>
        public TakeSnapshotCommand(Action takeSnapshotAction)
        {
            this.takeSnapshotAction = takeSnapshotAction;
        }

        #endregion

        #region Events

        /// <summary>
        /// Event handler for can execute changed event.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes and evaluates if take snapshot command is executable or not.
        /// </summary>
        /// <param name="parameter">Input parameter, in case no parameter then pass null.</param>
        /// <returns>Returns a value indicating whether take snapshot command is executable or not.</returns>
        public bool CanExecute(object parameter)
        {
            return null != this.takeSnapshotAction;
        }

        /// <summary>
        /// Execute take snapshot command.
        /// </summary>
        /// <param name="parameter">Input parameter, in case no parameter then pass null.</param>
        public void Execute(object parameter)
        {
            if (null != this.takeSnapshotAction)
            {
                this.takeSnapshotAction();
            }
        }

        #endregion
    }
}