using FIsioNet.Models;
using System.Collections.ObjectModel;

namespace FIsioNet.Views;

public partial class FeedPage : ContentPage
{
    public ObservableCollection<Post> Posts { get; set; } = new();

    public FeedPage()
    {
        InitializeComponent();

        Posts.Add(new Post
        {
            AuthorName = "Dra. Fernanda Lima",
            AuthorSpecialty = "Fisioterapia Neurológica · CREFITO-2: 98312",
            AuthorIsVerified = true,
            MediaSource = "image_b79185.jpg",
            Description = "Reabilitação motora pós-AVC: protocolo de 8 semanas com resultados mensuráveis em escala FIM.",
            TagCid = "CID G82.5",
            TagMethod = "Método Bobath",
            LikesCount = "1.247",
            SavesCount = "389"
        });

        Posts.Add(new Post
        {
            AuthorName = "Dr. Ricardo Alves",
            AuthorSpecialty = "Fisioterapia Ortopédica · CREFITO-3: 45210",
            AuthorIsVerified = false,
            MediaSource = "image_b79185.jpg",
            Description = "Fortalecimento de manguito rotador: exercícios progressivos pós-cirurgia.",
            TagCid = "CID M75.1",
            TagMethod = "Cadeia Cinética Fechada",
            LikesCount = "532",
            SavesCount = "120"
        });

        BindingContext = this;
    }

    private async void BtnProfilePage_Clicked(object? sender, SwipedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ProfilePage));
    }

    private async void BtnPostPage_Clicked(object? sender, SwipedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PostPage));
    }

    // Rodapé (barra de navegação inferior)
    public void BtnFeedPage_Clicked(object sender, EventArgs e)
    {
        // Já estamos no Feed, nada a fazer.
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

    public async void BtnProfilePage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ProfilePage));
    }
}