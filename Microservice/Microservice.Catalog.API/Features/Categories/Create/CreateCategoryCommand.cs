using MediatR;
using Microservice.Shared;

namespace Microservice.Catalog.API.Features.Categories.Create
{
    public record CreateCategoryCommand(string Name):IRequest<ServiceResult<CreateCategoryResponse>>;
}
