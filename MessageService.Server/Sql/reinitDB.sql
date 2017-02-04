use [master]

if DB_ID('MessageService') is not null begin
	drop database MessageService;
end;

create database MessageService;
GO

if exists(select * from sys.server_principals sp where sp.name='MessageService') begin
	drop login MessageService;
end;
CREATE LOGIN MessageService WITH PASSWORD=N'MessageService123', DEFAULT_DATABASE=[MessageService], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON;


use [MessageService]
GO

CREATE USER MessageService FOR LOGIN MessageService;
GO

create schema [mq];
GO

GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::[mq] TO MessageService;
GO

create table mq.tMessageTypeEnum(
	Id tinyint not null,
	constraint PK_tMessageType primary key clustered(Id),
	Name varchar(15) not null
);

insert into mq.tMessageTypeEnum(Id, Name)
values (1, 'Email'), (2, 'Sms'), (3, 'Push');

create table mq.tQueuedMessage(
	Id int not null identity(1,1),
	constraint PK_tQueuedMessage primary key clustered(Id),
	[Text] nvarchar(4000) not null,
	Recipients nvarchar(300) not null,
	
	MessageType tinyint not null,
	constraint FK_QueuedMessage_MessageTypeEnum foreign key(MessageType) references mq.tMessageTypeEnum(Id),
	
	CreatedOn datetime2(3) default(sysdatetime())	
);

create index IX_CreatedOn__Text_Recpt_MessageType on mq.tQueuedMessage(CreatedOn) include([Text], Recipients, MessageType);

create table mq.tQueuedMessageSendAttempt(
	Id int not null identity(1,1),
	constraint PK_tQueuedMessageSendAttempt primary key clustered(Id),
	QueuedMessageId int not null,
	constraint FK_QueuedMessageSendAttempt_QueuedMessage foreign key(QueuedMessageId) references mq.tQueuedMessage(Id),

	Success bit not null,
	ErrorInfo nvarchar(4000) null,
	CreatedOn datetime2(3) default(sysdatetime())	
);

create table mq.tArchivedMessage(
	Id int not null,
	constraint PK_tArchivedMessage primary key clustered(Id),
	[Text] nvarchar(4000) not null,
	Recipients nvarchar(300) not null,
	
	MessageType tinyint not null,
	--constraint FK_ArchivedMessage_MessageTypeEnum foreign key(MessageType) references mq.tMessageTypeEnum(Id),
	QueuedOn datetime2(3),
	ArchivedOn datetime2(3) default(sysdatetime()),
	ArchivedAttempts nvarchar(max) null
)