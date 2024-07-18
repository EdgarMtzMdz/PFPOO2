using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal;

public class ClientesModel
    {
        public ClientesModel()
        {
        }

       
        public Guid idClientes { get; set; }

        public string nombreClientes { get; set; }

        public string telNumClientes { get; set; }

        public DateTime fechaRegistrio { get; set; }

        public int Puntos { get; set; }
    }