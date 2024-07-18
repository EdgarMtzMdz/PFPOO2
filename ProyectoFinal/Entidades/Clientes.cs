using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal;

public class Clientes
    {
        public Clientes()
        {
            nombreClientes = string.Empty;
            telNumClientes = string.Empty;
        }

        [Key]
        public Guid idClientes { get; set; }

        public string nombreClientes { get; set; }

        public string telNumClientes { get; set; }

        public DateTime fechaRegistrio { get; set; }

        public int Puntos { get; set; }
    }