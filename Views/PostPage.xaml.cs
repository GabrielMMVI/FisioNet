namespace FIsioNet.Views;

public partial class PostPage : ContentPage
{
    public PostPage()
    {
        InitializeComponent();
    }

    public async void BtnVoltar_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FeedPage));
    }

    private async void BtnSubstituir_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("FisioNet", "Em breve.", "OK");
    }

    private async void BtnReproduzir_Tapped(object sender, EventArgs e)
    {
        await DisplayAlert("FisioNet", "Em breve.", "OK");
    }

    private void Campo_TextChanged(object sender, TextChangedEventArgs e)
    {
        AtualizarEstadoPublicar();
    }

    private void CheckboxEtica_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        AtualizarEstadoPublicar();
    }

    private void AtualizarEstadoPublicar()
    {
        var camposPreenchidos =
            !string.IsNullOrWhiteSpace(editorHistoria.Text) &&
            !string.IsNullOrWhiteSpace(editorIntervencao.Text) &&
            !string.IsNullOrWhiteSpace(editorResultados.Text);

        btnPublicar.IsEnabled = camposPreenchidos && checkboxEtica.IsChecked;
    }

    private async void BtnPublicar_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("FisioNet", "Caso clínico publicado com sucesso!", "OK");
        await Shell.Current.GoToAsync(nameof(FeedPage));
    }

    public void BtnPostPage_Clicked(object sender, EventArgs e)
    {
        // Já estamos na aba Publicar, nada a fazer.
    }

    public async void BtnProfilePage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ProfilePage));
    }

    public async void BtnFeedPage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FeedPage));
    }

    public async void BtnEmBreve_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("FisioNet", "Em breve.", "OK");
    }

    public async void BtnAvisosPage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AvisosPage));
    }
}
