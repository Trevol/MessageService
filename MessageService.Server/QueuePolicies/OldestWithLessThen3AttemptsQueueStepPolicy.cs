using System;
using System.Linq;
using System.Linq.Expressions;
using MessageService.Common.Model;

namespace MessageService.Server.QueuePolicies
{
    class OldestWithLessThen3AttemptsQueueStepPolicy : IQueueStepPolicy
    {
        public QueuedMessage StepMessage(IQueryable<QueuedMessage> queue)
        {
            Expression<Func<QueuedMessage, bool>> attemptsPolicyPredicate = e=>e.SendAttempts.Count<3;
            return queue.Where(attemptsPolicyPredicate).OrderBy(e=>e.CreatedOn).FirstOrDefault();
        }
    }
}