using MyCompany.Domain.Repositories.Abstract;

namespace MyCompany.Domain
{
    public class DataManager
    {
        //свойства
        public ITextFieldsRepository TextFields { get; set; }
        public IServiceItemsRepository ServiceItems { get; set; }
        public IVehicleFleetItemsRepository VehicleFleet { get; set; }

        //конструктор
        public DataManager(ITextFieldsRepository textFieldsRepository, 
            IServiceItemsRepository serviceItemsRepository, 
            IVehicleFleetItemsRepository vehicleFleetItemsRepository)
        {
            TextFields = textFieldsRepository;
            ServiceItems = serviceItemsRepository;
            VehicleFleet = vehicleFleetItemsRepository;
        }
    }
}
