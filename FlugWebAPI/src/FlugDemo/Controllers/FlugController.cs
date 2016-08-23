using FlugDemo.Data;
using FlugDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FlugDemo.Api
{
    [Route("api/[controller]")]
    //
    // Dieses Kommentar zum Testen der Authentifizierung entfernen:
    //
    // [Authorize]
    public class FlugController: Controller
    {
        private IFlugRepository repo;

        public FlugController(IFlugRepository repo)
        {
            this.repo = repo;
        }

        // GET api/flug
        /// <summary>
        /// Liefert sämtliche Flüge via GET api/flug
        /// </summary>
        /// <returns>Liste mit Flüge</returns>
        [HttpGet]
        public List<Flug> GetAll() {
            var givenName = User.FindFirst("given_name");
            Debug.WriteLine($"User: {givenName}");
            return repo.FindAll();
        }

        // api/flug/byRoute
        [HttpGet("byRoute")]
        public List<Flug> GetByRoute(string von, string nach) {
            return repo.FindByRoute(von, nach);
        }

        // api/flug/{id}
        [HttpGet("{id}")]
        public Flug GetById(int id) {
            var result = repo.FindById(id);
            return result;
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] Flug flug) {
            repo.Save(flug);

            // /api/flug/{id}
            // /api/flug/17
            return CreatedAtAction("GetById", new { id = flug.Id }, flug);
            // return new AcceptedActionResult();
        }
        
    }
}
