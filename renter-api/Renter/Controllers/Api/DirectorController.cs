using AutoMapper;
using Database.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Renter.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/director")]
    [Authorize]
    public class DirectorController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDirectorRepositoryService directorRepositoryService;

        public DirectorController(IUnitOfWork unitOfWork, IDirectorRepositoryService directorRepositoryService)
        {
            this.unitOfWork = unitOfWork;
            this.directorRepositoryService = directorRepositoryService;
        }

        [HttpGet]
        public IEnumerable<Director> Get() => this.directorRepositoryService.Queryable().Include(x => x.Movies).ToList();

        [HttpGet("{id}")]
        public Director Get(long id) => this.directorRepositoryService.Get(id);
    }
}