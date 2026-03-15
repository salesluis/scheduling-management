using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Entities;
using scheduling_management.Infrastructure.Repositories;

namespace scheduling_management.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserDto> CreateAsync(CreateUserDto request, CancellationToken cancellationToken = default)
    {
        var entity = new User(request.Name.Trim(), request.Email.Trim().ToLowerInvariant(), request.PhoneNumber?.Trim());
        var created = await _repository.CreateAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateUserDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdForUpdateAsync(id, cancellationToken);
        if (entity == null) return false;
        entity.Update(request.Name.Trim(), request.Email.Trim().ToLowerInvariant(), request.PhoneNumber?.Trim());
        await _repository.UpdateAsync(entity, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity == null) return false;
        await _repository.DeleteAsync(entity, cancellationToken);
        return true;
    }

    public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<PagedResponse<UserDto>> ListAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var paged = await _repository.GetPagedAsync(page, pageSize, cancellationToken);
        var items = paged.Items.Select(MapToDto).ToList();
        return new PagedResponse<UserDto>(items, page, pageSize, paged.TotalCount);
    }

    public async Task<IReadOnlyList<UserDto>> SearchAsync(string? name, string? email, CancellationToken cancellationToken = default)
    {
        var items = await _repository.SearchAsync(name, email, cancellationToken);
        return items.Select(MapToDto).ToList();
    }

    private static UserDto MapToDto(User u) => new(
        u.Id,
        u.Name,
        u.Email,
        u.PhoneNumber,
        u.CreatedAtUtc,
        u.UpdatedAtUtc);
}
