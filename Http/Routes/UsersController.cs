using Microsoft.AspNetCore.Mvc;
using scheduling_management.Application.DTOs;
using scheduling_management.Application.Services;

namespace scheduling_management.Http.Routes;

/// <summary>CRUD e operações de usuários.</summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>Cria um novo usuário.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUserDto request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var response = await _userService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>Lista usuários com paginação.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken cancellationToken = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);
        var response = await _userService.ListAsync(page, pageSize, cancellationToken);
        return Ok(response);
    }

    /// <summary>Busca usuário por ID.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _userService.GetByIdAsync(id, cancellationToken);
        if (response == null) return NotFound();
        return Ok(response);
    }

    /// <summary>Atualiza um usuário.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var found = await _userService.UpdateAsync(id, request, cancellationToken);
        if (!found) return NotFound();
        return NoContent();
    }

    /// <summary>Remove um usuário.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var found = await _userService.DeleteAsync(id, cancellationToken);
        if (!found) return NotFound();
        return NoContent();
    }

    /// <summary>Pesquisa usuários por nome e e-mail.</summary>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] string? email, CancellationToken cancellationToken)
    {
        var response = await _userService.SearchAsync(name, email, cancellationToken);
        return Ok(response);
    }
}
