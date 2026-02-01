using System.Globalization;
using System.Windows.Data;

namespace BudgetPlanner.App.Converters
{
	public class BoolToIndexConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool boolValue)
			{
				return boolValue ? 1 : 0; // true = Ascending (index 1), false = Descending (index 0)
			}
			return 0;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is int index)
			{
				return index == 1; // index 1 = Ascending (true), index 0 = Descending (false)
			}
			return false;
		}
	}
}
