using System.ComponentModel.DataAnnotations;

namespace MyCompany.Domain.Entities
{
    //модель для автопарка
    public class VehicleFleetItem : EntityBase
    {
        [Required(ErrorMessage = "Введите имя модели машины")]
        [Display(Name = "Имя автомобиля")]
        public override string Title { get; set; }

        [Display(Name = "Краткое описание автомобиля")]
        public override string Subtitle { get; set; }

        [Display(Name = "Полное описание автомобиля")]
        public override string Text { get; set; }
    }
}
