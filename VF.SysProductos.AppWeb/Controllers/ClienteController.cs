using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VF.SysProductos.BL;
using VF.SysProductos.EN;
using Rotativa.AspNetCore;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VF.SysProductos.AppWeb.Controllers
{
    public class ClienteController : Controller
    {
        readonly ClienteBL _clienteBL;

        public ClienteController(ClienteBL pClienteBL)
        {
            _clienteBL = pClienteBL;
        }

        // GET: ClienteController
        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteBL.ObtenerTodosAsync();
            return View(clientes);
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Cliente pCliente)
        {
            try
            {
                await _clienteBL.CrearAsync(pCliente);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _clienteBL.ObtenerPorIdAsync(new Cliente { Id = id });
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cliente pCliente)
        {
            try
            {
                await _clienteBL.ModificarAsync(pCliente);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _clienteBL.ObtenerPorIdAsync(new Cliente { Id = id });
            return View(cliente);
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            try
            {
                await _clienteBL.EliminarAsync(new Cliente { Id = id });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> ReporteClientes()
        {
            var clientes = await _clienteBL.ObtenerTodosAsync();
            return new ViewAsPdf("rpCliente", clientes);
        }

        public async Task<JsonResult> ClientesJson()
        {
            var clientes = await _clienteBL.ObtenerTodosAsync();

            var clientesData = clientes
                .Select(c => new
                {
                    nombre = c.Nombre,
                    apellido = c.Apellido,
                    email = c.Email,
                    telefono = c.Telefono,
                    dui = c.DUI
                })
                .ToList();

            return Json(clientesData);
        }

        public async Task<IActionResult> ReporteClienteExcel()
        {
            var clientes = await _clienteBL.ObtenerTodosAsync();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var hojaExcel = package.Workbook.Worksheets.Add("Clientes");
                hojaExcel.Cells["A1"].Value = "Nombre";
                hojaExcel.Cells["B1"].Value = "Apellido";
                hojaExcel.Cells["C1"].Value = "Email";
                hojaExcel.Cells["D1"].Value = "Teléfono";
                hojaExcel.Cells["E1"].Value = "DUI";

                int row = 2;
                foreach (var cliente in clientes)
                {
                    hojaExcel.Cells[row, 1].Value = cliente.Nombre;
                    hojaExcel.Cells[row, 2].Value = cliente.Apellido;
                    hojaExcel.Cells[row, 3].Value = cliente.Email;
                    hojaExcel.Cells[row, 4].Value = cliente.Telefono;
                    hojaExcel.Cells[row, 5].Value = cliente.DUI;
                    row++;
                }

                hojaExcel.Cells["A:E"].AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteClientesExcel.xlsx");
            }
        }

        public async Task<IActionResult> SubirExcelClientes(IFormFile archivoExcel)
        {
            if (archivoExcel == null || archivoExcel.Length == 0)
                return RedirectToAction("Index");

            var clientes = new List<Cliente>();

            using (var stream = new MemoryStream())
            {
                await archivoExcel.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    var hojaExcel = package.Workbook.Worksheets[0];

                    int rowCount = hojaExcel.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var nombre = hojaExcel.Cells[row, 1].Text;
                        var apellido = hojaExcel.Cells[row, 2].Text;
                        var email = hojaExcel.Cells[row, 3].Text;
                        var telefono = hojaExcel.Cells[row, 4].Text;
                        var dui = hojaExcel.Cells[row, 5].Text;

                        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(email))
                            continue;

                        clientes.Add(new Cliente
                        {
                            Nombre = nombre,
                            Apellido = apellido,
                            Email = email,
                            Telefono = telefono,
                            DUI = dui
                        });
                    }
                }
            }

            if (clientes.Count > 0)
            {
                await _clienteBL.AgregarTodosAsync(clientes);
            }

            return RedirectToAction("Index");
        }
    }
}
