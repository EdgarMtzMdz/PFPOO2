using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal;

public class InventarioController : Controller
{
    public InventarioController()
    {

    }

    public ActionResult inventarioList()
    {
        return View();
    }

   
}