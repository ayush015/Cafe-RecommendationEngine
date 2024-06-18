using RecommendationEngineClient._30_Services.Chef;

namespace RecommendationEngineClient._30_Services
{
    public class ChefService : IChefService
    {
        private RequestServices _requestServices;

        public ChefService(RequestServices requestServices)
        {
            _requestServices = requestServices;
        }
    }
}
