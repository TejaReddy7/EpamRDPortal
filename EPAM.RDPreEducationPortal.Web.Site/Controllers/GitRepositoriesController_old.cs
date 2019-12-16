using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPAM.RDPreEducationPortal.CoreModels;
using EPAM.RDPreEducationPortal.Web.Site.Models;

namespace EPAM.RDPreEducationPortal.Web.Site.Controllers
{

    public class GitRepositoriesController_old : Controller
    {
        private GitRepositoriesModel _model;

        public GitRepositoriesController_old()
        {
            _model = new GitRepositoriesModel();
        }

        // GET: GitRepositories/Create
        public ActionResult Create()
        {
            return View(_model);
        }

        // POST: GitRepositories/Create
        [HttpPost]
        public ActionResult Create(GitRepositories model)
        {
            try
            {
                _model.ValidateGithubAccount(model.RepositoryUrl);
                if (string.IsNullOrEmpty(_model.ErrorMessage))
                {
                    _model.Save(model);
                }
                else
                {
                    ModelState.AddModelError("", _model.ErrorMessage);
                }
                return View("Create", _model);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public ActionResult Get()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            _model.Edit(id);
            return View("Create", _model);
        }
        public ActionResult Delete(int id)
        {
            _model.Delete(id);
            return View("Create", _model);
        }
    }
}

