using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using System.Windows.Data;

namespace WikipediaViewer.Views
{
    public class BrLineBreakConvertercs : IValueConverter
    {
        public object Convert(object value, Type targetType, object prameter, CultureInfo culture)
        {
            return value.ToString().Replace("<br/>", "\n");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
