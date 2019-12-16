using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPAM.RDPreEducationPortal.Web.Site.EntityFramework;

namespace EPAM.RDPreEducationPortal.Web.Site.Models
{
    public class TasksModel
    { 
        public List<SelectListItem> GetAllTasks()
        {
            var list = new List<SelectListItem>();
            var entities = new EpamRDPreEducationEntities();
            foreach (var item in entities.Tasks)
            {
                SelectListItem obj = new SelectListItem { Value = item.TaskId.ToString(), Text = item.TaskName };
                list.Add(obj);
            }
            return list;
        }
    }
}