using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace ProyectoFinal;

public class ApplicationDBContext : IdentityDbContext
{
    public ApplicationDBContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("SqlServer.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Inventario> Inventario { get; set; }
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Empleados> Empleados { get; set; }
    public DbSet<Proveedor> Proveedor { get; set; }
    public IEnumerable<object> User { get; internal set; }
}