using Microsoft.AspNetCore.Mvc;
using scheduling_management.Application.DTOs;
using scheduling_management.Application.Services;

namespace scheduling_management.Http.Routes;

/// <summary>CRUD e operações de serviços.</summary>
[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IServiceCatalogService _serviceCatalogService;

    public ServicesController(IServiceCatalogService serviceCatalogService)
    {
        _serviceCatalogService = serviceCatalogService;
    }

    /// <summary>Cria um novo serviço.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateServiceDto request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var response = await _serviceCatalogService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>Lista serviços com paginação.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] Guid? establishmentId = null, CancellationToken cancellationToken = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);
        var response = await _serviceCatalogService.ListAsync(page, pageSize, establishmentId, cancellationToken);
        return Ok(response);
    }

    /// <summary>Busca serviço por ID.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _serviceCatalogService.GetByIdAsync(id, cancellationToken);
        if (response == null) return NotFound();
        return Ok(response);
    }

    /// <summary>Atualiza um serviço.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateServiceDto request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var found = await _serviceCatalogService.UpdateAsync(id, request, cancellationToken);
        if (!found) return NotFound();
        return NoContent();
    }

    /// <summary>Remove um serviço.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var found = await _serviceCatalogService.DeleteAsync(id, cancellationToken);
        if (!found) return NotFound();
        return NoContent();
    }

    /// <summary>Pesquisa serviços.</summary>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromQuery] Guid? establishmentId, [FromQuery] string? name, [FromQuery] bool? isActive, CancellationToken cancellationToken)
    {
        var response = await _serviceCatalogService.SearchAsync(establishmentId, name, isActive, cancellationToken);
        return Ok(response);
    }

    /// <summary>Ativa um serviço.</summary>
    [HttpPatch("{id:guid}/activate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Activate(Guid id, CancellationToken cancellationToken)
    {
        var found = await _serviceCatalogService.ActivateAsync(id, cancellationToken);
        if (!found) return NotFound();
        return NoContent();
    }

    /// <summary>Desativa um serviço.</summary>
    [HttpPatch("{id:guid}/deactivate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var found = await _serviceCatalogService.DeactivateAsync(id, cancellationToken);
        if (!found) return NotFound();
        return NoContent();
    }
}
