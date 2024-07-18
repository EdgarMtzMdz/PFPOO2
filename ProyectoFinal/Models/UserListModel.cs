namespace ProyectoFinal;

public class UserListModel
{
    public UserListModel()
    {
        UserList = new List<UserViewModel>();
        MessageConfirmed = string.Empty;
        MessageRemoved = string.Empty;
    }

    public List<UserViewModel> UserList { get; set; }

        public string MessageConfirmed { get; set; }
        public string MessageRemoved { get; set; }
}