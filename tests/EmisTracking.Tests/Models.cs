using AutoMapper;
using EmisTracking.Services.Interfaces;
using EmisTracking.Services.Services;
using System.Linq.Expressions;

namespace EmisTracking.Tests
{
    public class TestEntity : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class TestEntityService : GenericEntityService<TestEntity>
    {
        public TestEntityService(IRepository<TestEntity> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        protected override Expression<Func<TestEntity, object>>[] DependenciesIncludes =>
        [
            entity => entity.Name,
        ];

        protected override Task ValidateAsync(TestEntity item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentNullException(nameof(item.Name), "Name is required");

            return Task.CompletedTask;
        }
    }
}
