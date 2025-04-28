using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("not-found")]
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(); //404
        }
        [HttpGet("server-error")]
        public IActionResult GetServerErrorRequest()
        {
            throw new Exception(); //500
            return Ok(); //200
        }
        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(); //400
        }

        [HttpGet("bad-request/{id}")] 
        public IActionResult GetBadRequest(int id)
        {
            return BadRequest(); //400
        }
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorizedRequest()
        {
            return Unauthorized(); //401
        }

    }
}
