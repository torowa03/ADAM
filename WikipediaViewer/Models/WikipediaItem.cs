using Newtonsoft.Json;
using System;

namespace WikipediaViewer.Modells
{
    public class WikipediaItem
    {
        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("datetime")]
        public DateTime Date { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }

        [JsonProperty("length")]
        public long Length { get; set; }

        [JsonProperty("redirect")]
        public int Redirect { get; set; }

        [JsonProperty("strict")]
        public int Strict { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

    }
}