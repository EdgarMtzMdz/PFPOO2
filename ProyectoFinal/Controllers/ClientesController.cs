using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal
{
public class ClientesController : Controller
    {
        private readonly ILogger<ClientesController> _logger;
        private readonly ApplicationDBContext _context;


        public ClientesController(ILogger<ClientesController> logger, ApplicationDBContext context)
        {
            _logger = logger;
            _context = context;
        }
         public IActionResult ClienteList()
        {
            List <ClientesModel> listClientes = new List<ClientesModel> ();
            listClientes=_context.Clientes.Select(s => new ClientesModel()
                {
                    idClientes = s.idClientes,
                    nombreClientes = s.nombreClientes,
                    fechaRegistrio = s.fechaRegistrio,

                }).ToList();


        return View(listClientes);
        }
        public IActionResult ClienteAdd(ClientesModel cliente)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }


            var clienteEntity = new Clientes
            {
                idClientes = Guid.NewGuid(),
                nombreClientes = cliente.nombreClientes,
                fechaRegistrio = cliente.fechaRegistrio,
            };
   
            this._context.Clientes.Add(clienteEntity);
            this._context.SaveChanges();


            return RedirectToAction("ClienteList");
        }
        [HttpGet]
        public IActionResult ClienteDeleted(Guid id)
        {
            Clientes? cliente = this._context.Clientes.Where(p => p.idClientes== id).FirstOrDefault();


                if (cliente == null)
                {
                    return RedirectToAction("ClienteList", "Clientes");
                }


            ClientesModel model = new ClientesModel();
            model.idClientes = id;
            model.nombreClientes = cliente.nombreClientes;
            model.fechaRegistrio = cliente.fechaRegistrio;
       
            return View(model);
        }


        public IActionResult ClienteEdit(Guid id)
        {
              var cliente =_context.Clientes.FirstOrDefault(p=>p.idClientes==id);
            if(cliente == null)
            {
                return RedirectToAction("ClienteList");
            }


            var model = new ClientesModel
            {
                idClientes = cliente.idClientes,
                nombreClientes = cliente.nombreClientes,
                fechaRegistrio = cliente.fechaRegistrio,
            };


            return View(model);
        }


        public IActionResult ClienteSave(ClientesModel model)
        {
            if(ModelState.IsValid)
            {
                var cliente = _context.Clientes.FirstOrDefault(p => p.idClientes==model.idClientes);
                if(cliente!=null)
                {
                    cliente.nombreClientes = model.nombreClientes;
                    cliente.fechaRegistrio = model.fechaRegistrio;


                    _context.SaveChanges();
                    return RedirectToAction("ClienteList");
                }
            }
            return View(model);


        }


        [HttpPost]
        public IActionResult ClienteDeleted(ClientesModel cliente)
        {
            bool exits = this._context.Clientes.Any(p => p.idClientes == cliente.idClientes);
            if(!exits)
            {
                return View(cliente);
            }




            Clientes clienteEntity = this._context.Clientes.Where(p => p.idClientes == cliente.idClientes).First();


            this._context.Clientes.Remove(clienteEntity);
            this._context.SaveChanges();


            return RedirectToAction("ClienteList", "Clientes");
        }


       [HttpPost]
        public IActionResult ClienteEdit(ClientesModel model)
        {
               if (ModelState.IsValid)
            {
                var cliente = _context.Clientes.FirstOrDefault(p => p.idClientes == model.idClientes);
                if (cliente!=null)
                {
                    cliente.nombreClientes=model.nombreClientes;
                    cliente.fechaRegistrio = model.fechaRegistrio;
                    cliente.fechaRegistrio = model.fechaRegistrio;


                    _context.SaveChanges();
                    return RedirectToAction("ClienteList");
                }
            }


            return View(model);
         
        }


   
    }
}