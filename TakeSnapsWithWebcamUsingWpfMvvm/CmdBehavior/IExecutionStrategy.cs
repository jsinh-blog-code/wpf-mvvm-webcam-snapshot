namespace TakeSnapsWithWebcamUsingWpfMvvm.CmdBehavior
{
    /// <summary>
    /// Represents contract for a strategy of execution for the CommandBehaviorBinding.
    /// </summary>
    public interface IExecutionStrategy
    {
        /// <summary>
        /// Gets or sets the Behavior that we execute this strategy
        /// </summary>
        CommandBehaviorBinding Behavior
        {
            get;
            set;
        }

        /// <summary>
        /// Executes according to the strategy type
        /// </summary>
        /// <param name="parameter">The parameter to be used in the execution</param>
        void Execute(object parameter);
    }
}
