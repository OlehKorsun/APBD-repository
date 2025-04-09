using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zajecie6.Models;

namespace Zajecie6.Controllers
{
    // api/tests => [controller] = Tests    poprostu podstawia Tests zamiast [controller]
    
    [Route("api/[controller]")]
    //[Route("blabla")]
    [ApiController]
    public class TestsController : ControllerBase
    {

        
        
        // GET api/tests
        [HttpGet]    // ważne podać, bo nazwa funkcji nic nie oznacza
        public ActionResult Get()
        {   
            var tests = Database.Tests;
            return Ok(tests);
        }

        // GET api/tests/{id}
        [HttpGet("{id}")]
        //[Route("{id}")]    potem w atrybutach: [FromRouteAttribute]
        public IActionResult GetById( int id)
        {
            var test = Database.Tests.FirstOrDefault(x => x.Id == id);
            return Ok(test);
        }


        // POST api/tests { "id": 4, "name": "Test4" }
        [HttpPost]
        public IActionResult Add(Test test)
        {
            Database.Tests.Add(test);
            return Created("", test);
        }
    }
}



// Trzeba jeszcze skonfigurować kontroller zeby go zarejestrowac


// GET api/tests/1        <= poprawna forma 
// GET api/tests?id=1     <= nie mozemy miec takich samych koncuwek