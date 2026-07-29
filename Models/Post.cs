namespace FIsioNet.Models;

public class Post
{
    // Autor
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorSpecialty { get; set; } = string.Empty;
    public string AuthorRegistration { get; set; } = string.Empty;
    public bool AuthorIsVerified { get; set; }
    public string AuthorAvatar { get; set; } = string.Empty;

    // Conteúdo do post
    public string MediaSource { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Tags (CID, método, etc.)
    public string TagCid { get; set; } = string.Empty;
    public string TagMethod { get; set; } = string.Empty;

    // Interações
    public string LikesCount { get; set; } = "0";
    public string SavesCount { get; set; } = "0";
}