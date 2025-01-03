namespace MsAcceso.Domain.Root.Reports.HojaDePresupuesto;

public sealed class HojaPresupuesto
{
    private HojaPresupuesto(){}

    private HojaPresupuesto(
        string codPresupuesto,
        string descPresupuesto,
        string codSubPresupuesto,
        string descSubPresupuesto,
        string cliente, 
        string lugar, 
        string fechaCosto,
        List<Titulo> titulos
    )
    {
        CodPresupuesto = codPresupuesto;
        DescPresupuesto = descPresupuesto;
        CodSubPresupuesto = codSubPresupuesto;
        DescSubPresupuesto = descSubPresupuesto;
        Cliente = cliente;
        Lugar = lugar;
        FechaCosto = fechaCosto;
        Titulos = titulos;
    }

    public string CodPresupuesto { get; private set; }
    public string DescPresupuesto { get; private set;}
    public string CodSubPresupuesto {get; private set;}
    public string DescSubPresupuesto {get; private set;}
    public string Cliente {get; private set;}
    public string Lugar {get; private set;}
    public string FechaCosto {get; private set;}
    public List<Titulo> Titulos {get; private set;}

    public static HojaPresupuesto Create(
        string codPresupuesto,
        string descPresupuesto,
        string codSubPresupuesto,
        string descSubPresupuesto,
        string cliente, 
        string lugar, 
        string fechaCosto,
        List<Titulo> titulos
    )
    {

        var nuevoHojaPresupuesto = new HojaPresupuesto(
            codPresupuesto,
            descPresupuesto,
            codSubPresupuesto,
            descSubPresupuesto,
            cliente,
            lugar,
            fechaCosto,
            titulos
        );
        return nuevoHojaPresupuesto;
    }

}

    public record Titulo(
        string item,
        string descripcion,
        decimal parcial,
        List<SubTitulo> subTitulos);

    public record SubTitulo(
        string item,
        string descripcion,
        string? unidad,
        string? metrado,
        decimal precio,
        decimal parcial);

    public record Partida(
        string descripcion,
        string unidad,
        string cuadrilla,
        string cantidad,
        decimal precio,
        decimal parcial);
