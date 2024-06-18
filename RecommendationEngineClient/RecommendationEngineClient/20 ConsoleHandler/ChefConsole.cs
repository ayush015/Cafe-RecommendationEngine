using RecommendationEngineClient._30_Services;
using RecommendationEngineClient._30_Services.Chef;

namespace RecommendationEngineClient._20_ConsoleHandler
{
    public class ChefConsole
    {
        IChefService _chefService;
        public ChefConsole(RequestServices requestServices)
        {
            _chefService = new ChefService(requestServices);
        }
    }
}
