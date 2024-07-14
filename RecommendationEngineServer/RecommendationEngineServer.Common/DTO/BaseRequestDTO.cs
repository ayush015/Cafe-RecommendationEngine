namespace RecommendationEngineServer.Common.DTO
{
    public class BaseRequestDTO
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Data { get; set; }
    }
}
