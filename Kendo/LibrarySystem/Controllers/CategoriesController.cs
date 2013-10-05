using System.Data.Entity.Validation;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using LibrarySystem.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibrarySystem.Models;

namespace LibrarySystem.Controllers
{
    public class CategoriesController : Controller
    {
        private Models.LibrarySystemEntities db;

        public CategoriesController()
        {
            this.db = new LibrarySystem.Models.LibrarySystemEntities();
        }

        //
        // GET: /Categories/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var categories = db.Categories.Select(CategoryVM.FromCategory);

            return Json(categories.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request,
            Category newCategory)
        {
            if (newCategory != null && ModelState.IsValid)
            {
                try
                {
                    db.Categories.Add(newCategory);
                    db.SaveChanges();
                    return Json("");
                }
                catch (DbEntityValidationException validationError)
                {
                    if (validationError != null)
                    {
                        foreach (var item in validationError.EntityValidationErrors.First().ValidationErrors)
                        {
                            ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                        }
                    }
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request,
            CategoryVM category)
        {
            if (category != null && ModelState.IsValid)
            {
                try
                {
                    db.Entry<Category>(category.ToCategory()).State = 
                        System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();
                    return Json("");
                }
                catch (DbEntityValidationException validationError)
                {
                    if (validationError != null)
                    {
                        foreach (var item in validationError.EntityValidationErrors.First().ValidationErrors)
                        {
                            ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                        }
                    }
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }
	}
}