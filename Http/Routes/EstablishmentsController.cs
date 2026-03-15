using Microsoft.AspNetCore.Mvc;
using scheduling_management.Application.DTOs;
using scheduling_management.Application.Services;

namespace scheduling_management.Http.Routes;

/// <summary>CRUD e operações de estabelecimentos.</summary>
[ApiController]
[Route("api/[controller]")]
public class EstablishmentsController : ControllerBase
{
    private readonly IEstablishmentService _establishmentService;

    public EstablishmentsController(IEstablishmentService establishmentService)
    {
        _establishmentService = establishmentService;
    }

    /// <summary>Cria um novo estabelecimento.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateEstablishmentDto request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var response = await _establishmentService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>Lista estabelecimentos com paginação.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken cancellationToken = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);
        var response = await _establishmentService.ListAsync(page, pageSize, cancellationToken);
        return Ok(response);
    }

    /// <summary>Busca estabelecimento por ID.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _establishmentService.GetByIdAsync(id, cancellationToken);
        if (response == null) return NotFound();
        return Ok(response);
    }

    /// <summary>Atualiza um estabelecimento.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEstablishmentDto request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var found = await _establishmentService.UpdateAsync(id, request, cancellationToken);
        if (!found) return NotFound();
        return NoContent();
    }

    /// <summary>Remove um estabelecimento (delete físico).</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var found = await _establishmentService.DeleteAsync(id, cancellationToken);
        if (!found) return NotFound();
        return NoContent();
    }

    /// <summary>Pesquisa estabelecimentos por nome e status.</summary>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] bool? isActive, CancellationToken cancellationToken)
    {
        var response = await _establishmentService.SearchAsync(name, isActive, cancellationToken);
        return Ok(response);
    }

    /// <summary>Ativa um estabelecimento.</summary>
    [HttpPatch("{id:guid}/activate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Activate(Guid id, CancellationToken cancellationToken)
    {
        var found = await _establishmentService.ActivateAsync(id, cancellationToken);
        if (!found) return NotFound();
        return NoContent();
    }

    /// <summary>Desativa um estabelecimento.</summary>
    [HttpPatch("{id:guid}/deactivate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var found = await _establishmentService.DeactivateAsync(id, cancellationToken);
        if (!found) return NotFound();
        return NoContent();
    }
}
