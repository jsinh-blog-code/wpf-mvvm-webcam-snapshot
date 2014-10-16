namespace TakeSnapsWithWebcamUsingWpfMvvm.CmdBehavior
{
    #region Namespace

    using System;
    using System.Collections.Specialized;
    using System.Windows;

    #endregion

    /// <summary>
    /// Represents command behavior collection.
    /// </summary>
    public static class CommandBehaviorCollection
    {
        /// <summary>
        /// Represents dependency property key for behaviors read-only dependency property.
        /// </summary>
        public static readonly DependencyPropertyKey BehaviorsPropertyKey = DependencyProperty.RegisterAttachedReadOnly("BehaviorsInternal", typeof(BehaviorBindingCollection), typeof(CommandBehaviorCollection), new FrameworkPropertyMetadata((BehaviorBindingCollection)null));

        /// <summary>
        /// Represents behavior dependency property.
        /// </summary>
        // ReSharper disable StaticFieldInitializersReferesToFieldBelow
        public static readonly DependencyProperty BehaviorsProperty = BehaviorsPropertyKey.DependencyProperty;
        // ReSharper restore StaticFieldInitializersReferesToFieldBelow

        /// <summary>
        /// Gets behaviors property.  
        /// </summary>
        /// <param name="dependencyObject">Instance of dependency object.</param>
        /// <returns>Returns behavior binding collection.</returns>
        // ReSharper disable ReturnTypeCanBeEnumerable.Global
        public static BehaviorBindingCollection GetBehaviors(DependencyObject dependencyObject)
        // ReSharper restore ReturnTypeCanBeEnumerable.Global
        {
            if (null == dependencyObject)
            {
                throw new InvalidOperationException("Dependency object trying to attach to is set to null");
            }

            var behaviorBindingCollection = dependencyObject.GetValue(BehaviorsProperty) as BehaviorBindingCollection;
            if (null == behaviorBindingCollection)
            {
                // ReSharper disable UseObjectOrCollectionInitializer
                behaviorBindingCollection = new BehaviorBindingCollection();
                // ReSharper restore UseObjectOrCollectionInitializer
                behaviorBindingCollection.Owner = dependencyObject;
                SetBehaviors(dependencyObject, behaviorBindingCollection);
            }

            return behaviorBindingCollection;
        }

        /// <summary>
        /// Provides way to set behavior property.
        /// </summary>
        /// <param name="dependencyObject">Instance of dependency object.</param>
        /// <param name="behaviorBindingCollection">Behavior binding collection.</param>
        private static void SetBehaviors(DependencyObject dependencyObject, BehaviorBindingCollection behaviorBindingCollection)
        {
            if (null == dependencyObject || null == behaviorBindingCollection)
            {
                return;
            }

            dependencyObject.SetValue(BehaviorsPropertyKey, behaviorBindingCollection);
            INotifyCollectionChanged behaviorBindingCollectionCasted = behaviorBindingCollection;
            behaviorBindingCollectionCasted.CollectionChanged += OnCollectionChanged;
        }

        /// <summary>
        /// Event handler for behavior binding collection on changed event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private static void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            var sourceCollection = (BehaviorBindingCollection)sender;
            if (null == sourceCollection)
            {
                return;
            }

            switch (eventArgs.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (eventArgs.NewItems != null)
                    {
                        foreach (BehaviorBinding item in eventArgs.NewItems)
                        {
                            item.Owner = sourceCollection.Owner;
                        }
                    }

                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (eventArgs.OldItems != null)
                    {
                        foreach (BehaviorBinding item in eventArgs.OldItems)
                        {
                            if (item.Behavior != null)
                            {
                                item.Behavior.Dispose();
                            }
                        }
                    }

                    break;

                case NotifyCollectionChangedAction.Replace:
                    if (eventArgs.NewItems != null)
                    {
                        foreach (BehaviorBinding item in eventArgs.NewItems)
                        {
                            item.Owner = sourceCollection.Owner;
                        }
                    }

                    if (eventArgs.OldItems != null)
                    {
                        foreach (BehaviorBinding item in eventArgs.OldItems)
                        {
                            item.Behavior.Dispose();
                        }
                    }

                    break;

                case NotifyCollectionChangedAction.Reset:
                    if (eventArgs.OldItems != null)
                    {
                        foreach (BehaviorBinding item in eventArgs.OldItems)
                        {
                            item.Behavior.Dispose();
                        }
                    }

                    break;
            }
        }
    }
}
