namespace TakeSnapsWithWebcamUsingWpfMvvm.CmdBehavior
{
    #region Namespace

    using System;
    using System.Windows;
    using System.Windows.Input;
    #endregion
    /// <summary>
    /// Defines a Command Binding
    /// This inherits from freezable so that it gets inheritance context for DataBinding to work
    /// </summary>
    public class BehaviorBinding : Freezable
    {
        #region Variable declaration

        /// <summary>
        /// Represents command dependency property.
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(BehaviorBinding), new FrameworkPropertyMetadata(OnCommandChanged));

        /// <summary>
        /// Represents event dependency property.
        /// </summary>
        public static readonly DependencyProperty EventProperty = DependencyProperty.Register("Event", typeof(string), typeof(BehaviorBinding), new FrameworkPropertyMetadata(OnEventChanged));

        /// <summary>
        /// Represents action dependency property.
        /// </summary>
        public static readonly DependencyProperty ActionProperty = DependencyProperty.Register("Action", typeof(Action<object>), typeof(BehaviorBinding), new FrameworkPropertyMetadata(OnActionChanged));

        /// <summary>
        /// Represents command parameter dependency property.
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(BehaviorBinding), new FrameworkPropertyMetadata(OnCommandParameterChanged));

        /// <summary>
        /// Instance of command behavior binding.
        /// </summary>
        private CommandBehaviorBinding commandBehaviorBinding;

        /// <summary>
        /// Instance of dependency object.
        /// </summary>
        private DependencyObject ownerDependencyObject;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the owner object of the binding.
        /// </summary>
        public DependencyObject Owner
        {
            get
            {
                return this.ownerDependencyObject;
            }

            set
            {
                this.ownerDependencyObject = value;
                this.ResetEventBinding();
            }
        }

        /// <summary>
        /// Gets or sets command property.  
        /// </summary>
        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }

            set
            {
                this.SetValue(CommandProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets event property.  
        /// </summary>
        public string Event
        {
            get
            {
                return (string)GetValue(EventProperty);
            }

            set
            {
                this.SetValue(EventProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets action property. 
        /// </summary>
        public Action<object> Action
        {
            get
            {
                return (Action<object>)GetValue(ActionProperty);
            }

            set
            {
                this.SetValue(ActionProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets command parameter property.  
        /// </summary>
        public object CommandParameter
        {
            get
            {
                return this.GetValue(CommandParameterProperty);
            }

            set
            {
                this.SetValue(CommandParameterProperty, value);
            }
        }

        /// <summary>
        /// Gets command behavior binding.
        /// </summary>
        internal CommandBehaviorBinding Behavior
        {
            get
            {
                return this.commandBehaviorBinding ?? (this.commandBehaviorBinding = new CommandBehaviorBinding());
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the Command property.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected virtual void OnCommandChanged(DependencyPropertyChangedEventArgs eventArgs)
        {
            this.Behavior.Command = this.Command;
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the Event property.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected virtual void OnEventChanged(DependencyPropertyChangedEventArgs eventArgs)
        {
            this.ResetEventBinding();
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the Action property.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected virtual void OnActionChanged(DependencyPropertyChangedEventArgs eventArgs)
        {
            this.Behavior.Action = this.Action;
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the CommandParameter property.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected virtual void OnCommandParameterChanged(DependencyPropertyChangedEventArgs eventArgs)
        {
            this.Behavior.CommandParameter = this.CommandParameter;
        }

        /// <summary>
        /// Create instance core.
        /// </summary>
        /// <returns>Returns instance of freezable object.</returns>
        /// <remarks>
        /// This is not actually used but used so that this object gets WPF Inheritance Context
        /// </remarks>
        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Event handler for command changed event.
        /// </summary>
        /// <param name="dependencyObject">Instance of dependency object.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private static void OnCommandChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            ((BehaviorBinding)dependencyObject).OnCommandChanged(eventArgs);
        }

        /// <summary>
        /// Event handler for changes to the Event property event.
        /// </summary>
        /// <param name="dependencyObject">Dependency object.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private static void OnEventChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            ((BehaviorBinding)dependencyObject).OnEventChanged(eventArgs);
        }

        /// <summary>
        /// Event handler for changes to the action property event.
        /// </summary>
        /// <param name="dependencyObject">Dependency object.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private static void OnActionChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            ((BehaviorBinding)dependencyObject).OnActionChanged(eventArgs);
        }

        /// <summary>
        /// Event handler for changes to the command parameter property event.
        /// </summary>
        /// <param name="dependencyObject">Dependency object.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private static void OnCommandParameterChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            ((BehaviorBinding)dependencyObject).OnCommandParameterChanged(eventArgs);
        }

        /////// <summary>
        /////// Reset owner event binding.
        /////// </summary>
        /////// <param name="dependencyObject">Dependency object.</param>
        /////// <param name="eventArgs">Event arguments.</param>
        ////private static void OwnerReset(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        ////{
        ////    ((BehaviorBinding)dependencyObject).ResetEventBinding();
        ////}

        /// <summary>
        /// Reset event binding.
        /// </summary>
        private void ResetEventBinding()
        {
            if (this.Owner == null)
            {
                return;
            }

            if (this.Behavior.Event != null && this.Behavior.Owner != null)
            {
                this.Behavior.Dispose();
            }

            this.Behavior.BindEvent(this.Owner, this.Event);
        }

        #endregion
    }
}
