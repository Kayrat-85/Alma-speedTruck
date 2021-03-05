using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.Abstract;

namespace MyCompany.Domain.Repositories.EntityFramework
{
    public class EFVehicleFleetItemsRepository : IVehicleFleetItemsRepository
    {
        private readonly AppDbContext context;
        //конструктор
        public EFVehicleFleetItemsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<VehicleFleetItem> GetVehicleFleet()
        {
            return context.VehicleFleets;
        }

        public VehicleFleetItem GetVehicleFleetById(Guid id)
        {
            return context.VehicleFleets.FirstOrDefault(x => x.Id == id);
        }

        public void SaveVehicleFleet(VehicleFleetItem entity)
        {
            if(entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
            }
            else context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteVehicleFleet(Guid id)
        {
            context.VehicleFleets.Remove(new VehicleFleetItem() { Id = id });
            context.SaveChanges();
        }
    }
}
