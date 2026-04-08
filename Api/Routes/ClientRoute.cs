using scheduling_management.Application.UseCases.Client;

namespace scheduling_management.Api.Routes;

public static class ClientRoute
{
    public static WebApplication MapClientRoute(this WebApplication app)
    {
        var g = app.MapGroup("/api/client");
        
        g.MapGet("{id:guid}", async (Guid id, Guid establishmentId, IClientUseCase clientUseCase,CancellationToken ct) =>
        {
            var c = await clientUseCase.GetByIdAsync(id, ct);
            return c;
        });
        
        g.MapGet("", async (Guid establishmentId,IClientUseCase clientUseCase, CancellationToken ct) =>
        {
            var c = await clientUseCase.GetAllAsync(establishmentId, ct);
            return c;
        });
        
        return app;
    }
}