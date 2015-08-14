namespace TakeSnapsWithWebcamUsingWpfMvvm.CmdBehavior
{
    #region Namespace

    using System;

    #endregion

    /// <summary>
    /// Provides implementation for execution strategy.
    /// </summary>
    public class CommandExecutionStrategy : IExecutionStrategy
    {
        /// <summary>
        /// Gets or sets the Behavior that we execute this strategy
        /// </summary>
        public CommandBehaviorBinding Behavior { get; set; }

        /// <summary>
        /// Executes the Command that is stored in the CommandProperty of the CommandExecution
        /// </summary>
        /// <param name="parameter">The parameter for the command</param>
        public void Execute(object parameter)
        {
            if (null == this.Behavior)
            {
                throw new InvalidOperationException("Behavior property cannot be null when executing a strategy");
            }

            if (this.Behavior.Command.CanExecute(this.Behavior.CommandParameter))
            {
                this.Behavior.Command.Execute(this.Behavior.CommandParameter);
            }
        }
    }
}
