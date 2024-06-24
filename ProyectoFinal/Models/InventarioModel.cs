using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoFinal;

public class InventarioModel
{
    public InventarioModel()
    {
    }


    public int codBarras { get; set; }
    public string nombreProducto { get; set; }
    public int cantProducto { get; set; }
    
    public float costoProducto { get; set; }

    public Guid idProveedores { get; set; }

    public ProveedoresModel ProveedoresModel { get; set; }
    public string? nombreProveedores { get; set; }

    public List<SelectListItem> ProoveedoresList { get; set; }


    
}