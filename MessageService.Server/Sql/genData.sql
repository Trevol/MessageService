use [MessageService]

delete from mq.tQueuedMessageSendAttempt;
delete from mq.tQueuedMessage;

;with id as (
	select row_number() over (order by (select null)) as n
	from sys.columns c1
	cross join sys.columns c2
)

insert into mq.tQueuedMessage([Text], Recipients, MessageType, CreatedOn)
select top 100 
concat('Test Text ', n),
concat('Test Recipients ', n),
(n%3)+1,
DATEADD(day, -300 + n, SYSDATETIME())
from id


;with seq as (
	select top 3 row_number() over (order by (select null)) as n
	from sys.columns c1
	cross join sys.columns c2
)
insert into mq.tQueuedMessageSendAttempt(CreatedOn, QueuedMessageId, Success, ErrorInfo)

select 
DATEADD(minute, -5 + n, SYSDATETIME()),
m.Id,
0,
concat('TEST ERROR ', n)
 from mq.tQueuedMessage m
cross join seq
order by Id
