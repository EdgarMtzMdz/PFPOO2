using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal;

public class Proveedor
{
    public Proveedor ()
    {
        nombreProveedor = string.Empty;
        codProveedor = string.Empty;
        Inventario = new List<Inventario>();
    }
    [Key]
    public string codProveedor { get; set; }
    
    public string nombreProveedor {get; set; }

    public List<Inventario> Inventario { get; set; }
}