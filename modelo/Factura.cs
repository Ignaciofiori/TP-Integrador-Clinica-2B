using System;

public class Factura
{
    public int IdFactura { get; set; }
    public Turno Turno { get; set; }   
    public decimal MontoBase { get; set; }
    public decimal CoberturaAplicada { get; set; }
    public decimal DescuentoAplicado { get; set; }
    public decimal MontoTotal { get; set; }
    public DateTime FechaEmision { get; set; }
    public bool Activo { get; set; }
}