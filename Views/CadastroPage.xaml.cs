namespace FIsioNet.Views;

public partial class CadastroPage : ContentPage
{
    public CadastroPage()
    {
        InitializeComponent();
    }

    private async void BtnCadastrar_Clicked(object sender, EventArgs e)
    {
        var nome = EntryNome.Text?.Trim();
        var email = EntryEmail.Text?.Trim();
        var senha = EntrySenha.Text;
        var confirmarSenha = EntryConfirmarSenha.Text;

        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(senha) || string.IsNullOrWhiteSpace(confirmarSenha))
        {
            await DisplayAlert("Cadastro", "Preencha todos os campos.", "OK");
            return;
        }

        if (!email.Contains('@'))
        {
            await DisplayAlert("Cadastro", "Informe um email válido.", "OK");
            return;
        }

        if (senha != confirmarSenha)
        {
            await DisplayAlert("Cadastro", "As senhas não coincidem.", "OK");
            return;
        }

        await DisplayAlert("Cadastro", "Conta criada com sucesso! Faça login para continuar.", "OK");
        await Shell.Current.GoToAsync(nameof(MainPage));
    }

    private async void OnVoltarParaLoginTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainPage));
    }
}
