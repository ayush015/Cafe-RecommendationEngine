using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.DAL.Repository.Generic;
using RecommendationEngineServer.DAL.Repository.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.DAL.Repository.Order
{
    public class Order : GenericRepository<Models.Order>, IOrder
    {
        public Order(DbContext context) : base(context)
        {
        }
    }
}
