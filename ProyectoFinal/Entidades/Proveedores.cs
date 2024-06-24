using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal;

public class Proveedores
{
    public Proveedores ()
    {

    }
    [Key]
    public Guid idProveedores { get; set; }
    public int codProveedores { get; set; }
    public string nombreProveedores {get; set; }

    public List<Inventario> Inventario { get; set; }
}