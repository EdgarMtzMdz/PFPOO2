using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal;

public class LoginViewModel
{
    public LoginViewModel()
    {
        

    }

    

    [Required(ErrorMessage = "El campo {0} es requerido")]
    [EmailAddress(ErrorMessage = "El campo debe ser valido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "El campo{0} es requerido")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Recuerdame")]
    public bool Remember { get; set; }
}