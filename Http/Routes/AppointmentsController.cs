using Microsoft.AspNetCore.Mvc;
using scheduling_management.Application.DTOs;
using scheduling_management.Application.Services;

namespace scheduling_management.Http.Routes;

/// <summary>CRUD e operações de agendamentos.</summary>
[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    /// <summary>Cria um novo agendamento.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentDto request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var response = await _appointmentService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>Lista agendamentos com paginação.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20, 
        CancellationToken cancellationToken = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);
        var response = await _appointmentService.ListAsync(page, pageSize);
        return Ok(response);
    }

    /// <summary>Busca agendamento por ID.</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _appointmentService.GetByIdAsync(id, cancellationToken);
        if (response == null) return NotFound();
        return Ok(response);
    }

    /// <summary>Atualiza um agendamento (reagendar).</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAppointmentDto request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var found = await _appointmentService.UpdateAsync(id, request, cancellationToken);
        if (!found) return NotFound();
        return NoContent();
    }

    /// <summary>Remove um agendamento.</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var found = await _appointmentService.DeleteAsync(id, cancellationToken);
        if (!found) return NotFound();
        return NoContent();
    }

    /// <summary>Pesquisa agendamentos.</summary>
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] Guid? establishmentId, [FromQuery] Guid? professionalId, [FromQuery] Guid? clientId, [FromQuery] DateOnly? fromDate, [FromQuery] DateOnly? toDate, [FromQuery] int? status, CancellationToken cancellationToken)
    {
        var response = await _appointmentService.SearchAsync(establishmentId, professionalId, clientId, fromDate, toDate, status, cancellationToken);
        return Ok(response);
    }

    /// <summary>Cancela um agendamento.</summary>
    [HttpPatch("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var found = await _appointmentService.CancelAsync(id, cancellationToken);
            if (!found) return NotFound();
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>Marca agendamento como realizado.</summary>
    [HttpPatch("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var found = await _appointmentService.CompleteAsync(id, cancellationToken);
            if (!found) return NotFound();
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
