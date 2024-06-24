using System;
using System.ComponentModel.DataAnnotations;
namespace ProyectoFinal;

public class ProveedoresModel
{
    public Guid idProveedores { get; set; }
    public int codProveedores { get; set; }
    [StringLength(250)]
    
    public string nombreProveedores {get; set; }
}