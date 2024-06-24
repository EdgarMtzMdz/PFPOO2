using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal;

public class VentasController : Controller
{
    private readonly ApplicationDBContext _context;
    public VentasController(ApplicationDBContext context)
    {
        _context = context;
    }

    public IActionResult VentasPunto()
    {
        return View();
    }

    
    public IActionResult CalculoTotal ()
    {
        return View();
    }


    public IActionResult MuestraProducto ()
    {
        


        return View();
    }

}