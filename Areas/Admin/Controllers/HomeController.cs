using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;

namespace MyCompany.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        //для доступа к нашей доменной модели (доменным объектам).
        private readonly DataManager dataManager;

        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public IActionResult Index()
        {
            return View(dataManager.ServiceItems.GetServiceItems());
        }
        public IActionResult TwoPanel()
        {
            return View(dataManager.VehicleFleet.GetVehicleFleet());
        }
    }
}
