using Newtonsoft.Json;
using Xunit;

namespace RecommendationEngineServer.Test
{
    public class TestBase : IAsyncLifetime
    {
        public Dictionary<string, object> DataDictionary { get; set; }
        public string dataStorePath = Path.Combine(Directory.GetCurrentDirectory(), "../../../data.json");
        public async Task InitializeAsync()
        {
            if (DataDictionary == null)
            {

                using (var stream = new FileStream(dataStorePath, FileMode.Open, FileAccess.Read))
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var result = await reader.ReadToEndAsync();
                            DataDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                        }
                    }
                    else
                    {
                        throw new FileNotFoundException($"The embedded resource '{dataStorePath}' was not found.");
                    }
                }
            }
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}