using RecommendationEngineServer.Logic.Chef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.Controller
{
    public class EmployeeController
    {
        private IChefLogic _chefLogic;
        public EmployeeController(IChefLogic chefLogic)
        {
            _chefLogic = chefLogic;
        }
    }
}
