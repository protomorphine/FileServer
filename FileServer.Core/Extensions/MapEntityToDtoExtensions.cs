using FileServer.Core.Dtos;
using FileServer.Core.Models;

namespace FileServer.Core.Extensions
{
    /// <summary>
    /// Методы расширения для мапинга сущностей на ДТО
    /// </summary>
    public static class MapEntityToDtoExtensions
    {
        /// <summary>
        /// Мапинг сущности на ДТО
        /// </summary>
        /// <param name="fileEntity">сущность - файл</param>
        /// <returns>ДТО - файл</returns>
        public static FileDto ToFileDto(this FileEntity fileEntity) =>
            new FileDto
            {
                FileId = fileEntity.Id,
                FileName = fileEntity.Name
            };

        /// <summary>
        /// Мапинг списка сущностей на список ДТО
        /// </summary>
        /// <param name="listFileEntyties">список сущностей</param>
        /// <returns>список ДТО</returns>
        public static List<FileDto> ToFileDtoList(this List<FileEntity> listFileEntyties)
        {
            var fileDtoList = new List<FileDto>();

            foreach(var fileEntity in listFileEntyties)
            {
                fileDtoList.Add(new FileDto
                {
                    FileId = fileEntity.Id,
                    FileName = fileEntity.Name
                });
            }

            return fileDtoList;
        }
    }
}
