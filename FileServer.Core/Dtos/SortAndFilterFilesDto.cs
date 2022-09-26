namespace FileServer.Core.Dtos;

/// <summary>
/// Дто поиска и сортировки файлов
/// </summary>
public class SortAndFilterFilesDto
{
    /// <summary>
    /// Строка поиска
    /// </summary>
    public string? SearchString { get; set; }
    
    /// <summary>
    /// Направление сортировки
    /// </summary>
    public string? SortOrder { get; set; }
}