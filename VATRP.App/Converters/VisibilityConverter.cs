﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using VATRP.Core.Enums;
using VATRP.Core.Model;

namespace com.vtcsecure.ace.windows.Converters
{
    public class IntToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// FalseEquivalent (default : Visibility.Collapsed => see Constructor)
        /// </summary>
        public Visibility FalseEquivalent { get; set; }
        /// <summary>
        /// Define whether the opposite boolean value is crucial (default : false)
        /// </summary>
        public bool OppositeBooleanValue { get; set; }

        public IntToVisibilityConverter()
        {
            this.FalseEquivalent = Visibility.Collapsed;
            this.OppositeBooleanValue = false;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            if (value is int && targetType == typeof(Visibility))
            {
                bool booleanValue = (int)value == 0;

                if (OppositeBooleanValue)
                {
                    booleanValue = !booleanValue;
                }

                return booleanValue ? Visibility.Visible : FalseEquivalent;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            return null;
        }
    }

    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// FalseEquivalent (default : Visibility.Collapsed => see Constructor)
        /// </summary>
        public Visibility FalseEquivalent { get; set; }
        /// <summary>
        /// Define whether the opposite boolean value is crucial (default : false)
        /// </summary>
        public bool OppositeBooleanValue { get; set; }

        public BoolToVisibilityConverter()
        {
            this.FalseEquivalent = Visibility.Collapsed;
            this.OppositeBooleanValue = false;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            if (value is bool && targetType == typeof(Visibility))
            {
                bool booleanValue = (bool)value ;

                if (OppositeBooleanValue)
                {
                    booleanValue = !booleanValue;
                }

                return booleanValue ? Visibility.Visible : FalseEquivalent;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            return null;
        }

    }

    public class StringLengthToVisibilityConverter : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        /// <summary>
        /// FalseEquivalent (default : Visibility.Collapsed => see Constructor)
        /// </summary>
        public Visibility FalseEquivalent { get; set; }
        /// <summary>
        /// Define whether the opposite boolean value is crucial (default : false)
        /// </summary>
        public bool OppositeBooleanValue { get; set; }

        public StringLengthToVisibilityConverter()
        {
            this.FalseEquivalent = Visibility.Collapsed;
            this.OppositeBooleanValue = false;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            if (value is string && targetType == typeof(Visibility))
            {
                bool booleanValue = string.IsNullOrEmpty((string)value);

                if (OppositeBooleanValue)
                {
                    booleanValue = !booleanValue;
                }

                return booleanValue ? Visibility.Visible : FalseEquivalent;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class DirectionToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// FalseEquivalent (default : Visibility.Collapsed => see Constructor)
        /// </summary>
        public Visibility FalseEquivalent { get; set; }
        /// <summary>
        /// Define whether the opposite boolean value is crucial (default : false)
        /// </summary>
        public bool OppositeBooleanValue { get; set; }

        public DirectionToVisibilityConverter()
        {
            this.FalseEquivalent = Visibility.Collapsed;
            this.OppositeBooleanValue = false;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            if (value is MessageDirection && targetType == typeof(Visibility))
            {
                bool booleanValue = (MessageDirection)value == MessageDirection.Incoming;

                if (OppositeBooleanValue)
                {
                    booleanValue = !booleanValue;
                }

                return booleanValue ? Visibility.Visible : FalseEquivalent;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            return null;
        }

    }

    public class CallStatusDurationVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// FalseEquivalent (default : Visibility.Collapsed => see Constructor)
        /// </summary>
        public Visibility FalseEquivalent { get; set; }
        /// <summary>
        /// Define whether the opposite boolean value is crucial (default : false)
        /// </summary>
        public bool OppositeBooleanValue { get; set; }

        public CallStatusDurationVisibilityConverter()
        {
            this.FalseEquivalent = Visibility.Visible;
            this.OppositeBooleanValue = false;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            if (value is VATRPHistoryEvent.StatusType && targetType == typeof(Visibility))
            {
                bool booleanValue = (VATRPHistoryEvent.StatusType)value == VATRPHistoryEvent.StatusType.Missed;

                if (OppositeBooleanValue)
                {
                    booleanValue = !booleanValue;
                }

                return booleanValue ? Visibility.Collapsed : FalseEquivalent;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            return null;
        }

    }
}
