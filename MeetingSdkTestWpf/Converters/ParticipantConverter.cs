using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using MeetingSdk.Wpf;

namespace MeetingSdkTestWpf.Converters
{
    public class ParticipantConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ListBoxItem listBoxItem = value as ListBoxItem;
            Participant participant = (Participant) listBoxItem.DataContext;
            return participant ?? value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
