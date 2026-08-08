namespace FIsioNet.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

	public async void BtnVoltar_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(FeedPage));
    }

    // Rodapé (barra de navegação inferior)
    public async void BtnFeedPage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FeedPage));
    }

    public async void BtnEmBreve_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("FisioNet", "Em breve.", "OK");
    }

    public async void BtnPostPage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PostPage));
    }

    public async void BtnAvisosPage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AvisosPage));
    }

    public void BtnProfilePage_Clicked(object sender, EventArgs e)
    {
        // Já estamos no Perfil, nada a fazer.
    }
}