namespace ProyectoFinal;

public class VentasListModel
{
    public VentasListModel()
    {
    }

    public string nombreProducto { get; set; }
    public float costoProducto { get; set; }
    public int cantVenta { get; set; }

    public List<InventarioModel> InventarioList { get; set; }
}