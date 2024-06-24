using Microsoft.EntityFrameworkCore;
namespace ProyectoFinal;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("SqlServer.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Inventario> Inventario { get; set; }
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Empleados> Empleados { get; set; }
    public DbSet<Proveedores> Proveedores { get; set; }
}