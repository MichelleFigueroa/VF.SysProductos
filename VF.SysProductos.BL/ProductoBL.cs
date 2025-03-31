using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VF.SysProductos.EN;
using VF.SysProductosVF.SysProductos.DAL;

namespace VF.SysProductos.BL
{
    public class ProductoBL
    {
        private readonly ProductoDAL _ProductoDAL;

        public ProductoBL(ProductoDAL productoDAL)
        {
            _ProductoDAL = productoDAL ?? throw new ArgumentNullException(nameof(productoDAL));
        }

        public async Task<int> CrearAsync(Producto pProducto)
        {
            if (pProducto == null)
                throw new ArgumentNullException(nameof(pProducto));

            return await _ProductoDAL.CrearAsync(pProducto);
        }

        public async Task<int> ModificarAsync(Producto pProducto)
        {
            if (pProducto == null)
                throw new ArgumentNullException(nameof(pProducto));

            return await _ProductoDAL.ModificarAsync(pProducto);
        }

        public async Task<int> EliminarAsync(Producto pProducto)
        {
            if (pProducto == null)
                throw new ArgumentNullException(nameof(pProducto));

            return await _ProductoDAL.EliminarAsync(pProducto);
        }

        public async Task<Producto> ObtenerPorIdAsync(Producto pRol)
        {
            if (pRol == null)
                throw new ArgumentNullException(nameof(pRol));

            return await _ProductoDAL.ObtenerPorIdAsync(pRol);
        }

        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            return await _ProductoDAL.ObtenerTodosAsync();
        }
        public Task AgregarTodosAsync(List<Producto> pProductos)
        {
            return _ProductoDAL.AgregarTodosAsync(pProductos);
        }

    }
}
