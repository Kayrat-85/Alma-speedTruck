using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;

namespace MyCompany.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataManager dataManager;
        //конструктор
        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public IActionResult Index()
        {
            return View(dataManager.TextFields.GetTextFieldByCodeWord("PageIndex"));
        }
        public IActionResult Contacts()
        {
            return View(dataManager.TextFields.GetTextFieldByCodeWord("PageContacts"));
        }
        public IActionResult Garage(Guid id)
        {
            if (id != default)
            {
                return View("ShowCars", dataManager.VehicleFleet.GetVehicleFleetById(id));
            }
            ViewBag.TextField = dataManager.TextFields.GetTextFieldByCodeWord("PageVehicleFleet");
            return View(dataManager.VehicleFleet.GetVehicleFleet());
        }

        public IActionResult Company_achievements()
        {
            return View(dataManager.TextFields.GetTextFieldByCodeWord("PageCompanyAchievements"));
        }
    }
}
