create database [Test]
go
use [Test]
go
create table [Role]
(
	RoleID int primary key identity,
	RoleName nvarchar(100) not null
)
go
create table [User]
(
	UserID int primary key identity,
	UserRole int foreign key references [Role](RoleID) not null,
	UserName nvarchar(max) not null,
	UserLogin nvarchar(max),
	UserPassword nvarchar(max)
)
go
create table PickupPoints(
PointID int identity (1,1) not null primary key,
PointAddress nvarchar(max))
go
create table [Order]
(
	OrderID int primary key identity,
	OrderDate datetime not null,
	OrderDeliveryDate datetime not null,
	OrderPickupPoint int foreign key references PickupPoints(PointID),
	OrderClientID int foreign key references [User](UserID),
	OrderRecieveCode int not null,
	OrderStatus nvarchar(max) not null,
)
go
create table Product
(
	ProductArticleNumber nvarchar(100) primary key,
	ProductName nvarchar(max) not null,
	ProductUnit nvarchar(max) not null,
	ProductCost decimal(19,4) not null,
	ProductDiscountAmount tinyint null,
	ProductManufacturer nvarchar(max) not null,
	ProductSupplier nvarchar(max) not null,
	ProductCategory nvarchar(max) not null,
	ProductCurrentDiscount int not null,
	ProductQuantityInStock int not null,
	ProductDescription nvarchar(max) not null,
	ProductPhoto nvarchar(max),
)
go
create table OrderProduct
(
	OrderID int foreign key references [Order](OrderID) not null,
	ProductArticleNumber nvarchar(100) foreign key references Product(ProductArticleNumber) not null,
	ProductQuantity int not null,
	Primary key (OrderID,ProductArticleNumber)
)

insert into [Role](RoleName) values
('Администратор'),
('Менеджер'),
('Клиент')



