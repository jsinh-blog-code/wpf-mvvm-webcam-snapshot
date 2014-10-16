namespace TakeSnapsWithWebcamUsingWpfMvvm.CmdBehavior
{
    #region Namespace

    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    #endregion

    /// <summary>
    /// Represents class that generates delegates according to the specified signature on runtime.
    /// </summary>
    public static class EventHandlerGenerator
    {
        /// <summary>
        /// Create <see cref="Delegate"/> with a matching signature of the supplied event handler type.
        /// Note: It supports event that have a <see cref="Delegate"/> of type void.
        /// </summary>
        /// <param name="eventHandlerType">Event handler type.</param>
        /// <param name="methodToInvoke">Method to invoke.</param>
        /// <param name="methodInvoker">Instance of method invoker.</param>
        /// <returns>Return <see cref="Delegate"/> instance.</returns>
        public static Delegate CreateDelegate(Type eventHandlerType, MethodInfo methodToInvoke, object methodInvoker)
        {
            if (null == eventHandlerType)
            {
                throw new ArgumentNullException("eventHandlerType");
            }

            if (null == methodInvoker)
            {
                throw new ArgumentNullException("methodInvoker");
            }

            var eventHandlerInfo = eventHandlerType.GetMethod("Invoke");
            if (null != eventHandlerInfo.ReturnParameter)
            {
                var returnType = eventHandlerInfo.ReturnParameter.ParameterType;
                if (returnType != typeof(void))
                {
                    throw new InvalidOperationException("Delegate has a return type. This only supports event handlers that are void");
                }
            }

            var delegateParameters = eventHandlerInfo.GetParameters();
            var hookupParameters = new Type[delegateParameters.Length + 1];
            hookupParameters[0] = methodInvoker.GetType();
            for (var counter = 0; counter < delegateParameters.Length; counter++)
            {
                hookupParameters[counter + 1] = delegateParameters[counter].ParameterType;
            }

            var handler = new DynamicMethod(string.Empty, null, hookupParameters, typeof(EventHandlerGenerator));
            var eventIl = handler.GetILGenerator();
            var local = eventIl.DeclareLocal(typeof(object[]));
            eventIl.Emit(OpCodes.Ldc_I4, delegateParameters.Length + 1);
            eventIl.Emit(OpCodes.Newarr, typeof(object));
            eventIl.Emit(OpCodes.Stloc, local);
            for (var counter = 1; counter < delegateParameters.Length + 1; counter++)
            {
                eventIl.Emit(OpCodes.Ldloc, local);
                eventIl.Emit(OpCodes.Ldc_I4, counter);
                eventIl.Emit(OpCodes.Ldarg, counter);
                eventIl.Emit(OpCodes.Stelem_Ref);
            }

            eventIl.Emit(OpCodes.Ldloc, local);
            eventIl.Emit(OpCodes.Ldarg_0);
            eventIl.EmitCall(OpCodes.Call, methodToInvoke, null);
            eventIl.Emit(OpCodes.Pop);
            eventIl.Emit(OpCodes.Ret);
            return handler.CreateDelegate(eventHandlerType, methodInvoker);
        }
    }
}
