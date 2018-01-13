using AutoMapper;
using Database.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.Role;
using Models.Models;

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
        public void CreateRole([FromBody] CreateRoleDto createRole)
        {
            var role = Mapper.Map<Role>(createRole);
            this.roleRepositoryService.Insert(role);
            unitOfWork.Save();
        }
    }
}