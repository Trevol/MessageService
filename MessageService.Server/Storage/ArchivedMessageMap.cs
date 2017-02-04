using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MessageService.Server.Storage
{
    internal class ArchivedMessageMap: EntityTypeConfiguration<ArchivedMessage>
    {
        public ArchivedMessageMap()
        {
            ToTable("tArchivedMessage", "mq");
            HasKey(e => e.Id);
            Property(e => e.Id).IsRequired().HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(e => e.Text).IsRequired().HasMaxLength(4000).HasColumnName("Text");
            Property(e => e.Recipients).IsRequired().HasMaxLength(300).HasColumnName("Recipients");
            Property(e => e.MessageType).IsRequired().HasColumnName("MessageType");
            Property(e => e.QueuedOn).IsRequired().HasColumnName("QueuedOn");
            Property(e => e.ArchivedOn).IsRequired().HasColumnName("ArchivedOn").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(e => e.ArchivedAttempts).IsOptional().HasMaxLength(null).HasColumnName("ArchivedAttempts");
        }
    }
}