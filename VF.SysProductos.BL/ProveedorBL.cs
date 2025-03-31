using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VF.SysProductos.DAL;
using VF.SysProductos.EN;
using VF.SysProductosVF.SysProductos.DAL;

namespace VF.SysProductos.BL
{
    public class ProveedorBL
    {
        readonly ProveedorDAL proveedorDAL;

        public ProveedorBL(ProveedorDAL _proveedorDAL)
        {
            proveedorDAL = _proveedorDAL;
        }

        public async Task<int> CrearAsync(Proveedor pProveedor)
        {
            return await proveedorDAL.CrearAsync(pProveedor);
        }


        public async Task<int> ModificarAsync(Proveedor pProveedor)
        {
            return await proveedorDAL.ModificarAsync(pProveedor);
        }

        public async Task<int> EliminarAsync(Proveedor pProveedor)
        {
            return await proveedorDAL.EliminarAsync(pProveedor);
        }

        public async Task<Proveedor> ObtenerPorIdAsync(Proveedor pProveedor)
        {
            return await proveedorDAL.ObtenerPorIdAsync(pProveedor);
        }

        public async Task<List<Proveedor>> ObtenerTodosAsync()
        {
            return await proveedorDAL.ObtenerTodosAsync();
        }
        public Task AgregarTodosAsync(List<Proveedor> pProveedores)
        {
            return proveedorDAL.AgregarTodosAsync(pProveedores);
        }
    }

}
