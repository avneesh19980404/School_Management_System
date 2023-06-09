﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace School.Core.Helper
{
    public class Guard
    {
        const string AgainstMessage = "Assertion evaluation failed with 'false'.";
        const string ImplementsMessage = "Type '{0}' must implement type '{1}'.";
        const string InheritsFromMessage = "Type '{0}' must inherit from type '{1}'.";
        const string IsTypeOfMessage = "Type '{0}' must be of type '{1}'.";
        const string IsEqualMessage = "Compared objects must be equal.";
        const string IsPositiveMessage = "Argument '{0}' must be a positive value. Value: '{1}'.";
        const string IsTrueMessage = "True expected for '{0}' but the condition was False.";
        const string NotNegativeMessage = "Argument '{0}' cannot be a negative value. Value: '{1}'.";
        const string NotEmptyStringMessage = "String parameter '{0}' cannot be null or all whitespace.";
        const string NotEmptyColMessage = "Collection cannot be null and must contain at least one item.";
        const string NotEmptyGuidMessage = "Argument '{0}' cannot be an empty guid.";
        const string InRangeMessage = "The argument '{0}' must be between '{1}' and '{2}'.";
        const string NotOutOfLengthMessage = "Argument '{0}' cannot be more than {1} characters long.";
        const string NotZeroMessage = "Argument '{0}' must be greater or less than zero. Value: '{1}'.";
        const string IsEnumTypeMessage = "Type '{0}' must be a valid Enum type.";
        const string IsEnumTypeMessage2 = "The value of the argument '{0}' provided for the enumeration '{1}' is invalid.";
        const string IsSubclassOfMessage = "Type '{0}' must be a subclass of type '{1}'.";
        const string HasDefaultConstructorMessage = "The type '{0}' must have a default parameterless constructor.";

        private Guard()
        {
        }

        #region 3.0

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull(object arg, string argName)
        {
            if (arg == null)
                throw new ArgumentNullException(argName);
        }

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Against<TException>(bool assertion, string message = AgainstMessage) where TException : Exception
        {
            if (assertion)
                throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Against<TException>(Func<bool> assertion, string message = AgainstMessage) where TException : Exception
        {
            //Execute the lambda and if it evaluates to true then throw the exception.
            if (assertion())
                throw (TException)Activator.CreateInstance(typeof(TException), message);
        }


        #endregion

        [DebuggerStepThrough]
        [Obsolete("Use NotNull() with nameof operator instead")]
        public static void ArgumentNotNull(object arg, string argName)
        {
            if (arg == null)
                throw new ArgumentNullException(argName);
        }

        [DebuggerStepThrough]
        [Obsolete("Use NotNull() with nameof operator instead")]
        public static void ArgumentNotNull<T>(Func<T> arg)
        {
            if (arg() == null)
                throw new ArgumentNullException(GetParamName(arg));
        }

 
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InheritsFrom<TBase>(Type type, string message)
        {
            if (type.BaseType != typeof(TBase))
                throw new InvalidOperationException(message);
        }

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsTypeOf<TType>(object instance, string message)
        {
            if (!(instance is TType))
                throw new InvalidOperationException(message);
        }

        [DebuggerStepThrough]
        public static void IsEqual<TException>(object compare, object instance, string message = IsEqualMessage) where TException : Exception
        {
            if (!compare.Equals(instance))
                throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetParamName<T>(Func<T> expression)
        {
            return expression.Method.Name;
        }

    }
}
