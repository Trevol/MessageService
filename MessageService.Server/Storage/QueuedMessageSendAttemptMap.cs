using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MessageService.Common.Model;

namespace MessageService.Server.Storage
{
    internal class QueuedMessageSendAttemptMap : EntityTypeConfiguration<QueuedMessageSendAttempt>
    {
        public QueuedMessageSendAttemptMap()
        {
            ToTable("tQueuedMessageSendAttempt", "mq");
            HasKey(e => e.Id);
            Property(e => e.Id).IsRequired().HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.QueuedMessageId).IsRequired().HasColumnName("QueuedMessageId");
            Property(e => e.Success).IsRequired().HasColumnName("Success");
            Property(e => e.ErrorInfo).IsOptional().HasMaxLength(4000).HasColumnName("ErrorInfo");
            Property(e => e.CreatedOn).IsRequired().HasColumnName("CreatedOn").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
}