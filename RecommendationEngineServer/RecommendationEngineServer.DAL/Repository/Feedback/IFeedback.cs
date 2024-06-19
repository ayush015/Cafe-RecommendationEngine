using RecommendationEngineServer.DAL.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.DAL.Repository.Feedback
{
    public interface IFeedback : IGenericRepository<Models.Feedback>
    {
        Task AddUserFeebacks(IEnumerable<Models.Feedback> feedbacks);
    }
}
