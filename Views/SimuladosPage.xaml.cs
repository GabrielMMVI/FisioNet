using FIsioNet.Models;
using System.Collections.ObjectModel;

namespace FIsioNet.Views;

public partial class SimuladosPage : ContentPage
{
    private const string Todos = "Todos";
    private static readonly System.Globalization.CultureInfo PtBr = new("pt-BR");

    private readonly List<Simulado> _todosSimulados = new();

    // Categoria + emoji exibido no chip
    private readonly (string Nome, string Icone)[] _categorias =
    {
        (Todos, ""), ("Geral", "📋"), ("Neurologia", "🧠"), ("Ortopedia", "🦴"),
        ("Respiratória", "🫁"), ("Residência", "🏥")
    };
    private string _categoriaAtual = Todos;

    public ObservableCollection<Simulado> SimuladosFiltrados { get; } = new();

    private Simulado? _simuladoDestaque;
    public Simulado? SimuladoDestaque
    {
        get => _simuladoDestaque;
        private set { _simuladoDestaque = value; OnPropertyChanged(); OnPropertyChanged(nameof(TemDestaque)); }
    }

    public bool TemDestaque => SimuladoDestaque is not null;
    public bool ListaVazia => SimuladosFiltrados.Count == 0;
    public string ContagemLista => SimuladosFiltrados.Count == 1 ? "1 prova" : $"{SimuladosFiltrados.Count} provas";
    public string SubtituloCabecalho => $"{_todosSimulados.Count} provas · Questões de concursos e residências reais";

    // Estatísticas do topo — depois virão da API
    public string TotalQuestoesTexto => "4.200+";
    public string AprovacaoMediaTexto => $"{(int)Math.Round(_todosSimulados.Average(s => s.TaxaAprovacao))}%";
    public string AtivosHojeTexto => 1840.ToString("N0", PtBr);

    public SimuladosPage()
    {
        InitializeComponent();
        CarregarSimulados();
        MontarChips();
        BindingContext = this;
        AplicarFiltro(Todos);
    }

    // Dados de exemplo — depois isso virá da sua API/banco de dados
    private void CarregarSimulados()
    {
        _todosSimulados.Add(new Simulado
        {
            Titulo = "Simulado Completo COFFITO 2024",
            Subtitulo = "Modelo fiel à prova do Conselho Federal",
            Categoria = "Geral", Nivel = "Avançado", Selo = "MAIS RESOLVIDO",
            TotalQuestoes = 100, Duracao = "3h 00min",
            Tentativas = 4820, TaxaAprovacao = 72, Nota = 4.9, TotalAvaliacoes = 1203,
            Temas = { "Bases Anatômicas", "Biomecânica", "Ética Profissional", "Legislação COFFITO", "Áreas de Atuação" },
            EmDestaque = true
        });
        _todosSimulados.Add(new Simulado
        {
            Titulo = "Concurso EBSERH – Fisioterapia",
            Subtitulo = "Questões das últimas 5 edições",
            Categoria = "Geral", Nivel = "Intermediário", Selo = "NOVO",
            TotalQuestoes = 60, Duracao = "2h 00min",
            Tentativas = 2310, TaxaAprovacao = 68, Nota = 4.8, TotalAvaliacoes = 540,
            Temas = { "SUS", "Saúde Pública", "Cinesioterapia" }
        });
        _todosSimulados.Add(new Simulado
        {
            Titulo = "Fisioterapia Neurofuncional",
            Subtitulo = "AVC, lesão medular, Parkinson e EM",
            Categoria = "Neurologia", Nivel = "Avançado",
            TotalQuestoes = 40, Duracao = "1h 20min",
            Tentativas = 1650, TaxaAprovacao = 64, Nota = 4.7, TotalAvaliacoes = 380,
            Temas = { "Neuroanatomia", "Escalas Funcionais", "Bobath" }
        });
        _todosSimulados.Add(new Simulado
        {
            Titulo = "Traumato-Ortopedia Essencial",
            Subtitulo = "Lesões, testes especiais e pós-operatório",
            Categoria = "Ortopedia", Nivel = "Intermediário",
            TotalQuestoes = 50, Duracao = "1h 40min",
            Tentativas = 2980, TaxaAprovacao = 75, Nota = 4.8, TotalAvaliacoes = 710,
            Temas = { "Testes Ortopédicos", "Fraturas", "LCA" }
        });
        _todosSimulados.Add(new Simulado
        {
            Titulo = "Fisioterapia em Terapia Intensiva",
            Subtitulo = "Ventilação mecânica e desmame",
            Categoria = "Respiratória", Nivel = "Avançado",
            TotalQuestoes = 45, Duracao = "1h 30min",
            Tentativas = 1420, TaxaAprovacao = 61, Nota = 4.9, TotalAvaliacoes = 295,
            Temas = { "VM", "Gasometria", "Desmame" }
        });
        _todosSimulados.Add(new Simulado
        {
            Titulo = "Residência Multiprofissional – USP",
            Subtitulo = "Prova comentada edição 2024",
            Categoria = "Residência", Nivel = "Avançado",
            TotalQuestoes = 80, Duracao = "2h 30min",
            Tentativas = 1980, TaxaAprovacao = 58, Nota = 4.8, TotalAvaliacoes = 460,
            Temas = { "Políticas de Saúde", "Epidemiologia", "Reabilitação" }
        });
        _todosSimulados.Add(new Simulado
        {
            Titulo = "Revisão Rápida – Anatomia e Cinesiologia",
            Subtitulo = "Para treinar no dia a dia",
            Categoria = "Geral", Nivel = "Iniciante",
            TotalQuestoes = 30, Duracao = "45min",
            Tentativas = 3540, TaxaAprovacao = 81, Nota = 4.6, TotalAvaliacoes = 820,
            Temas = { "Anatomia", "Cinesiologia" }
        });
    }

