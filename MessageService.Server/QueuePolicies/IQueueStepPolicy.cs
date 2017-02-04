using System.Linq;
using MessageService.Common.Model;

namespace MessageService.Server.QueuePolicies
{
    public interface IQueueStepPolicy
    {
        QueuedMessage StepMessage(IQueryable<QueuedMessage> queue);
    }
}