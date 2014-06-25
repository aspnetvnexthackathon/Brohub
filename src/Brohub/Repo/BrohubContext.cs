using System;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.OptionsModel;

namespace Brohub
{
    public class BrohubContext : DbContext
    {
        public BrohubContext(IServiceProvider serviceProvider,
                             IOptionsAccessor<DbContextOptions> optionsAccessor)
            : base(serviceProvider, optionsAccessor.Options)
        {

        }

        public DbSet<Repository> Repositories { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RepositoryBadge> RepositoryBadges { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Repository>()
                   .Key(e => e.Id);

            builder.Entity<Badge>()
                   .Key(e => e.Id);

            builder.Entity<User>()
                   .Key(u => u.Id);

            builder.Entity<RepositoryBadge>()
                   .Key(e => e.Id)
                   .ForeignKeys(f =>
                   {
                       f.ForeignKey<Repository>(r => r.RepositoryId);
                       f.ForeignKey<Badge>(b => b.BadgeId);
                       f.ForeignKey<User>(u => u.UserId);
                   });

            var repositoryBadge = builder.Model.GetEntityType(typeof(RepositoryBadge));
            repositoryBadge.AddNavigation(new Navigation(repositoryBadge.ForeignKeys
                                                                        .First(k => k.ReferencedEntityType == builder.Model.GetEntityType(typeof(Repository))),
                                                         "Repository",
                                                         true));

            repositoryBadge.AddNavigation(new Navigation(repositoryBadge.ForeignKeys
                                                                        .First(k => k.ReferencedEntityType == builder.Model.GetEntityType(typeof(Badge))),
                                                         "Badge",
                                                         true));

            repositoryBadge.AddNavigation(new Navigation(repositoryBadge.ForeignKeys
                                                                        .First(k => k.ReferencedEntityType == builder.Model.GetEntityType(typeof(User))),
                                                         "User",
                                                         true));
        }
    }
}