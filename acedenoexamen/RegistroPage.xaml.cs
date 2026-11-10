using System.Globalization;
using acedenoexamen.Models;

namespace acedenoexamen;

public partial class RegistroPage : ContentPage
{
    private readonly string usuario;
    private decimal pagoMensual;

    public RegistroPage(string usuario)
    {
        InitializeComponent();
        this.usuario = usuario;
        UsuarioConectadoLabel.Text = $"Usuario conectado: {usuario}";
        FechaPicker.Date = DateTime.Today;
    }

    private async void OnCalcularPagoClicked(object? sender, EventArgs e)
    {
        if (!TryGetMontoInicial(out var montoInicial))
        {
            await DisplayAlertAsync("Monto inválido", "Ingrese un monto inicial entre 0 y 1500.", "Aceptar");
            return;
        }

        pagoMensual = (1500m - montoInicial) / 4m + 1500m * 0.04m;
        PagoMensualEntry.Text = pagoMensual.ToString("C", CultureInfo.CurrentCulture);
    }

    private async void OnVerResumenClicked(object? sender, EventArgs e)
    {
        if (!TryGetMontoInicial(out var montoInicial))
        {
            await DisplayAlertAsync("Monto inválido", "Ingrese un monto inicial entre 0 y 1500.", "Aceptar");
            return;
        }

        if (string.IsNullOrWhiteSpace(NombreEntry.Text) ||
            string.IsNullOrWhiteSpace(ApellidoEntry.Text) ||
            !int.TryParse(EdadEntry.Text, out var edad) || edad <= 0 ||
            PaisPicker.SelectedIndex < 0 || CiudadPicker.SelectedIndex < 0)
        {
            await DisplayAlertAsync("Datos incompletos", "Complete todos los campos del registro.", "Aceptar");
            return;
        }

        if (pagoMensual == 0)
        {
            pagoMensual = (1500m - montoInicial) / 4m + 60m;
            PagoMensualEntry.Text = pagoMensual.ToString("C", CultureInfo.CurrentCulture);
        }

        var estudiante = new Estudiante
        {
            Nombre = NombreEntry.Text.Trim(),
            Apellido = ApellidoEntry.Text.Trim(),
            Edad = edad,
            Fecha = FechaPicker.Date ?? DateTime.Today,
            Pais = PaisPicker.SelectedItem.ToString()!,
            Ciudad = CiudadPicker.SelectedItem.ToString()!,
            MontoInicial = montoInicial,
            PagoMensual = pagoMensual
        };

        await Navigation.PushAsync(new ResumenPage(usuario, estudiante));
    }

    private bool TryGetMontoInicial(out decimal montoInicial)
    {
        var texto = MontoInicialEntry.Text?.Trim() ?? string.Empty;
        var valido = decimal.TryParse(texto, NumberStyles.Number, CultureInfo.CurrentCulture, out montoInicial) &&
                     montoInicial >= 0 && montoInicial <= 1500;
        return valido;
    }
}
