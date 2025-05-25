using System;
using System.Globalization;
using Avalonia.Data.Converters;
using SportShop.Models;

namespace SportShop.Helpers
{
  public class OrderStatusConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is OrderStatus status)
      {
        switch (status)
        {
          case OrderStatus.COMPLETED:
            return "Done";
          case OrderStatus.IN_PROGRESS:
            return "In progress";
          default:
            return "Uknown";
        }
      }
      return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotSupportedException();
    }
  }
}