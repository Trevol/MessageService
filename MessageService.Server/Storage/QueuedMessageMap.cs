using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MessageService.Common.Model;

namespace MessageService.Server.Storage
{
    internal class QueuedMessageMap : EntityTypeConfiguration<QueuedMessage>
    {
        public QueuedMessageMap()
        {
            ToTable("tQueuedMessage", "mq");
            HasKey(e=>e.Id);
            Property(e => e.Id).IsRequired().HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.Text).IsRequired().HasMaxLength(4000).HasColumnName("Text");
            Property(e => e.Recipients).IsRequired().HasMaxLength(300).HasColumnName("Recipients");
            Property(e => e.MessageType).IsRequired().HasColumnName("MessageType");
            Property(e => e.CreatedOn).IsRequired().HasColumnName("CreatedOn").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            HasMany(e => e.SendAttempts).WithRequired().HasForeignKey(e => e.QueuedMessageId);
        }
    }
}