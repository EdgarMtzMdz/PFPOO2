using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal;

public class LoginViewModel
{
    [Required(ErrorMessage = "El campo{0} es requerido")]
    public int Matricula { get; set; }

    [Required(ErrorMessage = "El campo{0} es requerido")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Recuerdame")]
    public bool Remember { get; set; }
}