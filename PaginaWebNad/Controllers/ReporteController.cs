using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        // GET: api/Reporte
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TUsuarioSistemaRol>>> GetTUsuarioSistemaRols()
        {
            return await _context.TUsuarioSistemaRols.ToListAsync();
        }

        // GET: api/Reporte/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TUsuarioSistemaRol>> GetTUsuarioSistemaRol(int id)
        {
            var tUsuarioSistemaRol = await _context.TUsuarioSistemaRols.FindAsync(id);

            if (tUsuarioSistemaRol == null)
            {
                return NotFound();
            }

            return tUsuarioSistemaRol;
        }
    }
}
