namespace TakeSnapsWithWebcamUsingWpfMvvm.CmdBehavior
{
    /// <summary>
    /// Provides implementation for execution strategy.
    /// </summary>
    public class ActionExecutionStrategy : IExecutionStrategy
    {
        /// <summary>
        /// Gets or sets behavior to execute this strategy.
        /// </summary>
        public CommandBehaviorBinding Behavior
        {
            get;
            set;
        }

        /// <summary>
        /// Executes an Action delegate
        /// </summary>
        /// <param name="parameter">The parameter to pass to the Action</param>
        public void Execute(object parameter)
        {
            this.Behavior.Action(parameter);
        }
    }
}