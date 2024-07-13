using Microsoft.AspNetCore.Mvc;
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
            List<ProveedoresModel> categories 
            = await _context.Proveedores.Select(Proveedor => new ProveedoresModel()
            {
                idProveedores = Proveedor.idProveedores,
                nombreProveedores = Proveedor.nombreProveedores,
                codProveedores = Proveedor.codProveedores
            }).ToListAsync();

            return View(categories);
        }   

        public IActionResult ProveedorAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProveedorAdd(ProveedoresModel Proveedor)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("No es valido el modelo de proveedores");
                return View(Proveedor);
            }

            var SupplierEntity = new Proveedores();
            SupplierEntity.idProveedores = new Guid();
            SupplierEntity.nombreProveedores = Proveedor.nombreProveedores;
            SupplierEntity.codProveedores = Proveedor.codProveedores;

            await this._context.Proveedores.AddAsync(SupplierEntity);
            this._context.SaveChanges();
            
            return RedirectToAction("ProveedorList","Proveedor");
        }

        public IActionResult ProveedorEdit(Guid Id)
        {
            Proveedores? Proveedor = this._context.Proveedores
                .Where(p => p.idProveedores == Id).FirstOrDefault();
             
            if (Proveedor == null)
            {
                return RedirectToAction("ProveedorList","Proveedor");
            }

            ProveedoresModel model = new ProveedoresModel();
            model.idProveedores = Proveedor.idProveedores;
            model.nombreProveedores = Proveedor.nombreProveedores;

            return View(model);
        }

        [HttpPost]
        public IActionResult ProveedorEdit(ProveedoresModel Proveedor)
        {
            //Carga la información de la BD
            Proveedores ProveedorEntity = this._context.Proveedores
             .Where(p => p.idProveedores == Proveedor.idProveedores).First();

            //VALIDACION
            if (ProveedorEntity == null)
            {
                return View(Proveedor);
            }
            
            if (!ModelState.IsValid)
            {
                return View(Proveedor);
            }

            ProveedorEntity.nombreProveedores = Proveedor.nombreProveedores;

            this._context.Proveedores.Update(ProveedorEntity);
            this._context.SaveChanges();
             
            return RedirectToAction("ProveedorList","Proveedor");
        }

        public IActionResult ProveedorDeleted(Guid Id)
        {
            Proveedores? Proveedor = this._context.Proveedores
            .Where(p => p.idProveedores == Id).FirstOrDefault();
            
            if (Proveedor == null)
            {
                return RedirectToAction("ProveedorList","Proveedor");
            }


            ProveedoresModel model = new ProveedoresModel();
            model.idProveedores = Proveedor.idProveedores;
            model.nombreProveedores = Proveedor.nombreProveedores;

            return View(model);
        }

        [HttpPost]
        public IActionResult ProveedorDeleted(ProveedoresModel Proveedor)
        {
            bool exits = this._context.Proveedores.Any(p => p.idProveedores == Proveedor.idProveedores);
            if (!exits)
            {
                return View(Proveedor);
            }


            Proveedores ProveedorEntity = this._context.Proveedores
            .Where(p => p.idProveedores == Proveedor.idProveedores).First();

            this._context.Proveedores.Remove(ProveedorEntity);
            this._context.SaveChanges();
            
            return RedirectToAction("ProveedorList","Proveedor");
        }

    }
}