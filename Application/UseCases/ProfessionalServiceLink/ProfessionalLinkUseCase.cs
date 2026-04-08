using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Contracts;
using scheduling_management.Domain.Contracts.Repositories;
using scheduling_management.Domain.Entities;

namespace scheduling_management.Application.UseCases.ProfessionalServiceLink;

public class ProfessionalLinkUseCase(IProfessionalServiceRepository repository, IUnitOfWork unitOfWork)
    : IProfessionalLinkUseCase
{
    private readonly IProfessionalServiceRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResponseProfessionalServiceDto> CreateAsync(CreateProfessionalServiceDto request, CancellationToken cancellationToken = default)
    {
        var entity = new ProfessionalService(request.EstablishmentId, request.ProfessionalId, request.ServiceId);
        var created = await _repository.CreateAsync(entity, cancellationToken);
        await _unitOfWork.CommitAsync();
        return MapToDto(created);
    }

    public async Task<PagedResponse<ResponseProfessionalServiceDto>> ListAsync(int page, int pageSize, Guid? establishmentId, Guid? professionalId, Guid? serviceId, CancellationToken cancellationToken = default)
    {
        var all = await _repository.GetAllAsync(Guid.Empty, cancellationToken);
        var query = all.AsQueryable();
        if (establishmentId.HasValue)
            query = query.Where(ps => ps.EstablishmentId == establishmentId.Value);
        if (professionalId.HasValue)
            query = query.Where(ps => ps.ProfessionalId == professionalId.Value);
        if (serviceId.HasValue)
            query = query.Where(ps => ps.ServiceId == serviceId.Value);
        var items = query.Skip((page - 1) * pageSize).Take(pageSize).Select(MapToDto).ToList();
        return new PagedResponse<ResponseProfessionalServiceDto>(items, page, pageSize, query.Count());
    }

    public async Task<ResponseProfessionalServiceDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateProfessionalServiceDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity == null) return false;
        entity.Update(request.ProfessionalId, request.ServiceId);
        _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity == null) return false;
        _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<IReadOnlyList<ResponseProfessionalServiceDto>> SearchAsync(Guid? establishmentId, Guid? professionalId, Guid? serviceId, CancellationToken cancellationToken = default)
    {
        var all = await _repository.GetAllAsync(Guid.Empty, cancellationToken);
        var query = all.AsQueryable();
        if (establishmentId.HasValue)
            query = query.Where(ps => ps.EstablishmentId == establishmentId.Value);
        if (professionalId.HasValue)
            query = query.Where(ps => ps.ProfessionalId == professionalId.Value);
        if (serviceId.HasValue)
            query = query.Where(ps => ps.ServiceId == serviceId.Value);
        return query.Select(MapToDto).ToList();
    }

    private static ResponseProfessionalServiceDto MapToDto(ProfessionalService ps) => new(
        ps.Id,
        ps.EstablishmentId,
        ps.ProfessionalId,
        ps.ServiceId,
        ps.CreatedAtUtc,
        ps.UpdatedAtUtc);
}