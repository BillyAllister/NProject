﻿using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using NProject.Models;
using NProject.Models.Domain;
using NProject.Models.Infrastructure;

namespace NProject.Controllers
{
    [HandleError]
    public class TaskController : Controller
    {
        [Dependency]
        public IAccessPoint AccessPoint { get; set; }

        /// <summary>
        /// Add task to project which specified by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles="PM")]
        public ActionResult AddToProject(int id)
        {
            FillViewAddToProject(id);

            return View();
        }

        private void FillViewAddToProject(int id)
        {
            ViewData["ProjectTitle"] = AccessPoint.Projects.First(p => p.Id == id).Name;

            ViewData["Statuses"] =
                AccessPoint.ProjectStatuses.Select(p => new SelectListItem { Text = p.Name, Value = SqlFunctions.StringConvert((double)p.Id) }).
                    ToList();

            //TODO: maybe add filter on users?
            ViewData["Users"] =
                AccessPoint.Users.Where(u => u.Role.Name == "Programmer").Select(
                    u => new SelectListItem { Text = u.Username, Value = SqlFunctions.StringConvert((double)u.Id) }).
                    ToList();
        }

        [Authorize(Roles = "PM")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToProject(int id, int userId, int statusId, Task t)
        {
            if (!ModelState.IsValid)
            {
                FillViewAddToProject(id);
                return View();
            }
            else
            {
                var project = AccessPoint.Projects.First(p => p.Id == id);
                t.Project = project;
                t.Status = AccessPoint.ProjectStatuses.First(s => s.Id == statusId);
                t.Responsible = AccessPoint.Users.First(u => u.Id == userId);
                t.CreationDate = DateTime.Now;
                project.Tasks.Add(t);

                AccessPoint.SaveChanges();
                return RedirectToAction("list", "Projects");
            }
        }

        //
        // GET: /Task/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Task/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Task/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Task/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Task/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Task/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Task/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
