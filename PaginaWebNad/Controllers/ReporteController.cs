using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaginaWebNad.Context;
using PaginaWebNad.Models;

namespace PaginaWebNad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly DbsgiceContext _context;

        public ReporteController(DbsgiceContext context)
        {
            _context = context;
        }

        [HttpPost("filtros")]
        public async Task<ActionResult<IEnumerable<ReporteDTO>>> GetReporteFiltrado(
            [FromBody] FiltrosRequest filtros)
        {
            try
            {
                var query = _context.TUsuarioSistemaRols
                    .Include(usr => usr.IIdUsuarioNavigation)
                    .Include(usr => usr.IdSistemaNavigation)
                    .ThenInclude(s => s.TErpgrupoSistemas)
                    .ThenInclude(egs => egs.IdErpgrupoNavigation)
                    .ThenInclude(eg => eg.IIdClienteNavigation)
                    .Include(usr => usr.IIdRolNavigation)
                    .AsQueryable();

                // Filtro por clientes (corregido)
                if (filtros.Clientes != null && filtros.Clientes.Any())
                {
                    query = query.Where(x =>
                        x.IdSistemaNavigation.TErpgrupoSistemas
                            .Any(egs => filtros.Clientes.Contains(
                                egs.IdErpgrupoNavigation.IIdClienteNavigation.SNombre)));
                }

                // Filtro por usuarios
                if (filtros.Usuarios != null && filtros.Usuarios.Any())
                {
                    query = query.Where(x => filtros.Usuarios.Contains(x.IIdUsuarioNavigation.SUsuario));
                }

                var resultados = await query
                    .Select(usr => new
                    {
                        Cliente = usr.IdSistemaNavigation.TErpgrupoSistemas
                            .Select(egs => egs.IdErpgrupoNavigation.IIdClienteNavigation.SNombre)
                            .FirstOrDefault(),
                        Usuario = usr.IIdUsuarioNavigation.SUsuario,
                        Sistema = usr.IdSistemaNavigation.Nomenglatura,
                        Rol = usr.IIdRolNavigation.SRol
                    })
                    .ToListAsync();

                // Filtrado final del cliente mostrado
                if (filtros.Clientes != null && filtros.Clientes.Any())
                {
                    resultados = resultados
                        .Where(x => filtros.Clientes.Contains(x.Cliente))
                        .ToList();
                }

                var reporte = resultados
                    .GroupBy(x => new { x.Cliente, x.Usuario })
                    .Select(g => new ReporteDTO
                    {
                        Cliente = g.Key.Cliente ?? "Sin Cliente",
                        Usuario = g.Key.Usuario,
                        SOX = g.FirstOrDefault(x => x.Sistema == "SOX")?.Rol,
                        SGCE = g.FirstOrDefault(x => x.Sistema == "SGCE")?.Rol,
                        SGRO = g.FirstOrDefault(x => x.Sistema == "SGRO")?.Rol,
                        SGOV = g.FirstOrDefault(x => x.Sistema == "SGOV")?.Rol,
                        SGI = g.FirstOrDefault(x => x.Sistema == "SGI")?.Rol,
                        SGC = g.FirstOrDefault(x => x.Sistema == "SGC")?.Rol,
                        SIA = g.FirstOrDefault(x => x.Sistema == "SIA")?.Rol,
                        SOQVDC = g.FirstOrDefault(x => x.Sistema == "SOQVDC")?.Rol,
                        SSCS = g.FirstOrDefault(x => x.Sistema == "SSCS")?.Rol
                    });

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener datos: {ex.Message}");
            }
        }

        [HttpGet("clientes")]
        public async Task<ActionResult<IEnumerable<string>>> GetClientes([FromQuery] string searchTerm = "")
        {
            var query = _context.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.SNombre.ToLower().Contains(searchTerm.ToLower()));
            }

            return await query
                .Select(c => c.SNombre)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
        }

        [HttpGet("usuarios")]
        public async Task<ActionResult<IEnumerable<string>>> GetUsuarios([FromQuery] string searchTerm = "")
        {
            var query = _context.TUsuarios.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(u => u.SUsuario.ToLower().Contains(searchTerm.ToLower()));
            }

            return await query
                .Select(u => u.SUsuario)
                .Distinct()
                .OrderBy(u => u)
                .ToListAsync();
        }

        [HttpGet("sistemas")]
        public async Task<ActionResult<IEnumerable<string>>> GetSistemas()
        {
            return await _context.CSistemas
                .Select(s => s.Nomenglatura)
                .Distinct()
                .ToListAsync();
        }

        [HttpGet("roles")]
        public async Task<ActionResult<IEnumerable<string>>> GetRoles()
        {
            return await _context.CtRoles
                .Select(r => r.SRol)
                .Distinct()
                .ToListAsync();
        }

        [HttpPost("excel")]
        public async Task<IActionResult> GenerarExcel([FromBody] FiltrosRequest filtros)
        {
            try
            {
                var response = await GetReporteFiltrado(filtros);

                if (!(response.Result is OkObjectResult okResult))
                    return BadRequest("Error al obtener datos para el reporte");

                var reporte = okResult.Value as IEnumerable<ReporteDTO>;

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Reporte de Usuarios");

                // Colores corporativos
                var colorPrimario = XLColor.FromHtml("#1F4E79");  // Azul corporativo oscuro
                var colorSecundario = XLColor.FromHtml("#D0E2F3"); // Azul claro para fondos
                var colorBorde = XLColor.FromHtml("#8EAADB");      // Azul medio para bordes

                // Título del reporte
                worksheet.Cell("A1").Value = "REPORTE DE USUARIOS Y PERMISOS POR SISTEMA";
                var titleRange = worksheet.Range("A1:K1");
                titleRange.Merge();
                titleRange.Style
                    .Font.SetBold(true)
                    .Font.SetFontSize(14)
                    .Font.SetFontColor(XLColor.White)
                    .Fill.SetBackgroundColor(colorPrimario)
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                worksheet.Row(1).Height = 30;

                // Fecha de generación
                worksheet.Cell("A2").Value = $"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}";
                var dateRange = worksheet.Range("A2:K2");
                dateRange.Merge();
                dateRange.Style
                    .Font.SetItalic(true)
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right)
                    .Border.SetBottomBorder(XLBorderStyleValues.Thin)
                    .Border.SetBottomBorderColor(colorBorde);
                worksheet.Row(2).Height = 20;

                // Encabezados de columnas
                var headers = new[] { "Cliente", "Usuario", "SOX", "SGCE", "SGRO",
                                     "SGOV", "SGI", "SGC", "SIA", "SOQVDC", "SSCS" };

                int headerRow = 4;
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = worksheet.Cell(headerRow, i + 1);
                    cell.Value = headers[i];
                    cell.Style
                        .Font.SetBold(true)
                        .Font.SetFontColor(XLColor.White)
                        .Fill.SetBackgroundColor(colorPrimario)
                        .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                        .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                        .Border.SetOutsideBorderColor(colorBorde);
                }
                worksheet.Row(headerRow).Height = 22;

                // Aplicar los datos
                int row = headerRow + 1;
                foreach (var item in reporte)
                {
                    // Alternar colores para filas
                    bool isEvenRow = row % 2 == 0;
                    var rowColor = isEvenRow ? colorSecundario : XLColor.White;

                    worksheet.Cell(row, 1).Value = item.Cliente ?? "Sin cliente";
                    worksheet.Cell(row, 2).Value = item.Usuario ?? "Sin usuario";
                    worksheet.Cell(row, 3).Value = item.SOX ?? "Sin rol";
                    worksheet.Cell(row, 4).Value = item.SGCE ?? "Sin rol";
                    worksheet.Cell(row, 5).Value = item.SGRO ?? "Sin rol";
                    worksheet.Cell(row, 6).Value = item.SGOV ?? "Sin rol";
                    worksheet.Cell(row, 7).Value = item.SGI ?? "Sin rol";
                    worksheet.Cell(row, 8).Value = item.SGC ?? "Sin rol";
                    worksheet.Cell(row, 9).Value = item.SIA ?? "Sin rol";
                    worksheet.Cell(row, 10).Value = item.SOQVDC ?? "Sin rol";
                    worksheet.Cell(row, 11).Value = item.SSCS ?? "Sin rol";

                    // Estilo de fila
                    worksheet.Range(row, 1, row, headers.Length).Style
                        .Fill.SetBackgroundColor(rowColor)
                        .Border.SetBottomBorder(XLBorderStyleValues.Thin)
                        .Border.SetBottomBorderColor(XLColor.FromHtml("#E0E0E0"))
                        .Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                    // Estilos especiales para las columnas Cliente y Usuario
                    worksheet.Cell(row, 1).Style.Font.SetBold(true); // Cliente en negrita
                    worksheet.Cell(row, 2).Style.Font.SetItalic(true); // Usuario en cursiva

                    row++;
                }

                // Aplicar bordes al rango de datos
                var dataRange = worksheet.Range(headerRow, 1, row - 1, headers.Length);
                dataRange.Style
                    .Border.SetOutsideBorder(XLBorderStyleValues.Medium)
                    .Border.SetOutsideBorderColor(colorBorde);

                // Ajustar ancho de columnas
                worksheet.Columns(1, headers.Length).AdjustToContents();
                worksheet.Columns(1, headers.Length).Width = 15; // Ancho mínimo
                worksheet.Column(1).Width = 25; // Cliente más ancho
                worksheet.Column(2).Width = 20; // Usuario más ancho

                // Añadir filtros
                worksheet.Range(headerRow, 1, headerRow, headers.Length).SetAutoFilter();

                // Ajustar el área de impresión
                worksheet.PageSetup.PrintAreas.Add(1, 1, row, headers.Length);
                worksheet.PageSetup.SetRowsToRepeatAtTop(headerRow, headerRow);
                worksheet.PageSetup.PaperSize = XLPaperSize.A4Paper;
                worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                worksheet.PageSetup.CenterHorizontally = true;
                worksheet.PageSetup.Footer.Left.AddText("Confidencial");

                // Pie de página corporativo
                var footerRow = row + 1;
                worksheet.Cell(footerRow, 1).Value = "© Reporte Corporativo de Sistemas";
                var footerRange = worksheet.Range(footerRow, 1, footerRow, headers.Length);
                footerRange.Merge();
                footerRange.Style
                    .Font.SetItalic(true)
                    .Font.SetFontColor(XLColor.Gray)
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                // Guardar el documento
                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;

                return File(stream,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"Reporte_Usuarios_{DateTime.Now:yyyyMMdd}.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al generar Excel: {ex.Message}");
            }
        }

        public class ReporteDTO
        {
            public string Cliente { get; set; }
            public string Usuario { get; set; }
            public string SOX { get; set; }
            public string SGCE { get; set; }
            public string SGRO { get; set; }
            public string SGOV { get; set; }
            public string SGI { get; set; }
            public string SGC { get; set; }
            public string SIA { get; set; }
            public string SOQVDC { get; set; }
            public string SSCS { get; set; }
        }

        public class FiltrosRequest
        {
            public List<string> Clientes { get; set; } = new List<string>();
            public List<string> Usuarios { get; set; } = new List<string>();
        }
    }
}