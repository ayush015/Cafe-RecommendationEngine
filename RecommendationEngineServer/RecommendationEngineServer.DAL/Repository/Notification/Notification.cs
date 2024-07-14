using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;

namespace RecommendationEngineServer.DAL.Repository.Notification
{
    public class Notification : GenericRepository<Models.Notification>, INotification
    {
        public Notification(DbContext context) : base(context)
        {
        }
    }
}
