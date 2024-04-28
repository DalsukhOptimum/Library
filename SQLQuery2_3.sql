create database Library;
use Library;
create table BookCategory(
Id int primary key IDENTITY(1,1),
CategoryName varchar(35),
CreatedTime datetime
); 

create table Admin (
Id int primary key IDENTITY(1,1),
Email varchar(50),
Password varchar(50)
);
insert into Admin values('naneradalsukh@gamil.com','Dal@2002');
insert into Admin values('sanjaymakvana@gamil.com','San@2002');
insert into Admin values('savansolanki@gamil.com','Sav@2002');
select * from Admin;

create table UserTable(
Id int Primary key IDENTITY(1,1),
Email varchar(50),
Password varchar(50),
CreatedDatetime datetime,
CreatedBy int,
foreign key(CreatedBy) references Admin(Id)
);
drop table UserTable;

create table Book(
Id int primary key IDENTITY(1,1), 
Title varchar(50), 
Author varchar(50), 
Category int, 
Availability int,
BookOwn int, 
CreatedDate datetime ,
UpdatedDate datetime, 
CreatedBy int,
UpdatedBy int,
foreign key(CreatedBy) references Admin(Id),
foreign key(UpdatedBy) references Admin(Id),
foreign key(Category) references BookCategory(Id),
foreign key(BookOwn) references UserTable(Id)
);

select Book.Title, BookCategory.CategoryName, Book.Author from Book inner join BookCategory on BookCategory.Id = Book.Id where Book.Availability = 1 ; 

update Book set Availability = 0 , BookOwn = userId where Id = 1 ;

select * from BookCategory;
select * from Book;
select * from Admin ;
select * from BookCategory;