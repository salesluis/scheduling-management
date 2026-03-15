using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace scheduling_management.Http.Filters;

/// <summary>
/// Filtro que executa FluentValidation sobre o modelo da requisição quando existe validator registrado.
/// </summary>
public class FluentValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var arg in context.ActionArguments.Values)
        {
            if (arg == null) continue;
            var argType = arg.GetType();
            var validatorType = typeof(IValidator<>).MakeGenericType(argType);
            var validator = context.HttpContext.RequestServices.GetService(validatorType);
            if (validator == null) continue;

            var validateMethod = validatorType.GetMethod("ValidateAsync", new[] { argType, typeof(CancellationToken) });
            if (validateMethod == null) continue;

            var task = (Task)validateMethod.Invoke(validator, new[] { arg, context.HttpContext.RequestAborted })!;
            await task.ConfigureAwait(false);
            var result = task.GetType().GetProperty("Result")?.GetValue(task) as FluentValidation.Results.ValidationResult;
            if (result is { IsValid: false })
            {
                var problem = new ValidationProblemDetails(
                    result.Errors.GroupBy(e => e.PropertyName).ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray()));
                context.Result = new BadRequestObjectResult(problem);
                return;
            }
        }

        await next();
    }
}
