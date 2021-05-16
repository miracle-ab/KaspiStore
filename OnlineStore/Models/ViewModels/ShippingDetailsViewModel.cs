using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.ViewModels
{
    public class ShippingDetailsViewModel
    {
        [Required(ErrorMessage = "Укажите имя")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Вставьте адрес доставки")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите страну")]
        public Country Country { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Укажите телефон")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Введенный телефон не подходит")]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "Укажите почту")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+[A-Za-z0-9.-]+\.[A-Za-z] {2,4}", ErrorMessage = "Введенная почта не подходит")]
        public string Email { get; set; }

    }

    public enum Country
    {
        US,
        CA,
        FR,
        DE,
        AU,
        GB
    }
}
