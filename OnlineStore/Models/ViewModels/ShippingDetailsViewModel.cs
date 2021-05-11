using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.ViewModels
{
    public class ShippingDetailsViewModel
    {
        [Required(ErrorMessage = "Укажите как вас зовут")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите ваш телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Вставьте первый адрес доставки")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите страну")]
        public string Country { get; set; }

    }
}
