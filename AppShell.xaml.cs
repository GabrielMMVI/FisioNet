namespace FIsioNet
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(Views.CadastroPage), typeof(Views.CadastroPage));
            Routing.RegisterRoute(nameof(Views.PostPage), typeof(Views.PostPage));
            Routing.RegisterRoute(nameof(Views.ProfilePage), typeof(Views.ProfilePage));
            Routing.RegisterRoute(nameof(Views.FeedPage), typeof(Views.FeedPage));
            Routing.RegisterRoute(nameof(Views.AvisosPage), typeof(Views.AvisosPage));
        }
    }
}
