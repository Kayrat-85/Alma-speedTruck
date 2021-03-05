using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCompany.Domain.Entities;

namespace MyCompany.Domain.Repositories.Abstract
{
    public interface IVehicleFleetItemsRepository
    {
        IQueryable<VehicleFleetItem> GetVehicleFleet();
        VehicleFleetItem GetVehicleFleetById(Guid id);
        void SaveVehicleFleet(VehicleFleetItem entity);
        void DeleteVehicleFleet(Guid id);
    }
}
