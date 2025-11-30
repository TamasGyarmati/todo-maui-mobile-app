using System.Globalization;

namespace ToDo.Converters;

public class DeadlineConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is DateTime deadline)
        {
            var kulonbseg = deadline.Subtract(DateTime.Now);
            return kulonbseg.Days;
        }
        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}