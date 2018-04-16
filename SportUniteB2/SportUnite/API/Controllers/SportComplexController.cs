using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportUnite.BLL.Abstract;
using SportUnite.Domain.Models;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/SportComplex")]
    public class SportComplexController : Controller
    {

        private readonly ISportComplexAccess _complexAccess;

        public SportComplexController(ISportComplexAccess complexAccess)
        {
            _complexAccess = complexAccess;
        }

        

        [HttpGet]
        public IActionResult Get()
		{ 
	        return Ok(_complexAccess.GetSportComplexes());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
	        var result = _complexAccess.GetSportComplex(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] SportComplex sportComplex)
        {
            _complexAccess.AddSportComplex(sportComplex);
            return Ok(sportComplex);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
	        _complexAccess.DeleteSportComplex(id);
	        return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] SportComplex sportComplex)
        {
            _complexAccess.UpdateSportComplex(sportComplex);
	        return Ok(_complexAccess.GetSportComplex(sportComplex.SportComplexId));
        }


    }
}