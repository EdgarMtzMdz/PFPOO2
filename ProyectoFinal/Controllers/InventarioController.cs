using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{

    public class InventarioController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<InventarioController> _logger;

        public InventarioController(ApplicationDBContext context, ILogger<InventarioController> logger)
        {
            this._logger = logger;
            this._context = context;
        }


        public async Task<ActionResult> InventarioList()
        {
            List<InventarioModel> inventarios
                = await _context.Inventario
                .Include(s => s.Proveedor)
                .Select(product => new InventarioModel()
                {
                    codBarras = product.codBarras,
                    codProveedor = product.codProveedor,
                    nombreProducto = product.nombreProducto,
                    cantProducto = product.cantProducto,
                    costoProducto = product.costoProducto
                    // nombreProveedor = product.Proveedor.nombreProveedor
                }).ToListAsync();

            return View(inventarios);
        }

        public ActionResult BuscarProducto(string codBarras)
        {
            var producto = _context.Inventario
            .Include(p => p.Proveedor)
            .FirstOrDefault(p => p.codBarras == codBarras);


            if (producto != null)
            {
                var productoModel = new InventarioModel
                {
                    codBarras = producto.codBarras,
                    nombreProducto = producto.nombreProducto,
                    cantProducto = producto.cantProducto,
                    costoProducto = producto.costoProducto,
                    codProveedor = producto.codProveedor,


                    ProveedorList = _context.Proveedor.Select(p => new SelectListItem
                    {
                        Value = p.codProveedor,
                        Text = p.nombreProveedor
                    }).ToList()
                };


                var productosFiltrados = new List<InventarioModel> { productoModel };
                return View("InventarioList", productosFiltrados);
            }
            else
            {
                ViewBag.ErrorMessage = "Producto no encontrado";
                return View("InventarioList", new List<InventarioModel>());
            }
        }


        [HttpPost]
        public ActionResult ActualizarCantidad(string codBarras, int nuevaCantidad)
        {

            var producto = _context.Inventario.FirstOrDefault(p => p.codBarras == codBarras);
            if (producto != null)
            {
                return View(producto);
            }

            producto.cantProducto = producto.cantProducto;

            nuevaCantidad = nuevaCantidad + producto.cantProducto;

            producto.cantProducto = nuevaCantidad;

            this._context.Inventario.Update(producto);
            this._context.SaveChanges();


            return RedirectToAction("InventarioList");
        }

        public IActionResult InventarioAdd()
        {
            InventarioModel product = new InventarioModel();

            product.ProveedorList = _context.Proveedor.Select(p => new SelectListItem()
            {
                Value = p.codProveedor,
                Text = p.nombreProveedor
            }).ToList();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> InventarioAdd(InventarioModel inventarioModel)
        {
            // if (!ModelState.IsValid)
            // {
            //     _logger.LogError("El modelo del producto no es válido");

            //     // inventarioModel.ProveedorList = await _context.Proveedor.Select(p => new SelectListItem()
            //     // { 
            //     //     Value = p.codProveedor, 
            //     //     Text = p.nombreProveedor 
            //     // }).ToListAsync();

            //     return View(inventarioModel);
            // }

            var inventarioEntity = new Inventario
            {
                codBarras = inventarioModel.codBarras,
                codProveedor = inventarioModel.codProveedor,
                nombreProducto = inventarioModel.nombreProducto,
                cantProducto = inventarioModel.cantProducto,
                costoProducto = inventarioModel.costoProducto
            };



            _context.Inventario.Add(inventarioEntity);
            await _context.SaveChangesAsync();

            return RedirectToAction("InventarioList", "Inventario");
        }

        public async Task<IActionResult> InventarioDeleted(string codBarras)
        {
            Inventario? product = await this._context.Inventario
            .Where(p => p.codBarras == codBarras).FirstOrDefaultAsync();

            if (product == null)
            {
                _logger.LogError("No se encontro el producto");
                return RedirectToAction("InventarioList", "Inventario");
            }

            InventarioModel model = new InventarioModel();
            model.codBarras = product.codBarras;
            model.nombreProducto = product.nombreProducto;
            model.cantProducto = product.cantProducto;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> InventarioDeleted(InventarioModel product)
        {
            bool exits = await this._context.Inventario.AnyAsync(p => p.codBarras == product.codBarras);
            if (!exits)
            {
                _logger.LogError("No se encontro el producto");
                return View(product);
            }

            Inventario inventarioEntity = await this._context.Inventario
            .Where(p => p.codBarras == product.codBarras).FirstAsync();

            this._context.Inventario.Remove(inventarioEntity);
            await this._context.SaveChangesAsync();

            return RedirectToAction("InventarioList", "Inventario");
        }

        public async Task<IActionResult> InventarioEdit(string codBarras)
        {
            Inventario? product = await this._context.Inventario
            .Where(p => p.codBarras == codBarras).FirstOrDefaultAsync();

            if (product == null)
            {
                _logger.LogError("No se encontro el producto");
                return RedirectToAction("InventarioList", "Inventario");
            }




            InventarioModel inventarioModel = new InventarioModel();
            inventarioModel.codBarras = product.codBarras;
            // inventarioModel.codProveedor = product.codProveedor;
            inventarioModel.nombreProducto = product.nombreProducto;
            inventarioModel.cantProducto = product.cantProducto;
            inventarioModel.costoProducto = product.costoProducto;




            inventarioModel.ProveedorList = _context.Proveedor.Select(p => new SelectListItem()
            {
                Value = p.codProveedor,
                Text = p.nombreProveedor
            }).ToList();


            return View(inventarioModel);
        }

        [HttpPost]
        public IActionResult InventarioEdit(InventarioModel inventarioModel)
        {
            Inventario InventarioEntity = this._context.Inventario
                 .Where(i => i.codBarras == inventarioModel.codBarras).First();

            if (InventarioEntity == null)
            {
                return View(inventarioModel);
            }

            

            InventarioEntity.nombreProducto = inventarioModel.nombreProducto;
            InventarioEntity.cantProducto = inventarioModel.cantProducto;
            InventarioEntity.costoProducto = inventarioModel.costoProducto;
            InventarioEntity.codProveedor = inventarioModel.codProveedor;
            InventarioEntity.codBarras = inventarioModel.codBarras;

            // Inventario productEntity = await this._context.Inventario
            // .Where(p => p.codBarras == productModel.codBarras).FirstAsync();
            // productEntity.nombreProducto = productModel.nombreProducto;
            // productEntity.cantProducto = productModel.cantProducto;
            // productEntity.costoProducto = productModel.costoProducto;
            // productEntity.codProveedor = productModel.codProveedor;

            this._context.Inventario.Update(InventarioEntity);
            this._context.SaveChanges();

            return RedirectToAction("InventarioList", "Inventario");
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProductosPorProveedor(string codProveedor)
        {
            if (string.IsNullOrEmpty(codProveedor))
            {
                return BadRequest("Código de proveedor no puede estar vacío.");
            }

            var productos = await _context.Inventario
                .Where(i => i.codProveedor == codProveedor)
                .ToListAsync();

            return Json(productos);
        }
    }
}