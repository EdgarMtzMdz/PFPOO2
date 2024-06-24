using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal;

public class EmpleadosModel
        {
            public EmpleadosModel()
            {
            }

       
            public Guid idEmpleados { get; set; }

            public string nombreEmpleados { get; set; }

            public string Puesto { get; set; }

            public decimal Salario { get; set; }

            public string Email { get; set; }

            public string Telefono { get; set; }

            
        }