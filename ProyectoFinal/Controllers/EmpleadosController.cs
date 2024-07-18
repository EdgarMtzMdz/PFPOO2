using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal;

public class EmpleadosController : Controller
{
    private readonly ILogger<EmpleadosController> _logger;
    private readonly ApplicationDBContext _context;
    public EmpleadosController(ILogger<EmpleadosController> logger, ApplicationDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult EmpleadosList()
    {
        List <EmpleadosModel> listEmpleados = new List<EmpleadosModel> ();
            listEmpleados=_context.Empleados.Select(s => new EmpleadosModel()
                {
                    idEmpleados = s.idEmpleados,
                    nombreEmpleados = s.nombreEmpleados,
                    Puesto = s.Puesto,
                    Salario = s.Salario,
                    Email = s.Email,
                    Telefono = s.Telefono

                }).ToList();
        return View(listEmpleados);
    }

    public IActionResult EmpleadosAdd()
        {
            
            return View();
        }

    [HttpPost]
        public async Task<IActionResult> EmpleadosAdd (EmpleadosModel empleados)
        {
           


            var empleadosEntity = new Empleados
            {
                idEmpleados = Guid.NewGuid(),
                nombreEmpleados = empleados.nombreEmpleados,
                Puesto =  empleados.Puesto,
                Salario = empleados.Salario,
                Email = empleados.Email,
                Telefono = empleados.Telefono
            };
   
            _context.Empleados.Add(empleadosEntity);
            await _context.SaveChangesAsync();


            return RedirectToAction("EmpleadosList", "Empleados");
        }

        [HttpGet]
        public IActionResult EmpleadosDelete(Guid id)
        {
            Empleados? empleados = this._context.Empleados.Where(p => p.idEmpleados== id).FirstOrDefault();


                if (empleados == null)
                {
                    return RedirectToAction("EmpleadosList", "Empleados");
                }


            EmpleadosModel model = new EmpleadosModel();
            model.idEmpleados = id;
            model.nombreEmpleados = empleados.nombreEmpleados;
            model.Puesto = empleados.Puesto;
            model.Salario= empleados.Salario;
            model.Email = empleados.Email;
            model.Telefono = empleados.Telefono;
       
            return View(model);
        }

        public IActionResult EmpleadosEdit(Guid id)
        {
              var empleados =_context.Empleados.FirstOrDefault(p=>p.idEmpleados==id);
            if(empleados == null)
            {
                return RedirectToAction("EmpleadosList");
            }


            EmpleadosModel model = new EmpleadosModel();
            model.idEmpleados = id;
            model.nombreEmpleados = empleados.nombreEmpleados;
            model.Puesto = empleados.Puesto;
            model.Salario= empleados.Salario;
            model.Email = empleados.Email;
            model.Telefono = empleados.Telefono;


            return View(model);
        }

        [HttpPost]
        public IActionResult EmpleadosDelete(EmpleadosModel empleados)
        {
            bool exits = this._context.Empleados.Any(p => p.idEmpleados == empleados.idEmpleados);
            if(!exits)
            {
                return View(empleados);
            }
            
            Empleados empleadosEntity = this._context.Empleados.Where(p => p.idEmpleados == empleados.idEmpleados).First();


            this._context.Empleados.Remove(empleadosEntity);
            this._context.SaveChanges();


            return RedirectToAction("EmpleadosList", "Empleados");
        }

         [HttpPost]
        public IActionResult EmpleadosEdit(EmpleadosModel model)
        {
               if (ModelState.IsValid)
            {
                var empleados = _context.Empleados.FirstOrDefault(p => p.idEmpleados == model.idEmpleados);
                if (empleados!=null)
                {
                    empleados.nombreEmpleados=model.nombreEmpleados;
                    empleados.Puesto = model.Puesto;
                    empleados.Salario = model.Salario;
                    empleados.Email = model.Email;
                    empleados.Telefono = model.Telefono;


                    _context.SaveChanges();
                    return RedirectToAction("EmpleadosList");
                }
            }


            return View(model);
         
        }
}