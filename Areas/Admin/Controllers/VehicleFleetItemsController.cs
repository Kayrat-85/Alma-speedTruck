using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using MyCompany.Domain;
using MyCompany.Domain.Entities;
using MyCompany.Service;
using System;
using System.IO;



namespace MyCompany.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VehicleFleetItemsController : Controller
    {
        private readonly DataManager dataManager;
        private readonly IWebHostEnvironment hostingEnvironment;
        public VehicleFleetItemsController(DataManager dataManager, IWebHostEnvironment hostingEnvironment)
        {
            this.dataManager = dataManager;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Edit(Guid id)
        {
            var entity = id == default ? new VehicleFleetItem() : dataManager.VehicleFleet.GetVehicleFleetById(id);
            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(VehicleFleetItem model, IFormFile titleImageFile)
        {
            if (ModelState.IsValid)
            {
                if (titleImageFile != null)
                {
                    model.TitleImagePath = titleImageFile.FileName;
                    using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                dataManager.VehicleFleet.SaveVehicleFleet(model);
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            dataManager.VehicleFleet.DeleteVehicleFleet(id);
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }

    }
}
