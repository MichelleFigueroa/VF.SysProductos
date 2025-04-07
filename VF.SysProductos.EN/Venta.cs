using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VF.SysProductos.EN
{
    public class Venta
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha de la venta es obligatoria.")]
        public DateTime FechaVenta { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio.")]
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El total de la venta es obligatorio.")]
        [Range(0.00, 999999.99, ErrorMessage = "El total debe ser mayor o igual a 0 y menor a 1,000,000.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        public byte Estado { get; set; } = 1;

        // Relación con Cliente
        public virtual Cliente? Cliente { get; set; }

        // Relación con DetalleVenta
        public virtual ICollection<DetalleVenta>? DetalleVentas { get; set; }
    }

    public enum EnumEstadoVenta
    {
        Activa = 1,
        Anulada = 2
    }
}
