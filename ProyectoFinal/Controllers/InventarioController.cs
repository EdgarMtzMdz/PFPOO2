using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal;

public class InventarioController : Controller
{
    private readonly ApplicationDBContext _context;
    public InventarioController(ApplicationDBContext context)
    {
        _context = context;
    }

    public IActionResult InventarioList()
    {
       List<InventarioModel> InventarioList = _context.Inventario.Select(i => new InventarioModel()
        {
            codBarras = i.codBarras,
            nombreProducto = i.nombreProducto,
            cantProducto = i.cantProducto,
            
            costoProducto = i.costoProducto
        }).ToList();

        
        

        return View(InventarioList);
    }
   
}