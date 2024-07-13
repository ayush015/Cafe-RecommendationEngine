using RecommendationEngineClient._10_Common.DTO;
using System.Text.Json;

namespace RecommendationEngineClient
{
    public class DateStore
    {
        private static readonly string FilePath = Path.Combine(AppContext.BaseDirectory, "../../../Date.json");

        public static async Task SaveDataAsync(DateDTO data)
        {
            var jsonData = JsonSerializer.Serialize(data);
            await File.WriteAllTextAsync(FilePath, jsonData);
        }

        public static async Task<DateDTO> LoadDataAsync()
        {
            if (!File.Exists(FilePath))
            {
                return null;
            }

            var jsonData = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<DateDTO>(jsonData);
        }

    }
}
