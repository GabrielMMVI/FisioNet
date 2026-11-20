using System.ComponentModel;

namespace FIsioNet.Models;

public class Curso : INotifyPropertyChanged
{
    // Identificação
    public string Titulo { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;   // Neurologia, Ortopedia, Esportiva...
    public string Nivel { get; set; } = string.Empty;       // Iniciante, Intermediário, Avançado
    public string Selo { get; set; } = string.Empty;        // "MAIS VENDIDO", "DESTAQUE"... (vazio = sem selo)
    public string ImagemCapa { get; set; } = string.Empty;
    public string AtualizadoEm { get; set; } = string.Empty;

    // Instrutor
    public string InstrutorNome { get; set; } = string.Empty;
    public string InstrutorAvatar { get; set; } = string.Empty;

    // Avaliações
    public double Nota { get; set; }
    public int TotalAvaliacoes { get; set; }
    public int TotalAlunos { get; set; }

    // Conteúdo
    public string CargaHoraria { get; set; } = string.Empty;
    public int TotalModulos { get; set; }
    public bool TemCertificado { get; set; } = true;

    // Preço
    public decimal Preco { get; set; }
    public decimal PrecoOriginal { get; set; }

    public bool EmDestaque { get; set; }

    // Favorito muda em tempo real (botão de coração), por isso notifica a UI
    private bool _favorito;
    public bool Favorito
    {
        get => _favorito;
        set
        {
            if (_favorito == value) return;
            _favorito = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Favorito)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IconeFavorito)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    // ---------- Propriedades formatadas para a UI ----------
    private static readonly System.Globalization.CultureInfo PtBr = new("pt-BR");

    public bool TemSelo => !string.IsNullOrWhiteSpace(Selo);
    public bool TemDesconto => PrecoOriginal > Preco && PrecoOriginal > 0;
    public int PercentualDesconto => TemDesconto ? (int)Math.Round((1 - Preco / PrecoOriginal) * 100) : 0;

    public string PrecoTexto => $"R$ {Preco.ToString("N0", PtBr)}";
    public string PrecoOriginalTexto => $"R$ {PrecoOriginal.ToString("N0", PtBr)}";
    public string DescontoTexto => $"{PercentualDesconto}% OFF · Oferta por tempo limitado";

    public string NotaTexto => Nota.ToString("0.0", PtBr);
    public string AvaliacoesTexto => $"({TotalAvaliacoes.ToString("N0", PtBr)})";
    public string AlunosTexto => $"{TotalAlunos.ToString("N0", PtBr)} alunos";
    public string ModulosTexto => $"{TotalModulos} módulos";
    public string AtualizadoTexto => string.IsNullOrWhiteSpace(AtualizadoEm) ? string.Empty : $"Atualizado {AtualizadoEm}";

    // Estrelas cheias (★) e vazias (☆) arredondando para o inteiro mais próximo
    public string Estrelas
    {
        get
        {
            var cheias = (int)Math.Clamp(Math.Round(Nota), 0, 5);
            return new string('★', cheias) + new string('☆', 5 - cheias);
        }
    }

    public string IconeFavorito => Favorito ? "♥" : "♡";
}
