using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProyectoFinal.Controllers
{
    public class VentasController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<VentasController> _logger;
        public VentasController(ApplicationDBContext context, ILogger<VentasController> logger)
        {
            this._logger = logger;
            _context = context;
        }

        public async Task<ActionResult> VentasPunto()
        {
            List<InventarioModel> inventarios
                   = await _context.Inventario
                   .Select(product => new InventarioModel()
                   {
                       codBarras = "",
                       nombreProducto = "",
                       costoProducto = 0
                   }).ToListAsync();

            return View(inventarios);
        }

        public ActionResult BuscarVenta(string codBarras)
        {
            var producto = _context.Inventario

            .FirstOrDefault(p => p.codBarras == codBarras);


            if (producto != null)
            {
                var productoModel = new InventarioModel
                {
                    nombreProducto = producto.nombreProducto,
                    costoProducto = producto.costoProducto
                };


                var productosFiltrados = new List<InventarioModel> { productoModel };
                return View("VentasPunto", productosFiltrados);
            }
            else
            {
                ViewBag.ErrorMessage = "Producto no encontrado";
                return View("VentasPunto", new List<InventarioModel>());
            }
        }

        public async Task<IActionResult> VentasList()
        {
            List<InventarioModel> inventarios
                   = await _context.Inventario
                   .Select(product => new InventarioModel()
                   {
                       codBarras = "",
                       nombreProducto = "",
                       costoProducto = 0
                   }).ToListAsync();
            return View(inventarios);
        }

        [HttpPost]
        public IActionResult VentasList(string codBarras, string nombreProducto, float costoProducto)
        {
            var producto =  _context.Inventario
            .FirstOrDefault(p => p.codBarras == codBarras);

            if (producto == null)
            {
                ViewBag.ErrorMessage = "Producto no encontrado";
                return View(producto);
            }
            InventarioModel lista = new InventarioModel()
            {
                codBarras = producto.codBarras,
                nombreProducto = producto.nombreProducto,
                costoProducto = producto.costoProducto
            };

            if (producto.codBarras == lista.codBarras)
            {
                int controlProducto = 0;
                

                if (controlProducto >= 0)
                {

                    controlProducto = controlProducto + 1;
                    var ListaF = new VentasListModel()
                    {
                        nombreProducto = lista.nombreProducto,
                        costoProducto = lista.costoProducto

                    };
                    
                }
            }

            
            return View(producto);
        }

    }
}