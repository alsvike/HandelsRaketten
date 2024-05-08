using System.Text.Json;

namespace HandelsRaketten.Services.GenericServices
{
    public class GenericJsonFileService<T>
    {
        public IWebHostEnvironment _webHostEnvironment { get; set; }

        public GenericJsonFileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        private string JsonFileName
        {
            get { return Path.Combine(_webHostEnvironment.WebRootPath, "Data", $"{typeof(T).Name}.json"); }
        }

        public void SaveJson(List<T> obj)
        {
            using (FileStream jsonFileWriter = File.Create(JsonFileName))
            {
                Utf8JsonWriter jsonWriter = new Utf8JsonWriter(jsonFileWriter, new JsonWriterOptions()
                {
                    SkipValidation = false,
                    Indented = true
                });
                JsonSerializer.Serialize<T[]>(jsonWriter, obj.ToArray());
            }
        }

        public IEnumerable<T> GetJson()
        {
            using (StreamReader jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<T[]>(jsonFileReader.ReadToEnd());
            }
        }
    }
}
