using System;
using System.ComponentModel.DataAnnotations;
namespace ProyectoFinal;

public class ProveedorModel
{
    public ProveedorModel()
    {
        codProveedor = string.Empty;
        nombreProveedor = string.Empty;
        Inventario = new List<Inventario>();
    }

    public string codProveedor { get; set; }
    
    
    public string nombreProveedor {get; set; }

    public List<Inventario> Inventario { get; set; }
}