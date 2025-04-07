using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VF.SysProductos.DAL;
using VF.SysProductos.EN;
using VF.SysProductos.EN.Filtros;

namespace VF.SysProductos.BL
{
    public class VentaBL
    {
        readonly VentaDAL ventaDAL;

        public VentaBL(VentaDAL pVentaDAL)
        {
            ventaDAL = pVentaDAL;
        }

        public async Task<int> CrearAsync(Venta pVenta)
        {
            return await ventaDAL.CrearAsync(pVenta);
        }

        public async Task<int> AnularAsync(int idVenta)
        {
            return await ventaDAL.AnularAsync(idVenta);
        }

        public async Task<Venta> ObtenerPorIdAsync(int idVenta)
        {
            return await ventaDAL.ObtenerPorIdAsync(idVenta);
        }

        public async Task<List<Venta>> ObtenerTodosAsync()
        {
            return await ventaDAL.ObtenerTodosAsync();
        }

        public async Task<List<Venta>> ObtenerPorEstadoAsync(byte estado)
        {
            return await ventaDAL.ObtenerPorEstadoAsync(estado);
        }

        public async Task<List<Venta>> ObtenerReporteVentasAsync(VentaFiltros filtro)
        {
            return await ventaDAL.ObtenerReporteVentasAsync(filtro);
        }
    }
}
