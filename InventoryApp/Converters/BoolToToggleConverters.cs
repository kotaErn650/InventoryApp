using System.Globalization;

namespace InventoryApp.Converters;

public class BoolToToggleTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool activo)
            return activo ? "Deshabilitar" : "Habilitar";
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

public class BoolToToggleColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool activo)
            return activo ? Color.FromArgb("#CC3333") : Color.FromArgb("#33AA55");
        return Color.FromArgb("#CC3333");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
