using FIsioNet.Models;
using System.Collections.ObjectModel;

namespace FIsioNet.Views;

public partial class CursosPage : ContentPage
{
    private const string Todos = "Todos";

    private readonly List<Curso> _todosCursos = new();
    private readonly string[] _categorias = { Todos, "Neurologia", "Ortopedia", "Esportiva", "Respiratória", "Pediátrica" };
    private string _categoriaAtual = Todos;

    public ObservableCollection<Curso> CursosFiltrados { get; } = new();

    private Curso? _cursoDestaque;
    public Curso? CursoDestaque
    {
        get => _cursoDestaque;
        private set { _cursoDestaque = value; OnPropertyChanged(); OnPropertyChanged(nameof(TemDestaque)); }
    }

    public bool TemDestaque => CursoDestaque is not null;
    public bool ListaVazia => CursosFiltrados.Count == 0;
    public string ContagemLista => CursosFiltrados.Count == 1 ? "1 curso" : $"{CursosFiltrados.Count} cursos";
    public string SubtituloCabecalho => $"{_todosCursos.Count} cursos · Certificado reconhecido pelo COFFITO";

    public CursosPage()
    {
        InitializeComponent();
        CarregarCursos();
        MontarChips();
        BindingContext = this;
        AplicarFiltro(Todos);
    }

    // Dados de exemplo — depois isso virá da sua API/banco de dados
    private void CarregarCursos()
    {
        const string capa = "image_b79165.jpg";

        _todosCursos.Add(new Curso
        {
            Titulo = "Reabilitação Neurológica Avançada: do AVC à Esclerose Múltipla",
            Categoria = "Neurologia", Nivel = "Avançado", Selo = "MAIS VENDIDO",
            ImagemCapa = capa, AtualizadoEm = "Set/2026",
            InstrutorNome = "Dra. Ana Beatriz Santos",
            Nota = 4.9, TotalAvaliacoes = 2341, TotalAlunos = 8720,
            CargaHoraria = "24h 30min", TotalModulos = 14,
            Preco = 297, PrecoOriginal = 497, EmDestaque = true
        });
        _todosCursos.Add(new Curso
        {
            Titulo = "Terapia Manual na Coluna Lombar: Avaliação e Tratamento",
            Categoria = "Ortopedia", Nivel = "Intermediário", Selo = "DESTAQUE",
            ImagemCapa = capa, InstrutorNome = "Dr. Ricardo Alves",
            Nota = 4.8, TotalAvaliacoes = 1120, TotalAlunos = 4310,
            CargaHoraria = "18h", TotalModulos = 10,
            Preco = 247, PrecoOriginal = 397
        });
        _todosCursos.Add(new Curso
        {
            Titulo = "Prevenção e Reabilitação de Lesões no Esporte",
            Categoria = "Esportiva", Nivel = "Intermediário",
            ImagemCapa = capa, InstrutorNome = "Dr. Marcelo Teixeira",
            Nota = 4.7, TotalAvaliacoes = 860, TotalAlunos = 3050,
            CargaHoraria = "15h 20min", TotalModulos = 9,
            Preco = 197, PrecoOriginal = 297
        });
        _todosCursos.Add(new Curso
        {
            Titulo = "Fisioterapia Respiratória em UTI Adulto",
            Categoria = "Respiratória", Nivel = "Avançado",
            ImagemCapa = capa, InstrutorNome = "Dra. Juliana Rocha",
            Nota = 4.9, TotalAvaliacoes = 640, TotalAlunos = 2190,
            CargaHoraria = "20h", TotalModulos = 12,
            Preco = 327, PrecoOriginal = 427
        });
        _todosCursos.Add(new Curso
        {
            Titulo = "Desenvolvimento Motor Infantil: Avaliação na Prática",
            Categoria = "Pediátrica", Nivel = "Iniciante",
            ImagemCapa = capa, InstrutorNome = "Dra. Camila Nogueira",
            Nota = 4.6, TotalAvaliacoes = 410, TotalAlunos = 1580,
            CargaHoraria = "12h", TotalModulos = 8,
            Preco = 167, PrecoOriginal = 167
        });
        _todosCursos.Add(new Curso
        {
            Titulo = "Pós-operatório de LCA: Protocolo Baseado em Evidências",
            Categoria = "Ortopedia", Nivel = "Avançado",
            ImagemCapa = capa, InstrutorNome = "Dr. Felipe Moura",
            Nota = 4.8, TotalAvaliacoes = 930, TotalAlunos = 3720,
            CargaHoraria = "16h 45min", TotalModulos = 11,
            Preco = 257, PrecoOriginal = 357
        });
    }

