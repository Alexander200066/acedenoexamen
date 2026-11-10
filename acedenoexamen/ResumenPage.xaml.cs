using System.Globalization;
using acedenoexamen.Models;

namespace acedenoexamen;

public partial class ResumenPage : ContentPage
{
    public ResumenPage(string usuario, Estudiante estudiante)
    {
        InitializeComponent();
        UsuarioConectadoLabel.Text = $"Usuario conectado: {usuario}";
        NombreLabel.Text = estudiante.Nombre;
        ApellidoLabel.Text = estudiante.Apellido;
        EdadLabel.Text = estudiante.Edad.ToString(CultureInfo.CurrentCulture);
        FechaLabel.Text = estudiante.Fecha.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture);
        CiudadLabel.Text = estudiante.Ciudad;
        PaisLabel.Text = estudiante.Pais;
        MontoInicialLabel.Text = estudiante.MontoInicial.ToString("C", CultureInfo.CurrentCulture);
        PagoMensualLabel.Text = estudiante.PagoMensual.ToString("C", CultureInfo.CurrentCulture);
        PagoTotalLabel.Text = estudiante.PagoTotal.ToString("C", CultureInfo.CurrentCulture);
    }

    private async void OnVolverInicioClicked(object? sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}
