using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoFinal;

public class InventarioModel
{
    public InventarioModel()
    {
    }


    public string codBarras { get; set; }
    public string nombreProducto { get; set; }
    public int cantProducto { get; set; }
    
    public float costoProducto { get; set; }

    public int? nuevaCantidad { get; set; }

    public ProveedorModel ProveedorModel { get; set; }
    // public string? nombreProveedor { get; set; }
    public string? codProveedor {get; set; }
    public List<SelectListItem> ProveedorList { get; set; }


    
}