using Application.Behaviors;
using Application.Features.Books;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequisitionValidationPipelineBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionPipelineBehavior<,>));
        
        services.AddControllers()
            .AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<ListBooks.ListBooksQuery>();
                s.DisableDataAnnotationsValidation = true;
            });
        services.AddMediatR(typeof(ListBooks.ListBooksQuery).Assembly);
        
        return services;
    }
}