using System.Data.Entity.Core;

namespace FileServer.Core.Extensions
{
    internal static class ObjectNotFoundExtensions
    {
        /// <summary>
        /// Выбрасывает исключение когда объект равен null
        /// </summary>
        /// <param name="obj">объект</param>
        /// <param name="message">сообщение в исключении</param>
        public static void ThrowIfNotFound<T>(this T obj, string message)
        {
            if (obj == null)
                throw new ObjectNotFoundException(message);
        }

        //public static void ThrowIfFileNotFound<T>() { }
    }
}