    // ---------- Filtros (chips) ----------
    private void MontarChips()
    {
        foreach (var categoria in _categorias)
        {
            var label = new Label { Text = categoria, FontSize = 16, VerticalOptions = LayoutOptions.Center };
            var chip = new Border
            {
                StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 22 },
                Stroke = Colors.Transparent,
                Padding = new Thickness(20, 10),
                Content = label,
                ClassId = categoria
            };
            var tap = new TapGestureRecognizer();
            tap.Tapped += (_, _) => AplicarFiltro(categoria);
            chip.GestureRecognizers.Add(tap);
            ChipsContainer.Children.Add(chip);
        }
    }

    private void AtualizarChips()
    {
        var primary = (Color)Application.Current!.Resources["Primary"];
        var chipBg = (Color)Resources["ChipBg"];
        var ink = (Color)Resources["Ink"];

        foreach (var child in ChipsContainer.Children.OfType<Border>())
        {
            var ativo = child.ClassId == _categoriaAtual;
            child.BackgroundColor = ativo ? primary : chipBg;
            if (child.Content is Label l)
            {
                l.TextColor = ativo ? Colors.White : ink;
                l.FontAttributes = ativo ? FontAttributes.Bold : FontAttributes.None;
            }
        }
    }

    private void AplicarFiltro(string categoria)
    {
        _categoriaAtual = categoria;

        var daCategoria = _todosCursos
            .Where(c => categoria == Todos || c.Categoria == categoria)
            .ToList();

        // Destaque: o curso marcado como destaque, senão o mais bem avaliado da categoria
        CursoDestaque = daCategoria.FirstOrDefault(c => c.EmDestaque)
                        ?? daCategoria.OrderByDescending(c => c.Nota).FirstOrDefault();

        CursosFiltrados.Clear();
        foreach (var curso in daCategoria.Where(c => c != CursoDestaque))
            CursosFiltrados.Add(curso);

        OnPropertyChanged(nameof(ListaVazia));
        OnPropertyChanged(nameof(ContagemLista));
        AtualizarChips();
    }

    // ---------- Ações dos cards ----------
    private async void BtnComprar_Clicked(object? sender, EventArgs e)
    {
        if (sender is Button { CommandParameter: Curso curso })
        {
            // TODO: integrar com o checkout (ex.: Mercado Pago, Stripe, Pagar.me)
            await DisplayAlert("Comprar curso",
                $"{curso.Titulo}\n\nValor: {curso.PrecoTexto}", "OK");
        }
    }

    private void BtnFavorito_Tapped(object? sender, TappedEventArgs e)
    {
        if (e.Parameter is Curso curso)
            curso.Favorito = !curso.Favorito;
    }

    private async void BtnPreview_Tapped(object? sender, TappedEventArgs e)
    {
        await DisplayAlert("FisioNet", "Prévia do curso em breve.", "OK");
    }

    // ---------- Barra de navegação inferior ----------
    public async void BtnFeedPage_Clicked(object? sender, EventArgs e)
        => await Shell.Current.GoToAsync(nameof(FeedPage));

    public async void BtnPostPage_Clicked(object? sender, EventArgs e)
        => await Shell.Current.GoToAsync(nameof(PostPage));

    public async void BtnProfilePage_Clicked(object? sender, EventArgs e)
        => await Shell.Current.GoToAsync(nameof(ProfilePage));

    public async void BtnSimuladosPage_Clicked(object? sender, EventArgs e)
        => await Shell.Current.GoToAsync(nameof(SimuladosPage));

    public async void BtnEmBreve_Clicked(object? sender, EventArgs e)
        => await DisplayAlert("FisioNet", "Em breve.", "OK");
}
