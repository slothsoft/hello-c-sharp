using HelloCSharp.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloCSharp.Rest.Controllers;

[ApiController]
[Route("api/relationshiptype")]
public class RelationshipTypeController : ControllerBase
{
       
    [HttpGet]
    public IEnumerable<RelationshipTypeDto> GetList()
    {
        return RelationshipTypeMethods.Values().Select(ToDto);
    }

    private static RelationshipTypeDto ToDto(RelationshipType relationshipType)
    {
        return new RelationshipTypeDto
        {
            Id = relationshipType.ToString(),
            DisplayName = relationshipType.GetDisplayName()
        };
    }

    [HttpGet("{id}")]
    public IActionResult GetSingle(string id)
    {
        try
        {
            return Ok(ToDto(RelationshipTypeMethods.ValueOf(id)));
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}

public struct RelationshipTypeDto
{
    public string Id { get; init; }
    
    public string DisplayName { get; init; }
}