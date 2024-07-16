using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Entidades;
using ProyectoFinal.Models;

namespace ProyectoFinal;

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
            .Include(s => s.Proveedores)
            .Select(product => new InventarioModel()
            {
                codBarras = product.codBarras,
                codProveedores = product.Proveedores.codProveedores,
                nombreProducto = product.nombreProducto,
                cantProducto = product.cantProducto,
                costoProducto = product.costoProducto,
                nombreProveedores = product.Proveedores.nombreProveedores
            }).ToListAsync();

        return View(inventarios);
    }

    public ActionResult BuscarProducto(int codBarras)
    {
        var producto = _context.Inventario
        .Include(p => p.Proveedores)
        .FirstOrDefault(p => p.codBarras == codBarras);


        if (producto != null)
        {
            var productoModel = new InventarioModel
            {
                codBarras = producto.codBarras,
                nombreProducto = producto.nombreProducto,
                cantProducto = producto.cantProducto,
                costoProducto = producto.costoProducto,
                ProveedoresidProveedores = producto.Proveedores.idProveedores,
                nombreProveedores = producto.Proveedores.nombreProveedores,
                ProveedoresList = _context.Proveedores.Select(p => new SelectListItem
                {
                    Value = p.idProveedores.ToString(),
                    Text = p.nombreProveedores
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
    public ActionResult ActualizarCantidad(int codBarras, int nuevaCantidad)
    {
        var producto = _context.Inventario.FirstOrDefault(p => p.codBarras == codBarras);
        if (producto != null)
        {
            producto.cantProducto = nuevaCantidad;
        }
        return RedirectToAction("InventarioList");
    }

    public async Task<IActionResult> InventarioAdd()
    {
        InventarioModel product = new InventarioModel();

        product.ProveedoresList = _context.Proveedores.Select(p => new SelectListItem()
        {
            Value = p.idProveedores.ToString(),
            Text = p.nombreProveedores
        }).ToList();

        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> InventarioAdd(InventarioModel inventarioModel)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError("El modelo del producto no es válido");

            inventarioModel.ProveedoresList = await _context.Proveedores.Select(p => new SelectListItem()
            { 
                Value = p.idProveedores.ToString(), 
                Text = p.nombreProveedores 
            }).ToListAsync();

            return View(inventarioModel);
        }

        var inventarioEntity = new Inventario();

        inventarioEntity.codBarras = inventarioModel.codBarras;
        inventarioEntity.codProveedor = inventarioModel.codProveedores;
        inventarioEntity.nombreProducto = inventarioModel.nombreProducto;
        inventarioEntity.cantProducto = inventarioModel.cantProducto;
        inventarioEntity.costoProducto = inventarioModel.costoProducto;
        inventarioEntity.Proveedores.idProveedores = inventarioModel.ProveedoresidProveedores;


        _context.Inventario.Add(inventarioEntity);
        await _context.SaveChangesAsync();

        return RedirectToAction("InventarioList", "Inventario");
    }

    public async Task<IActionResult> InventarioDeleted(int codBarras)
    {
        Inventario? product = await this._context.Inventario
        .Where(p => p.codBarras == codBarras).FirstOrDefaultAsync();

        if (product == null)
        {
            _logger.LogError("No se encontro el producto");
            return RedirectToAction("ProductList", "Product");
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

    public async Task<IActionResult> InventarioEdit(int codBarras)
    {
        Inventario? product = await this._context.Inventario
        .Where(p => p.codBarras == codBarras).FirstOrDefaultAsync();

        if (product == null)
        {
            _logger.LogError("No se encontro el producto");
            return RedirectToAction("ProductList", "Product");
        }

        InventarioModel model = new InventarioModel();
        model.codBarras = product.codBarras;
        model.codProveedores = product.codProveedor;
        model.nombreProducto = product.nombreProducto;
        model.cantProducto = product.cantProducto;
        model.costoProducto = product.costoProducto;
        if (product.idProveedores.HasValue)
        {
            model.ProveedoresidProveedores = product.idProveedores.Value;
        }

        model.ProveedoresList =
            await _context.Proveedores.Select(s => new SelectListItem()
            { Value = s.idProveedores.ToString(), Text = s.nombreProveedores }
            ).ToListAsync();


        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> InventarioEdit(InventarioModel productModel)
    {
        bool exits = await this._context.Inventario.AnyAsync(p => p.codBarras == productModel.codBarras);
        if (!exits)
        {
            _logger.LogError("No se encontro el producto");
            return View(productModel);
        }

        if (!ModelState.IsValid)
        {
            productModel.ProveedoresList =
                _context.Proveedores.Select(p => new SelectListItem()
                { Value = p.idProveedores.ToString(), Text = p.nombreProveedores }
                ).ToList();

            _logger.LogError("El modelo no es valido");
            return View(productModel);
        }

        Inventario productEntity = await this._context.Inventario
        .Where(p => p.codBarras == productModel.codBarras).FirstAsync();
        productEntity.nombreProducto = productModel.nombreProducto;
        productEntity.cantProducto = productModel.cantProducto;
        productEntity.costoProducto = productModel.costoProducto;
        productEntity.idProveedores = productModel.ProveedoresidProveedores;

        this._context.Inventario.Update(productEntity);
        await this._context.SaveChangesAsync();

        return RedirectToAction("ProductList", "Product");
    }

    
}
