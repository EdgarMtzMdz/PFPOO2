using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal;

public class Clientes
    {
        public Clientes()
        {
        }

[Key]
        public Guid idClientes { get; set; }

        public string nombreClientes { get; set; }

        public DateTime fechaRegistrio { get; set; }

        public int Puntos { get; set; }
    }