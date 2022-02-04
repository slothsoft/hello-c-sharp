using HelloCSharp.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloCSharp.Rest.Controllers
{
    [ApiController]
    [Route("api/relationshiptype")]
    public class RelationshipTypeController : ControllerBase
    {
       
        [HttpGet]
        public IEnumerable<RelationshipType> GetList()
        {
            return RelationshipTypeMethods.Values();
        }
        
        [HttpGet("{id}")]
        public IActionResult GetSingle(string id)
        {
            try
            {
                return Ok(RelationshipTypeMethods.ValueOf(id));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}