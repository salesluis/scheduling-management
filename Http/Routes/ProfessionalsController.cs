using Microsoft.AspNetCore.Mvc;
using scheduling_management.Application.DTOs;
using scheduling_management.Application.Services;

namespace scheduling_management.Http.Routes;

/// <summary>CRUD e operações de profissionais.</summary>
[ApiController]
[Route("api/[controller]")]
public class ProfessionalsController : ControllerBase
{
    private readonly IProfessionalService _professionalService;
    private readonly IAppointmentService _appointmentService;

    public ProfessionalsController(IProfessionalService professionalService, IAppointmentService appointmentService)
    {
        _professionalService = professionalService;
        _appointmentService = appointmentService;
    }

    /// <summary>Cria um novo profissional.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProfessionalDto request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var response = await _professionalService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>Lista profissionais com paginação.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] Guid? establishmentId = null, CancellationToken cancellationToken = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);
        var response = await _professionalService.ListAsync(page, pageSize, establishmentId, cancellationToken);
        return Ok(response);
    }

    /// <summary>Busca profissional por ID.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _professionalService.GetByIdAsync(id, cancellationToken);
        if (response == null) return NotFound();
        return Ok(response);
    }

    /// <summary>Atualiza um profissional.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProfessionalDto request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var found = await _professionalService.UpdateAsync(id, request, cancellationToken);
        if (!found) return NotFound();
        return NoContent();
    }

    /// <summary>Remove um profissional.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var found = await _professionalService.DeleteAsync(id, cancellationToken);
        if (!found) return NotFound();
        return NoContent();
    }

    /// <summary>Pesquisa profissionais.</summary>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromQuery] Guid? establishmentId, [FromQuery] string? displayName, [FromQuery] bool? isActive, CancellationToken cancellationToken)
    {
        var response = await _professionalService.SearchAsync(establishmentId, displayName, isActive, cancellationToken);
        return Ok(response);
    }

    /// <summary>Ativa um profissional.</summary>
    [HttpPatch("{id:guid}/activate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Activate(Guid id, CancellationToken cancellationToken)
    {
        var found = await _professionalService.ActivateAsync(id, cancellationToken);
        if (!found) return NotFound();
        return NoContent();
    }

    /// <summary>Desativa um profissional.</summary>
    [HttpPatch("{id:guid}/deactivate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var found = await _professionalService.DeactivateAsync(id, cancellationToken);
        if (!found) return NotFound();
        return NoContent();
    }

    /// <summary>Lista agendamentos do profissional.</summary>
    [HttpGet("{id:guid}/appointments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAppointments(Guid id, [FromQuery] DateOnly? fromDate, [FromQuery] DateOnly? toDate, CancellationToken cancellationToken)
    {
        var response = await _appointmentService.ListByProfessionalAsync(id, fromDate, toDate, cancellationToken);
        return Ok(response);
    }

    /// <summary>Lista serviços do profissional (relacionamento).</summary>
    [HttpGet("{id:guid}/professional-services")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfessionalServices(Guid id, CancellationToken cancellationToken)
    {
        var response = await _professionalService.ListProfessionalServicesAsync(id, cancellationToken);
        return Ok(response);
    }
}
