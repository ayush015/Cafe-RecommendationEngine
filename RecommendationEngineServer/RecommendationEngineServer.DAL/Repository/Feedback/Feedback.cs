using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.Feedback
{
    public class Feedback : GenericRepository<Models.Feedback>, IFeedback
    {
        public Feedback(DbContext context) : base(context)
        {
        }
    }
}
