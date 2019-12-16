using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPAM.RDPreEducationPortal.CoreModels;
using EPAM.RDPreEducationPortal.DataAccess.Interfaces;
using EPAM.RDPreEducationPortal.Web.Services.Interfaces;

namespace EPAM.RDPreEducationPortal.Web.Services.Models
{
    public class GitRepositoriesModel : IGitRepositories
    {

        private readonly IGitRepositoriesData _model;

        //inject dependency
        public GitRepositoriesModel(IGitRepositoriesData model)
        {
            this._model = model;
        }

        #region Implementation of IGitRepositories
        /// <summary>
        /// Method to Add Git Repositories
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Save(GitRepositories model)
        {
            return _model.Save(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gitRepositoryId"></param>
        /// <returns></returns>
        public int UpdateEmailStatus(int gitRepositoryId)
        {
            return _model.UpdateEmailStatus(gitRepositoryId);
        }
        /// <summary>
        /// Repositories List
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GitRepositories> GitRepositoriesList()
        {
            return _model.GetRepositories();
        }
        #endregion
    }
}