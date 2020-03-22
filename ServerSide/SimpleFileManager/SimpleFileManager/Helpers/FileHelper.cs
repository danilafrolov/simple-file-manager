using SimpleFileManager.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace SimpleFileManager.Helpers
{
    public static class FileHelper
    {
        private static readonly string[] sizeNames = { "Б", "КБ", "МБ", "ГБ", "ТБ" };
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };
        public static readonly List<string> TextExtensions = new List<string> { ".TXT" };

        public static DirectoryInfo FilesDirectory { get; private set; }

        public static string GetFilesStorageFolderName() {
            var fileStorageFolder = WebConfigurationManager.AppSettings["fileStorageFolder"];
            return fileStorageFolder;
        }

        public static void SetFilesDirectory() {
            var folderName = GetFilesStorageFolderName();
            if (string.IsNullOrEmpty(folderName)) {
                throw new Exception("В web.config не задано имя папки с файлами пользователя");
            }

            var dir = AppDomain.CurrentDomain.BaseDirectory;
            FilesDirectory = new DirectoryInfo(dir + folderName);
            if (!FilesDirectory.Exists) {
                FilesDirectory.Create();
            }
        }

        public static IList<FileDto> GetUserFiles() {
            if (FilesDirectory == null) {
                SetFilesDirectory();
            }

            var fileDtos = FilesDirectory.GetFiles().Select(f => new FileDto(f)).ToList();
            return fileDtos;
        }

        public static string GetFormattedStringSize(long size) {
            if (size < 0) {
                return string.Empty;
            }

            if (size < 1024) {
                return $"{size} {sizeNames[0]}";
            }

            var order = Convert.ToInt32(Math.Floor(Math.Log(size, 1024)));
            var value = Math.Round(size / Math.Pow(1024, order), 1);
            var result = $"{value.ToString(CultureInfo.InvariantCulture)} {sizeNames[order]}";
            return result;
        }

        public static string GetTextFileContent(FileDto file) {
            if (!TextExtensions.Contains(file.Extension.ToUpper())) {
                return string.Empty;
            }

            var fileInfo = GetFileInfo(file.Name);
            return File.ReadAllText(fileInfo.FullName);
        }

        public static FileInfo GetFileInfo(string filename) {
            var fileInfo = new FileInfo($"{FilesDirectory}/{filename}");
            if (!fileInfo.Exists) {
                throw new Exception($"Файл {filename} отсутствует в файловой папке");
            }
            return fileInfo;
        }
    }
}