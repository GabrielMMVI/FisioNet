namespace FIsioNet;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void BtnEntrar_Clicked(object sender, EventArgs e)
    {
        // ...seu código do botão Entrar...
    }

    private async void OnCadastrarTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("Cadastro", "Ir para a tela de cadastro.", "OK");
    }
}