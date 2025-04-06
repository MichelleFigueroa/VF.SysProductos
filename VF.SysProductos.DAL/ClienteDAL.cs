using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VF.SysProductos.EN;

namespace VF.SysProductos.DAL
{
    public class ClienteDAL
    {
        readonly VFSyProductosDBContext dbContext;

        public ClienteDAL(VFSyProductosDBContext sysProductosDB)
        {
            dbContext = sysProductosDB;
        }

        public async Task<int> CrearAsync(Cliente pCliente)
        {
            Cliente cliente = new Cliente()
            {
                Nombre = pCliente.Nombre,
                Apellido = pCliente.Apellido,
                Email = pCliente.Email,
                Telefono = pCliente.Telefono,
                DUI = pCliente.DUI
            };

            dbContext.Cliente.Add(cliente);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> EliminarAsync(Cliente pCliente)
        {
            var cliente = await dbContext.Cliente.FirstOrDefaultAsync(p => p.Id == pCliente.Id);
            if (cliente != null)
            {
                dbContext.Cliente.Remove(cliente);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<int> ModificarAsync(Cliente pCliente)
        {
            var cliente = await dbContext.Cliente.FirstOrDefaultAsync(s => s.Id == pCliente.Id);
            if (cliente != null && cliente.Id != 0)
            {
                cliente.Nombre = pCliente.Nombre;
                cliente.Apellido = pCliente.Apellido;
                cliente.Email = pCliente.Email;
                cliente.Telefono = pCliente.Telefono;
                cliente.DUI = pCliente.DUI;

                dbContext.Update(cliente);
                return await dbContext.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<Cliente> ObtenerPorIdAsync(Cliente pCliente)
        {
            var cliente = await dbContext.Cliente.FirstOrDefaultAsync(s => s.Id == pCliente.Id);
            if (cliente != null && cliente.Id != 0)
            {
                return new Cliente
                {
                    Id = cliente.Id,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Email = cliente.Email,
                    Telefono = cliente.Telefono,
                    DUI = cliente.DUI
                };
            }
            else
            {
                return new Cliente();
            }
        }

        public async Task<List<Cliente>> ObtenerTodosAsync()
        {
            return await dbContext.Cliente.ToListAsync();
        }

        public async Task AgregarTodosAsync(List<Cliente> pClientes)
        {
            await dbContext.Cliente.AddRangeAsync(pClientes);
            await dbContext.SaveChangesAsync();
        }
    }
}
