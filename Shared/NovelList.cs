using System.Net.Http.Json;

namespace TungstenCubeTranslations.github.io.Shared
{
    public class NovelList
    {
        private NovelListElement[]? novelList;
        public int Count { get => novelList != null ? novelList.Length : 0; }

        public NovelListElement this[int i]
        {
            get 
            {
                if (novelList == null) return new NovelListElement();
                return novelList[i];
            }
        }

        public async Task<bool> FetchData(HttpClient http)
        {
            using HttpResponseMessage response = await http.GetAsync("novels/novel-list.json");

            if (response.IsSuccessStatusCode)
            {
                novelList = await response.Content.ReadFromJsonAsync<NovelListElement[]>();
                return novelList != null;
            }
            return false;
        }

        public string GetNovelUrl(int i)
        {
            return String.Format("/novel/{0}", this[i].Folder);
        }
    }

    public class NovelListElement
    {
        public string? Title { get; set; }
        public string? Folder { get; set; }
    }
}
