use BelAirWays;

create table discount (
	count_of_order int constraint PK_discount primary key,
	procent int
)

create table history (
	id_history int identity(1,1) constraint PK_history primary key,
	userId int,
	operation nvarchar(200),
	airportFrom nvarchar(100),
	airportTo nvarchar(100),
	date date,
	flightId int constraint FK_flights_history references flights(id_flight)
)

create table users (
	userId int identity(1,1) constraint PK_users primary key,
	userLogName nvarchar(200),
	userPassword nvarchar(500),
	count_of_orders int,
	userMail nvarchar(200) unique,
	bit int default(0)
)

create table aircrafts (
	name_aircraft nvarchar(50) constraint PK_aircrafts primary key,
	count_of_seats int,
	speed int
)

create table company (
	company_name nvarchar(50) constraint PK_company primary key,
	cost_of_1km real,
	cost_of_business real,
	cost_of_econom real
)

create table airport (
	nameAirport nvarchar(50) constraint PK_airport primary key,
	country nvarchar(50),
	town nvarchar(50)
)

create table route (
	id_route int identity(1,1) constraint PK_route primary key,
	id_airport_from nvarchar(50) constraint FK_id_airport_from_route foreign key references airport(nameAirport), --ÑÂßÇÀÒÜ Ñ ÒÀÁËÈÖÅÉ 'airport'
	id_airport_to nvarchar(50) constraint FK_id_airport_to_route foreign key references airport(nameAirport),    --ÑÂßÇÀÒÜ Ñ ÒÀÁËÈÖÅÉ 'airport'
	length_of_route int
)

create table flights (
	id_flight int identity(1,1) constraint PK_flights primary key,
	date_from date,
	date_to date,
	route int constraint FK_id_route_flights foreign key references route(id_route), --ÑÂßÇÀÒÜ Ñ ÒÀÁËÈÖÅÉ 'route'
	company nvarchar(50) constraint FK_company_flights foreign key references company(company_name),  --ÑÂßÇÀÒÜ Ñ ÒÀÁËÈÖÅÉ 'company'
	aircraft nvarchar(50) constraint FK_aircraft_flights foreign key references aircrafts(name_aircraft),   --ÑÂßÇÀÒÜ Ñ ÒÀÁËÈÖÅÉ 'aircrafts'
	count_of_customers int,
	class nvarchar(50)
)

create table tickets (
	id_ticket int identity(1,1) constraint PK_tickets primary key,
	id_customer int constraint FK_id_customer_tickets foreign key references users(userId),  --ÑÂßÇÀÒÜ Ñ ÒÀÁËÈÖÅÉ 'users'
	id_flight int constraint FK_id_flight_tickets foreign key references flights(id_flight),   --ÑÂßÇÀÒÜ Ñ ÒÀÁËÈÖÅÉ 'flights'
	Surname nvarchar(200),
	Name nvarchar(200),
	doc nvarchar(100),
	price int
)