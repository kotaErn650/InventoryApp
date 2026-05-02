using System.Globalization;

namespace InventoryApp.Converters;

public class BoolToEstadoConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool activo)
            return activo ? "🟢 Disponible" : "⚪ Pausado";
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
