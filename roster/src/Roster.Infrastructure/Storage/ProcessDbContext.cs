using System.Collections.Generic;
using System.Linq;
using MassTransit.EntityFrameworkCoreIntegration;
using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roster.Core.Sagas;
using Roster.Core.Storage;

namespace Roster.Infrastructure.Storage
{
    public class ProcessDbContext : SagaDbContext, IProcessSource
    {
        public DbSet<RecruitmentSaga> RecruitmentSaga { get; set; }

        public ProcessDbContext(DbContextOptions<ProcessDbContext> options) : base(options)
        {
        }

        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get
            {
                yield return new RecruitmentSagaMap();
            }
        }

        IQueryable<RecruitmentSaga> IProcessSource.RecruitmentSagas => RecruitmentSaga;

        public class RecruitmentSagaMap : SagaClassMap<RecruitmentSaga>
        {
            protected override void Configure(EntityTypeBuilder<RecruitmentSaga> entity, ModelBuilder model)
            {
            }
        }
    }
}