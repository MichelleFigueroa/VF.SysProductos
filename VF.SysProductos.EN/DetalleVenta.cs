using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VF.SysProductos.EN
{
    public class DetalleVenta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Venta")]
        public int IdVenta { get; set; }

        [Required(ErrorMessage = "El producto es obligatorio.")]
        [ForeignKey("Producto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage = "El subtotal es obligatorio.")]
        [Range(0.01, 9999999.99, ErrorMessage = "El subtotal debe ser mayor a 0.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal SubTotal { get; set; }

        // Relación con Venta
        public virtual Venta? Venta { get; set; }

        // Relación con Producto
        public virtual Producto? Producto { get; set; }
    }
}
