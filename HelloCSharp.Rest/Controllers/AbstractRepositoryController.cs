using HelloCSharp.Api.Database;
using HelloCSharp.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloCSharp.Rest.Controllers;

public abstract class AbstractRepositoryController<TValue, TSave> : ControllerBase
    where TValue : Identifiable
{
    private readonly Repository _repository;

    protected AbstractRepositoryController(Repository repository)
    {
        _repository = repository;
    }
        
    protected delegate IRepository<TValue, TSave> Repository();
        
    [HttpPost]
    public TValue Post(TSave input)
    {
        return _repository().Create(input);
    }
    
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
        catch (ArgumentException)
        {
            return NotFound();
        }
    }
        
    [HttpPut]
    [Route("{id}")]
    public TValue PutAtId(int id, TSave input)
    {
        return _repository().Update(id, input);
    }

}