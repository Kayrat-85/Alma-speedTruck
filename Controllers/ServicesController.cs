using System;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;

namespace MyCompany.Controllers
{
    public class ServicesController : Controller
    {
        private readonly DataManager dataManager; 
        //конструктор
        public ServicesController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public IActionResult Index(Guid id)
        {
            if (id != default)
            {
                return View("Show", dataManager.ServiceItems.GetServiceItemById(id));
            }
            ViewBag.TextField = dataManager.TextFields.GetTextFieldByCodeWord("PageServices");
            return View(dataManager.ServiceItems.GetServiceItems());
        }

        //вкладка Автопарк
        /*
        public IActionResult Garage(Guid id)
        {
            if (id != default)
            {
                return View("ShowCars", dataManager.VehicleFleet.GetVehicleFleetById(id));
            }
            ViewBag.TextField = dataManager.TextFields.GetTextFieldByCodeWord("PageVehicleFleet");
            return View(dataManager.VehicleFleet.GetVehicleFleet());
        }
        */
    }
}
