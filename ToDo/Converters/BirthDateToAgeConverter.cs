using System.Globalization;

namespace ToDo.Converters;

public class BirthDateToAgeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is DateTime date) 
        {
            var age = DateTime.Now.Year - date.Year; // 2026 - 2005
            if (date.Month < DateTime.Now.Month || (date.Month == DateTime.Now.Month && date.Day < DateTime.Now.Day))
            {
                age--;
            }
            return age;
        }
        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}