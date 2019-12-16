using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EPAM.RDPreEducationPortal.CoreModels;
using EPAM.RDPreEducationPortal.Web.Services.Interfaces;

namespace EPAM.RDPreEducationPortal.Web.Services.Controllers
{
    public class GitRepositoriesController : ApiController
    {
        private readonly IGitRepositories _model;

        public GitRepositoriesController(IGitRepositories model)
        {
            this._model = model;
        }
        // GET: api/GitRepositories
        public IEnumerable<GitRepositories> Get()
        {
            return _model.GitRepositoriesList();
        }
         
        // POST: api/GitRepositories
        public int Post(GitRepositories model)
        {
            return _model.Save(model);
        }
        [HttpGet]
        public int UpdateEmailStatus(int gitRepositoryId)
        {
            return _model.UpdateEmailStatus(gitRepositoryId);
        }
    }
}
