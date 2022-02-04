using HelloCSharp.Api.Database;
using HelloCSharp.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloCSharp.Rest.Controllers
{
    public abstract class AbstractRepositoryController<TValue> : ControllerBase
        where TValue : Identifiable
    {
        private readonly Repository _repository;

        protected AbstractRepositoryController(Repository repository)
        {
            _repository = repository;
        }
        
        protected delegate IRepository<TValue> Repository();
        
        [HttpGet]
        public List<TValue> GetList()
        {
            return _repository().FindAll();
        }
        
        [HttpGet("{id:int}")]
        public IActionResult GetSingle(int id)
        {
            try
            {
                return Ok(_repository().GetById(id));
            }
            catch (ArgumentException e)
            {
                return NotFound();
            }
        }
        
        // TODO: update and remove are ASYNC
    }
}