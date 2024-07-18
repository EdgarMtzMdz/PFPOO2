namespace ProyectoFinal;

public class UserViewModel
{
    public UserViewModel()
        {
            
        }
        public string? User { get; set; }

        public string? Email { get; set;}

        public bool Confirmed { get; set; }

        public bool IsAdmin { get; set; }
}