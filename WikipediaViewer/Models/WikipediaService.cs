using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace WikipediaViewer.Modells
{
    class WikipediaService : BindableBase
    {
        const string EndpointUri = "http://wikipedia.simpleapi.net/api";

        private List<WikipediaItem> results = new List<WikipediaItem>();
        public List<WikipediaItem> Results
        {
            get
            {
                return this.results;
            }
            private set
            {
                this.SetProperty(ref this.results, value);
            }
        }

        public async Task Search(string keyword)
        {
            using (var client = new HttpClient())
            {
                var builder = new UriBuilder(EndpointUri);
                builder.Port = -1;
                builder.Query =
                    string.Format("output=json&keyword={0}", Uri.EscapeDataString(keyword));
                var response = await client.GetStringAsync(builder.ToString());
                var data = JsonConvert.DeserializeObject<List<WikipediaItem>>(response);
                Console.WriteLine("Found: {0} Items",(data == null) ? 0 : data.Count);
                this.Results = data;
            }
        }
    }
}