    // ---------- Filtros (chips) ----------
    private void MontarChips()
    {
        foreach (var (nome, icone) in _categorias)
        {
            var label = new Label
            {
                Text = string.IsNullOrEmpty(icone) ? nome : $"{icone}  {nome}",
                FontSize = 16,
                VerticalOptions = LayoutOptions.Center
            };
            var chip = new Border
            {
                StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 22 },
                Stroke = Colors.Transparent,
                Padding = new Thickness(20, 10),
                Content = label,
                ClassId = nome
            };
            var tap = new TapGestureRecognizer();
            tap.Tapped += (_, _) => AplicarFiltro(nome);
            chip.GestureRecognizers.Add(tap);
            ChipsContainer.Children.Add(chip);
        }
    }

    private void AtualizarChips()
    {
        var primary = (Color)Application.Current!.Resources["Primary"];
        var chipBg = (Color)Resources["ChipBg"];
        var ink = (Color)Resources["Ink"];

        foreach (var chip in ChipsContainer.Children.OfType<Border>())
        {
            var ativo = chip.ClassId == _categoriaAtual;
            chip.BackgroundColor = ativo ? primary : chipBg;
            if (chip.Content is Label l)
            {
                l.TextColor = ativo ? Colors.White : ink;
                l.FontAttributes = ativo ? FontAttributes.Bold : FontAttributes.None;
            }
        }
    }

    private void AplicarFiltro(string categoria)
    {
        _categoriaAtual = categoria;

        var daCategoria = _todosSimulados
            .Where(s => categoria == Todos || s.Categoria == categoria)
            .ToList();

        // Destaque: o simulado marcado, senão o mais resolvido da categoria
        SimuladoDestaque = daCategoria.FirstOrDefault(s => s.EmDestaque)
                           ?? daCategoria.OrderByDescending(s => s.Tentativas).FirstOrDefault();

        SimuladosFiltrados.Clear();
        foreach (var s in daCategoria.Where(s => s != SimuladoDestaque))
            SimuladosFiltrados.Add(s);

        OnPropertyChanged(nameof(ListaVazia));
        OnPropertyChanged(nameof(ContagemLista));
        AtualizarChips();
    }

    // ---------- Ações ----------
    private async void BtnIniciar_Clicked(object? sender, EventArgs e)
    {
        if (sender is Button { CommandParameter: Simulado simulado })
        {
            // TODO: abrir a tela de resolução de questões (cronômetro, alternativas, gabarito comentado)
            await DisplayAlertAsync("Iniciar simulado",
                $"{simulado.Titulo}\n\n{simulado.QuestoesTexto} · {simulado.Duracao}", "OK");
        }
    }

    // ---------- Barra de navegação inferior ----------
    public async void BtnFeedPage_Clicked(object? sender, EventArgs e)
        => await Shell.Current.GoToAsync(nameof(FeedPage));

    public async void BtnCursosPage_Clicked(object? sender, EventArgs e)
        => await Shell.Current.GoToAsync(nameof(CursosPage));

    public async void BtnPostPage_Clicked(object? sender, EventArgs e)
        => await Shell.Current.GoToAsync(nameof(PostPage));

    public async void BtnProfilePage_Clicked(object? sender, EventArgs e)
        => await Shell.Current.GoToAsync(nameof(ProfilePage));
}
