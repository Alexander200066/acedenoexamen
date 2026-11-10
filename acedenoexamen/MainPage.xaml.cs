namespace acedenoexamen
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object? sender, EventArgs e)
        {
            var credencialesValidas =
                (UsuarioEntry.Text == "estudiante" && ContrasenaEntry.Text == "moviles") ||
                (UsuarioEntry.Text == "suda" && ContrasenaEntry.Text == "2025");

            if (!credencialesValidas)
            {
                await DisplayAlertAsync("Dato incorrecto", "El usuario o la contraseña no son válidos.", "Aceptar");
                return;
            }

            await Navigation.PushAsync(new RegistroPage(UsuarioEntry.Text));
        }
    }
}
