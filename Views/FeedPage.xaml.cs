namespace FIsioNet.Views;


public partial class FeedPage : ContentPage
{
	public FeedPage()
	{
		InitializeComponent();
	}

	public async void BtnPostPage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PostPage));
    }

    public async void BtnProfilePage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ProfilePage));
    }

    public void BtnFeedPage_Clicked(object sender, EventArgs e)
    {
        // Já estamos no Feed, nada a fazer.
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
