using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal;

public class Inventario
{
    public Inventario()
    {
        nombreProducto = string.Empty;
        codBarras = string.Empty;
    }

    [Key]
    public string codBarras { get; set; }
    public string nombreProducto { get; set; }
    public int cantProducto { get; set; }
    public float costoProducto { get; set; }

    public string? codProveedor { get; set; }
    public Proveedor? Proveedor { get; set; }
}