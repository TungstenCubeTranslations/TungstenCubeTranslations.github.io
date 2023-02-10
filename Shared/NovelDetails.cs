using static System.Net.WebRequestMethods;
using System.Net.Http.Json;
using System.Net.Http;


namespace TungstenCubeTranslations.github.io.Shared
{
    public class NovelDetails
    {
        public string Title { get; set; }

        public int[] Chapters { get; set; }

        public NovelDetails()
        {
            Title = "";
            Chapters = Array.Empty<int>();
        }

        public async Task<bool> FetchData(HttpClient http, string? novel)
        {
            if (novel == null) return false;

            using HttpResponseMessage response = await http.GetAsync(String.Format("novels/{0}/details.json", novel));

            var DoesNovelExists = response.IsSuccessStatusCode;
            if (DoesNovelExists)
            {
                var details = await response.Content.ReadFromJsonAsync<NovelDetails[]>();
                if (details != null && details.Length > 0)
                {
                    Title = details[0].Title;
                    Chapters = details[0].Chapters;
                    return true;
                }
            }
            return false;
        }
    }
}
