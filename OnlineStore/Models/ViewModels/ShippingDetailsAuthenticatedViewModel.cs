using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.ViewModels
{
    public class ShippingDetailsAuthenticatedViewModel
    {

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Укажите телефон")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Введенный телефон не подходит")]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "Укажите email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
