using Microservice.Catalog.API.Features.Courses;
using Microservice.Catalog.API.Repositories;

namespace Microservice.Catalog.API.Features.Categories
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<Course>? Courses { get; set; }
    }
}
