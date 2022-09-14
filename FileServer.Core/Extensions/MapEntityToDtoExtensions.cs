using FileServer.Core.Dtos;
using FileServer.Core.Entities;

namespace FileServer.Core.Extensions
{
    /// <summary>
    /// Методы расширения для мапинга сущностей на ДТО
    /// </summary>
    public static class MapEntityToDtoExtensions
    {
        /// <summary>
        /// Мапинг списка сущностей на список ДТО
        /// </summary>
        /// <param name="listFileEntyties">список сущностей</param>
        /// <returns>список ДТО</returns>
        public static List<FileDto> ToFileDtoList(this List<FileEntity> listFileEntyties) =>
            listFileEntyties.Select(it => it.ToFileDto()).ToList();
    }
}
