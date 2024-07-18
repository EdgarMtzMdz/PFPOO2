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
         public IActionResult ClientesList()
        {
            List <ClientesModel> listClientes = new List<ClientesModel> ();
            listClientes=_context.Clientes.Select(s => new ClientesModel()
                {
                    idClientes = s.idClientes,
                    nombreClientes = s.nombreClientes,
                    telNumClientes = s.telNumClientes,
                    fechaRegistrio = s.fechaRegistrio,
                    Puntos = s.Puntos

                }).ToList();


        return View(listClientes);
        }

        public IActionResult ClientesAdd()
        {
            ClientesModel clientes = new ClientesModel();
            clientes.fechaRegistrio = DateTime.Now;
            return View(clientes);
        }

       [HttpPost]
        public async Task<IActionResult> ClientesAdd (ClientesModel cliente)
        {
            // if (!ModelState.IsValid)
            // {
            //     return View(cliente);
            // }


            var clienteEntity = new Clientes
            {
                idClientes = Guid.NewGuid(),
                nombreClientes = cliente.nombreClientes,
                fechaRegistrio =  cliente.fechaRegistrio,
                telNumClientes = cliente.telNumClientes,
                Puntos = cliente.Puntos
            };
   
            _context.Clientes.Add(clienteEntity);
            await _context.SaveChangesAsync();


            return RedirectToAction("ClientesList", "Clientes");
        }

        [HttpGet]
        public IActionResult ClientesDeleted(Guid id)
        {
            Clientes? cliente = this._context.Clientes.Where(p => p.idClientes== id).FirstOrDefault();


                if (cliente == null)
                {
                    return RedirectToAction("ClientesList", "Clientes");
                }


            ClientesModel model = new ClientesModel();
            model.idClientes = id;
            model.nombreClientes = cliente.nombreClientes;
            model.fechaRegistrio = cliente.fechaRegistrio;
            model.telNumClientes= cliente.telNumClientes;
            model.Puntos = cliente.Puntos;
       
            return View(model);
        }


        public IActionResult ClientesEdit(Guid id)
        {
              var cliente =_context.Clientes.FirstOrDefault(p=>p.idClientes==id);
            if(cliente == null)
            {
                return RedirectToAction("ClientesList");
            }


            var model = new ClientesModel
            {
                idClientes = cliente.idClientes,
                nombreClientes = cliente.nombreClientes,
                fechaRegistrio = cliente.fechaRegistrio,
                telNumClientes = cliente.telNumClientes,
                Puntos = cliente.Puntos
            };


            return View(model);
        }

        [HttpPost]
        public IActionResult ClientesDeleted(ClientesModel cliente)
        {
            bool exits = this._context.Clientes.Any(p => p.idClientes == cliente.idClientes);
            if(!exits)
            {
                return View(cliente);
            }
            
            Clientes clienteEntity = this._context.Clientes.Where(p => p.idClientes == cliente.idClientes).First();


            this._context.Clientes.Remove(clienteEntity);
            this._context.SaveChanges();


            return RedirectToAction("ClientesList", "Clientes");
        }


       [HttpPost]
        public IActionResult ClientesEdit(ClientesModel model)
        {
               if (ModelState.IsValid)
            {
                var cliente = _context.Clientes.FirstOrDefault(p => p.idClientes == model.idClientes);
                if (cliente!=null)
                {
                    cliente.nombreClientes=model.nombreClientes;
                    cliente.fechaRegistrio = model.fechaRegistrio;
                    cliente.fechaRegistrio = model.fechaRegistrio;
                    cliente.telNumClientes = model.telNumClientes;


                    _context.SaveChanges();
                    return RedirectToAction("ClientesList");
                }
            }


            return View(model);
         
        }


   
    }
}