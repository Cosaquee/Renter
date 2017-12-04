using Authorization;
using Database.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Models.Models;
using System.Threading.Tasks;

namespace Renter.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/role")]
    // [Authorize]
    public class RolesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRoleRepositoryService roleRepositoryService;

        public RolesController(IUnitOfWork unitOfWork, IRoleRepositoryService roleRepositoryService)
        {
            this.unitOfWork = unitOfWork;
            this.roleRepositoryService = roleRepositoryService;
        }

        [HttpPost]
        public void CreateRole([FromBody] Role role)
        {
            this.roleRepositoryService.Insert(role);
            unitOfWork.Save();
        }
    }
}
