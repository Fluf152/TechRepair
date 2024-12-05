if exists (select name from sys.databases where name = 'GazizullinTechRepair')
begin
    alter database GazizullinTechRepair set single_user with rollback immediate
    use master
    drop database GazizullinTechRepair
end
go
create database GazizullinTechRepair
go 
use GazizullinTechRepair

create table Tech(
    Id int constraint PK_Tech primary key,
    TechName varchar(255) not null 
)

create table TechModel(
    Id int constraint PK_TechModel primary key,
    TechId int constraint Fk_TechModel_To_Tech references Tech(Id),
    TechModelName varchar(255)
)

create table OrderStatusType(
    Id int constraint PK_OrderStatusType primary key,
    StatusName varchar(255)
)

create table UserRole(
    Id int constraint PK_UserRole primary key,
    RoleName varchar(255)
)

create table SystemUser(
    Id int constraint PK_SystemUser primary key,
    LastName varchar(255),
    FirstName varchar(255),
    FatherName varchar(255),
    Phone varchar(11),
    UserLogin varchar(255),
    UserPassword varchar(255),
    UserRoleId int constraint FK_SystemUser_To_UserRole references UserRole(id)
)

create table ClientsOrder(
    Id int constraint PK_ClientsOrder primary key,
    StartDate date,
    ModelId int constraint FK_ClientsOrder_To_TechModel references TechModel(Id),
    ProblemDescription varchar(255),
    OrderStatusId int constraint FK_ClientsOrder_To_OrderStatusType references OrderStatusType(Id),
    CompliteDate date,
    MasterId int constraint FK_ClientsOrder_To_SystemUser_Master references SystemUser(Id),
    ClientId int constraint FK_ClientsOrder_To_SystemUser_Client references SystemUser(Id),   
)

create table OrderMasterComment(
    Id int constraint PK_OrderMasterComment primary key,
    Comment varchar(255),
    OrderId int constraint FK_OrderMasterComment_To_ClientsOrder references ClientsOrder(Id),
)