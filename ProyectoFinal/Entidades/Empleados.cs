using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal;

public class Empleados
        {
            public Empleados()
            {
                nombreEmpleados = string.Empty;
                Puesto = string.Empty;
                Email = string.Empty;
                Telefono = string.Empty;
            }
[Key]
            public Guid idEmpleados { get; set; }

            public string nombreEmpleados { get; set; }

            public string Puesto { get; set; }

            public decimal Salario { get; set; }

            public string Email { get; set; }

            public string Telefono { get; set; }

            
            public int Matricula { get; set; }
        }