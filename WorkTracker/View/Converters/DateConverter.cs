﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WorkTracker.WPF.View.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringDate = (string)value;
            if (DateTime.TryParse(stringDate, culture, DateTimeStyles.None, out var date))
                return date;
            return new DateTime();
        }
    }
}
