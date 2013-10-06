using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Data.Interfaces;
using App.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using App.Models.Search;

namespace App.Controllers
{
    [Authorize]
    public class LaptopsController : BaseController
    {
        public LaptopsController (IUowData data)
            :base(data)
        {
	    }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var laptops = this.Data.Laptops.All()
                .Select(LaptopViewModel.FromLaptop);

            return Json(laptops.ToDataSourceResult(request));
        }

        public ActionResult ReadModels(string text)
        {
            var models = this.Data.Laptops.All()
                .Where(l => l.Model.ToLower().Contains(text.ToLower()))
                .Select(l => l.Model)
                .Distinct();

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadManufacturers()
        {
            var manufacturers = this.Data.Manufacturers.All()
                .Select(m => m.Name);

            return Json(manufacturers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(SearchLaptopsViewModel searchModel)
        {
            var laptops = this.Data.Laptops.All();
            if (!string.IsNullOrEmpty(searchModel.Model))
            {
                laptops = laptops
                    .Where(l => 
                        l.Model == searchModel.Model || 
                        l.Model.ToLower().Contains(searchModel.Model.ToLower()));
            }

            if (!string.IsNullOrEmpty(searchModel.Manufacturer) && searchModel.Manufacturer != "All Manufacturers")
            {
                laptops = laptops
                    .Where(l => l.Manufacturer.Name == searchModel.Manufacturer);
            }

            if (searchModel.MaxPrice > 0)
            {
                laptops = laptops.Where(l => l.Price <= (decimal)searchModel.MaxPrice);
            }

            return Json(laptops.Select(LaptopViewModel.FromLaptop));
            //return PartialView("_SearchResultListPartial", laptops.Select(LaptopViewModel.FromLaptop).ToList());
        }
    }
}