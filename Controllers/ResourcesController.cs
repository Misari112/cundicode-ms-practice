using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ms_practice.Data;
using ms_practice.Entities;
using ms_practice.Models.Query;
using ms_practice.Models.Response;
using Newtonsoft.Json;

namespace ms_practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ResourcesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Resources
        [HttpGet("resources/{id}")]
        public async Task<ActionResult<IEnumerable<Resources>>> GetResources(string id)
        {
            if (_context.Resources.Any(n => n.IdUser == id) == false)
            {
                return new ObjectResult("No hay recursos creados aún.");
            }
            List<Resources> resources = await _context.Resources.Where(e => e.IdUser == id).ToListAsync();
            List<ResponseResourcesToList> responseResourcesToList = new List<ResponseResourcesToList>();
            for (int i = 0;i<resources.Count;i++) {
                ResponseResourcesToList responseResourcesToList1 = new ResponseResourcesToList();
                responseResourcesToList1.Title = resources.ElementAt(i).Title;
                responseResourcesToList1.Id = resources.ElementAt(i).Id;
                responseResourcesToList1.Summary = resources.ElementAt(i).Summary;
                responseResourcesToList1.Date = resources.ElementAt(i).Date;
                responseResourcesToList.Add(responseResourcesToList1);
            }
            string jsonString = JsonConvert.SerializeObject(responseResourcesToList);
            return Content(jsonString, "application/json");
        }

        // GET: api/Resources/5
        [HttpGet]
        public async Task<ActionResult<Resources>> GetResources()
        {
            if (_context.Resources.ToList() == null)
            {
                return new ObjectResult("No hay recursos creados aún.");
            }
            List<Resources> resources = await _context.Resources.ToListAsync();
            List<ResponseResourcesToList> responseResourcesToList = new List<ResponseResourcesToList>();
            for (int i = 0; i < resources.Count; i++)
            {
                ResponseResourcesToList responseResourcesToList1 = new ResponseResourcesToList();
                responseResourcesToList1.Title = resources.ElementAt(i).Title;
                responseResourcesToList1.Id = resources.ElementAt(i).Id;
                responseResourcesToList1.Summary = resources.ElementAt(i).Summary;
                responseResourcesToList1.Date = resources.ElementAt(i).Date;
                responseResourcesToList.Add(responseResourcesToList1);
            }
            string jsonString = JsonConvert.SerializeObject(responseResourcesToList);
            return Content(jsonString, "application/json");
        }

        [HttpGet("resource/{id}")]
        public async Task<ActionResult<IEnumerable<Resources>>> GetResource(int id)
        {
            if (_context.Resources.Any(n => n.Id == id) == false)
            {
                return new ObjectResult("???");
            }

            ResponseResource responseResource = new ResponseResource();
            Resources resources = await _context.Resources.Where(n => n.Id == id).FirstAsync();
            responseResource.Title = resources.Title;
            responseResource.Content = resources.Content;

            string jsonString = JsonConvert.SerializeObject(responseResource);
            return Content(jsonString, "application/json");
        }

        [HttpPost]
        public async Task<ActionResult<Resources>> PostResources(NewResource newResource)
        {
            if (await _context.Resources.AnyAsync(n => n.Title.Equals(newResource.Title))) {
                return new ObjectResult("Ya existe un recurso con este titulo.");
            }

            Resources resources = new Resources();
            resources.Title = newResource.Title;
            resources.Summary = newResource.Summary;
            resources.Content = newResource.Content;
            resources.Date = DateTime.Now;
            resources.IdUser = newResource.IdUser;

            _context.Resources.Add(resources);
            await _context.SaveChangesAsync();
            return new ObjectResult("Agregado exitosamente.");
        }

    }
}
