using System.Data.Entity;
using System.Linq;
using MessageService.Common.Model;

namespace MessageService.Server.Storage
{
    public class MQDataContext: DbContext
    {
        static MQDataContext()
        {
            Database.SetInitializer<MQDataContext>(null);
        }
        
        public MQDataContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public MQDataContext()
            : this("MessageService")
        {
        }

        public DbSet<QueuedMessage> QueuedMessages { get; set; }
        public DbSet<ArchivedMessage> ArchivedMessages { get; set; }
        public DbSet<QueuedMessageSendAttempt> QueuedMessageSendAttempts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations
                .Add(new QueuedMessageMap())
                .Add(new QueuedMessageSendAttemptMap())
                .Add(new ArchivedMessageMap());
        }

        public ArchivedMessage Archive(QueuedMessage message)
        {
            //load graph
            QueuedMessageSendAttempts.Where(e => e.QueuedMessageId == message.Id).ToArray();
            var archMessage = ArchivedMessages.Add(new ArchivedMessage(message));
            QueuedMessages.Remove(message);
            return archMessage;
        }
    }
}