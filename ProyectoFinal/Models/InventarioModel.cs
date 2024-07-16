using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoFinal;

public class InventarioModel
{
    public InventarioModel()
    {
        ProveedoresList = new List<SelectListItem>();
    }

    public int codBarras { get; set; }

    public int codProveedores {get; set;}

    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "Nombre del Producto")]
    public string nombreProducto { get; set; }
    
    
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0")]
    [Display(Name = "Cantidad")]
    public int cantProducto { get; set; }

    [Display(Name = "Costo")]
    [Range(1, float.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")] 
    public float costoProducto { get; set; }

    public Guid ProveedoresidProveedores {get; set;}
    public ProveedoresModel? ProveedoresModel { get; set; }

    public string? nombreProveedores {get; set;}

    public List<SelectListItem> ProveedoresList { get; set; }
}
