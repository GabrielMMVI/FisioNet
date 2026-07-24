namespace FIsioNet.Views;

public partial class AvisosPage : ContentPage
{
    public AvisosPage()
    {
        InitializeComponent();
    }

    public async void BtnVoltarFeed_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FeedPage));
    }

    public async void BtnFeedPage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FeedPage));
    }

    public async void BtnPostPage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PostPage));
    }

    public async void BtnProfilePage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ProfilePage));
    }

    public void BtnAvisosPage_Clicked(object sender, EventArgs e)
    {
        // Já estamos em Avisos, nada a fazer.
    }

    public async void BtnEmBreve_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("FisioNet", "Em breve.", "OK");
    }
}
