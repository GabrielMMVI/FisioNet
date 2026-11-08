namespace FIsioNet.Models;

public class Simulado
{
    private static readonly System.Globalization.CultureInfo PtBr = new("pt-BR");

    // Identificação
    public string Titulo { get; set; } = string.Empty;
    public string Subtitulo { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;   // Geral, Neurologia, Ortopedia...
    public string Nivel { get; set; } = string.Empty;       // Iniciante, Intermediário, Avançado
    public string Selo { get; set; } = string.Empty;        // "MAIS RESOLVIDO", "NOVO"... (vazio = sem selo)

    // Números
    public int TotalQuestoes { get; set; }
    public string Duracao { get; set; } = string.Empty;     // "3h 00min"
    public int Tentativas { get; set; }
    public int TaxaAprovacao { get; set; }                  // 0–100
    public double Nota { get; set; }
    public int TotalAvaliacoes { get; set; }

    public List<string> Temas { get; set; } = new();
    public bool EmDestaque { get; set; }

    // ---------- Propriedades formatadas para a UI ----------
    public bool TemSelo => !string.IsNullOrWhiteSpace(Selo);
    public string QuestoesTexto => $"{TotalQuestoes} questões";
    public string TentativasTexto => $"{Tentativas.ToString("N0", PtBr)} tentativas";
    public string AprovacaoTexto => $"{TaxaAprovacao}%";
    public double AprovacaoProgresso => TaxaAprovacao / 100.0;
    public string NotaTexto => Nota.ToString("0.0", PtBr);
    public string AvaliacoesTexto => $"({TotalAvaliacoes.ToString("N0", PtBr)})";
}
