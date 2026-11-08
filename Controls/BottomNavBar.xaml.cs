using FIsioNet.Views;

namespace FIsioNet.Controls;

public partial class BottomNavBar : ContentView
{
    // Aba -> rota registrada no AppShell
    private static readonly Dictionary<string, string> Rotas = new()
    {
        ["Feed"] = nameof(FeedPage),
        ["Cursos"] = nameof(CursosPage),
        ["Publicar"] = nameof(PostPage),
        ["Simulados"] = nameof(SimuladosPage),
        ["Perfil"] = nameof(ProfilePage),
    };

    public static readonly BindableProperty AbaAtualProperty = BindableProperty.Create(
        nameof(AbaAtual), typeof(string), typeof(BottomNavBar), string.Empty,
        propertyChanged: (b, _, _) => ((BottomNavBar)b).AtualizarVisual());

    /// <summary>Aba destacada: Feed, Cursos, Publicar, Simulados ou Perfil.</summary>
    public string AbaAtual
    {
        get => (string)GetValue(AbaAtualProperty);
        set => SetValue(AbaAtualProperty, value);
    }

    private static bool _navegando;

    public BottomNavBar()
    {
        InitializeComponent();
        AtualizarVisual();
    }

    private void AtualizarVisual()
    {
        var ativa = (Color)Application.Current!.Resources["Primary"];
        var inativa = (Color)Application.Current!.Resources["Gray400"];

        void Estilo(Label label, View? icone, string aba)
        {
            var on = AbaAtual == aba;
            label.TextColor = on ? ativa : inativa;
            label.FontAttributes = on ? FontAttributes.Bold : FontAttributes.None;
            if (icone is not null) icone.Opacity = on ? 1 : 0.5;
        }

        Estilo(LabelFeed, IconFeed, "Feed");
        Estilo(LabelCursos, IconCursos, "Cursos");
        Estilo(LabelPublicar, null, "Publicar");
        Estilo(LabelSimulados, IconSimulados, "Simulados");
        Estilo(LabelPerfil, IconPerfil, "Perfil");
    }

    private async void Tab_Tapped(object? sender, TappedEventArgs e)
    {
        if (e.Parameter is not string aba || aba == AbaAtual || _navegando) return;
        if (!Rotas.TryGetValue(aba, out var rota)) return;

        _navegando = true;
        try
        {
            var nav = Shell.Current.Navigation;
            var paginaAnterior = nav.NavigationStack.LastOrDefault();

            // 1) Abre a nova aba (sem animação, para parecer troca de aba)
            await Shell.Current.GoToAsync(rota, false);

            // 2) Remove a aba anterior da pilha, para não acumular telas a cada toque.
            //    (Não usamos "../rota" porque o Shell do MAUI dá erro com rota relativa.)
            if (paginaAnterior is not null
                && EhPaginaDeAba(paginaAnterior)
                && nav.NavigationStack.Contains(paginaAnterior)
                && nav.NavigationStack.LastOrDefault() != paginaAnterior)
            {
                nav.RemovePage(paginaAnterior);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[BottomNavBar] Erro ao navegar para {rota}: {ex}");
            if (Shell.Current?.CurrentPage is Page pagina)
                await pagina.DisplayAlertAsync("Erro de navegação", ex.Message, "OK");
        }
        finally
        {
            _navegando = false;
        }
    }

    // Só removemos telas que são abas — nunca o Login (MainPage) nem o Cadastro.
    private static bool EhPaginaDeAba(Page p) =>
        p is FeedPage or CursosPage or PostPage or SimuladosPage or ProfilePage or AvisosPage;
}
