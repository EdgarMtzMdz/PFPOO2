using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal;

public class RegistryViewModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.PhoneNumber)]
        public int Matricula { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }