namespace TakeSnapsWithWebcamUsingWpfMvvm.CmdBehavior
{
    #region Namespace

    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Input;

    #endregion

    /// <summary>
    /// Defines the command behavior binding
    /// </summary>
    public class CommandBehaviorBinding : IDisposable
    {
        #region Variable declaration

        /// <summary>
        /// Stores the strategy of how to execute the event handler.
        /// </summary>
        private IExecutionStrategy strategy;

        /// <summary>
        /// Instance of ICommand.
        /// </summary>
        private ICommand command;

        /// <summary>
        /// Instance of action.
        /// </summary>
        private Action<object> action;

        /// <summary>
        /// Indicates a value whether the object is disposed once or not.
        /// </summary>
        private bool disposed;

        #endregion

        #region Constructor

        /// <summary>
        /// Finalizes an instance of the <see cref="CommandBehaviorBinding"/> class. 
        /// </summary>
        ~CommandBehaviorBinding()
        {
            this.Dispose(false);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets owner of the command binding.
        /// E.g.: Button control. This property can only be send from the bind event method.
        /// This property can only be set from the BindEvent Method
        /// </summary>
        public DependencyObject Owner
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets event name to hook up with, this property can only be set from the bind event method.
        /// </summary>
        public string EventName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets information of the event.
        /// </summary>
        public EventInfo Event
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets event handler for the binding with the event.
        /// </summary>
        public Delegate EventHandler
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a CommandParameter
        /// </summary>
        public object CommandParameter
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets command to execute when the specified event is raised.
        /// </summary>
        public ICommand Command
        {
            get
            {
                return this.command;
            }

            set
            {
                this.command = value;
                this.strategy = new CommandExecutionStrategy { Behavior = this };
            }
        }

        /// <summary>
        /// Gets or sets action instance.
        /// </summary>
        public Action<object> Action
        {
            get
            {
                return this.action;
            }

            set
            {
                this.action = value;
                this.strategy = new ActionExecutionStrategy { Behavior = this };
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Dispose all used resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Creates an event handler on runtime and registers handler to event specified.
        /// </summary>
        /// <param name="owner">Instance of dependency object.</param>
        /// <param name="eventName">Event name.</param>
        public void BindEvent(DependencyObject owner, string eventName)
        {
            this.EventName = eventName;
            this.Owner = owner;
            this.Event = this.Owner.GetType().GetEvent(this.EventName, BindingFlags.Public | BindingFlags.Instance);
            if (null == this.Event)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Could not resolve event name {0}", this.EventName));
            }

            this.EventHandler = EventHandlerGenerator.CreateDelegate(this.Event.EventHandlerType, typeof(CommandBehaviorBinding).GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance), this);
            this.Event.AddEventHandler(this.Owner, this.EventHandler);
        }

        /// <summary>
        /// Executes the strategy
        /// </summary>
        public void Execute()
        {
            this.strategy.Execute(this.CommandParameter);
        }

        /// <summary>
        /// Dispose all used resources.
        /// </summary>
        /// <param name="disposing">Indicates the source call to dispose.</param>
        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.Event.RemoveEventHandler(this.Owner, this.EventHandler);
            }

            this.disposed = true;
        }

        #endregion
    }
}
