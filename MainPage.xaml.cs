using FIsioNet.Views;

namespace FIsioNet;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void BtnEntrar_Clicked(object sender, EventArgs e)
    {
        var usuario = EntryLoginUsuario.Text?.Trim();
        var senha = EntryLoginSenha.Text;

        if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(senha))
        {
            await DisplayAlert("Login", "Informe usuário e senha.", "OK");
            return;
        }

        await Shell.Current.GoToAsync(nameof(FeedPage));
    }

    private async void OnCadastrarTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CadastroPage));
    }
}
