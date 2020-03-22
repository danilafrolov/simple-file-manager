using Newtonsoft.Json;
using SimpleFileManager.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace SimpleFileManager.Models
{
    public class FileDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "extension")]
        public string Extension { get; set; }

        [JsonProperty(PropertyName = "creationDate")]
        public string CreationDate { get; set; }

        [JsonProperty(PropertyName = "size")]
        public long Size { get; set; }

        [JsonProperty(PropertyName = "formattedSize")]
        public string FormattedSize {
            get { return FileHelper.GetFormattedStringSize(Size); }
        }

        [JsonProperty(PropertyName = "contentUrl")]
        public string ContentUrl { get; set; }

        [JsonProperty(PropertyName = "contentText")]
        public string ContentText {
            get { return FileHelper.GetTextFileContent(this); }
        }

        public FileDto() {
        }

        public FileDto(FileInfo file) {
            Name = file.Name;
            Extension = file.Extension;
            CreationDate = file.LastWriteTime.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            Size = file.Length;
        }
    }
}