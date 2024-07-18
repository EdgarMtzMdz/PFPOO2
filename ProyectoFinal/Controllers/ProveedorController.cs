using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<ProveedorController> _logger;

        public ProveedorController(ApplicationDBContext context, ILogger<ProveedorController> logger)
        {
            this._logger = logger;
            this._context = context;
        }

        public async Task<IActionResult> ProveedorList()
        {
            List<ProveedorModel> proveedor 
            = await _context.Proveedor.Select(proveedor => new ProveedorModel()
            {
                codProveedor = proveedor.codProveedor,
                nombreProveedor = proveedor.nombreProveedor
            }).ToListAsync();

            return View(proveedor);
        }   
        public IActionResult ProveedorAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProveedorAdd(ProveedorModel proveedorModel)
        {
           

            var ProveedorEntity = new Proveedor();
            
            ProveedorEntity.nombreProveedor = proveedorModel.nombreProveedor;
            ProveedorEntity.codProveedor = proveedorModel.codProveedor;

            await this._context.Proveedor.AddAsync(ProveedorEntity);
            this._context.SaveChanges();
            
            return RedirectToAction("ProveedorList","Proveedor");
        }

        public IActionResult ProveedorEdit (string codProveedor)
        {
            Proveedor? proveedor =  this._context.Proveedor
                .Where(p => p.codProveedor == codProveedor).FirstOrDefault();

            if (proveedor == null)
            {
                this._logger.LogWarning("Proveedor no encontrado con el código: " + codProveedor);
                return NotFound(); // O bien, redirigir a una página de error o mostrar un mensaje apropiado
            }

            ProveedorModel model = new ProveedorModel();
            model.codProveedor = proveedor.codProveedor;
            model.nombreProveedor = proveedor.nombreProveedor;

            return View(model);
        }

        [HttpPost]
        public IActionResult ProveedorEdit(ProveedorModel proveedorModel)
        {
            //Carga la información de la BD
            Proveedor ProveedorEntity = this._context.Proveedor
             .Where(p => p.codProveedor == proveedorModel.codProveedor).First();

            //VALIDACION
            if (ProveedorEntity == null)
            {
                return View(proveedorModel);
            }
            
            if (!ModelState.IsValid)
            {
                return View(proveedorModel);
            }

            ProveedorEntity.codProveedor = proveedorModel.codProveedor;
            ProveedorEntity.nombreProveedor = proveedorModel.nombreProveedor;

            this._context.Proveedor.Update(ProveedorEntity);
            this._context.SaveChanges();
             
            return RedirectToAction("ProveedorList","Proveedor");
        }

        public IActionResult ProveedorDeleted(string codProveedor)
        {
            Proveedor? proveedor =  this._context.Proveedor
                .Where(p => p.codProveedor == codProveedor).FirstOrDefault();
            
            if (proveedor == null)
            {
                this._logger.LogWarning("Proveedor no encontrado con el código: " + codProveedor);
                return NotFound(); // O bien, redirigir a una página de error o mostrar un mensaje apropiado
            }


            ProveedorModel model = new ProveedorModel();
            model.codProveedor = proveedor.codProveedor;
            model.nombreProveedor = proveedor.nombreProveedor;

            return View(model);
        }

        [HttpPost]
        public IActionResult ProveedorDeleted(ProveedorModel Proveedor)
        {
            bool exits = this._context.Proveedor.Any(p => p.codProveedor == Proveedor.codProveedor);
            if (!exits)
            {
                return View(Proveedor);
            }


            Proveedor ProveedorEntity = this._context.Proveedor
            .Where(p => p.codProveedor == Proveedor.codProveedor).First();

            this._context.Proveedor.Remove(ProveedorEntity);
            this._context.SaveChanges();
            
            return RedirectToAction("ProveedorList","Proveedor");
        }
        

        public ActionResult ProductPerPorveedor(string codProveedor)
        {
            var producto = _context.Inventario
            .Where(p => p.codProveedor == codProveedor).ToList();



            // if (producto != null)
            // {
            //     var productoModel = new InventarioModel
            //     {
            //         codBarras = producto.codBarras,
            //         nombreProducto = producto.nombreProducto,
            //         cantProducto = producto.cantProducto,
            //         costoProducto = producto.costoProducto,


            //         ProveedorList = _context.Proveedor.Select(p => new SelectListItem
            //         {
            //             Value = p.codProveedor,
            //             Text = p.nombreProveedor
            //         }).ToList()
            //     };


            //     var productosFiltrados = new List<InventarioModel> { productoModel };
            //     return View("ProductPerPorveedor", productosFiltrados);
            // }
            // else
            // {
            //     ViewBag.ErrorMessage = "Producto no encontrado";
            //     return View("ProveedorList", new List<InventarioModel>());
            // }

            return View(producto);
        }

    }


}