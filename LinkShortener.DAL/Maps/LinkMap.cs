using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using LinkShortener.Data.Entities;

namespace LinkShortener.DAL.Maps
{
    public class LinkMap : EntityTypeConfiguration<Link>
    {
        public LinkMap()
        {
            HasKey(l => l.Id);

            Property(l => l.OriginalUrl).IsRequired();
            Property(l => l.TokenNumber)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                            new IndexAnnotation(new IndexAttribute()));

            Property(l => l.RowVersion).IsRowVersion();
        }
    }
}
