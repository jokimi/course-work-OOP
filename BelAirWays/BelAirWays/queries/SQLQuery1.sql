
select flights.id_flight, date_from, date_to,s1.town[�� ����], s2.town[����],company_name ,count_of_seats-count_of_customers, class, case class when '������' 
then cast(route.length_of_route*company.cost_of_1km + company.cost_of_business as int) else cast(route.length_of_route*company.cost_of_1km + company.cost_of_econom as int)
end ��������� from   flights inner join route 
on flights.route = route.id_route inner join airport s1  on s1.nameAirport = route.id_airport_from  inner join  airport s2 on s2.nameAirport = route.id_airport_to
inner join company on flights.company = company.company_name  inner join  aircrafts on flights.aircraft = aircrafts.name_aircraft


update users set userPassword = 'ewqewq' where 'e' = users.userLogName



select userLogName, Surname,Name, date_from,date_to, s1.town,s2.town, id_ticket from  tickets inner join users on tickets.id_customer = users.userId 
                inner join flights on tickets.id_flight = flights.id_flight inner join route on flights.route = route.id_route  inner join airport s1  on s1.nameAirport = route.id_airport_from 
                 inner join  airport s2 on s2.nameAirport = route.id_airport_to





select flights.id_flight, date_from[���� ������], date_to[���� �������],s1.town[�� ����], s2.town[����],company_name[�������� ��������]  ,count_of_seats[���������� ����], class, case class when '������' 
then cast(route.length_of_route*company.cost_of_1km + company.cost_of_business as int) else cast(route.length_of_route*company.cost_of_1km + company.cost_of_econom as int)
end ��������� from   flights inner join route 
on flights.route = route.id_route inner join airport s1  on s1.nameAirport = route.id_airport_from  inner join  airport s2 on s2.nameAirport = route.id_airport_to
inner join company on flights.company = company.company_name  inner join  aircrafts on flights.aircraft = aircrafts.name_aircraft 

select userLogName, Surname,Name, date_from,date_to, s1.town,s2.town, id_ticket, price from  tickets inner join users on tickets.id_customer = users.userId 
                inner join flights on tickets.id_flight = flights.id_flight inner join route on flights.route = route.id_route  inner join airport s1  on s1.nameAirport = route.id_airport_from 
                 inner join  airport s2 on s2.nameAirport = route.id_airport_to


delete route where id_route = 6
delete flights where route = 3


delete tickets where id_flight = 3

select id_airport_from,id_flight from route  inner join flights on flights.route = route.id_route where id_airport_from = 'QQ' or id_airport_to = 'QQ'


select * from company
select * from flights
select * from route
select * from aircrafts
select * from users
select * from airport
select * from tickets

delete aircrafts where name_aircraft = 'Air'
delete route where id_route = 31
delete users where userLogName = 'e'
delete flights where route = 30
delete airport where nameAirport = '�����������'
delete tickets where id_flight = 38


select userMail, Surname,Name, date_from,date_to, s1.town,s2.town, id_ticket,price from  tickets inner join users on tickets.id_customer = users.userId 
                inner join flights on tickets.id_flight = flights.id_flight inner join route on flights.route = route.id_route  inner join airport s1  on s1.nameAirport = route.id_airport_from 
                 inner join  airport s2 on s2.nameAirport = route.id_airport_to


select id_airport_from,id_flight from route  inner join flights on flights.route = route.id_route where id_airport_to = '����������'

insert into users (userLogName,userPassword,count_of_orders,userMail,bit ) values('���������������','��������','������')

insert into route (id_airport_from, id_airport_to, length_of_route ) values('���������������','��������','������')
delete airport where nameAirport = '���������'

select id_airport_from,id_flight from route  inner join flights on flights.route = route.id_route where id_airport_from = '���������'


insert into tickets values(1,2,'weq','dfds','dsfd','');
delete tickets where id_ticket = 51; 
'1')

select tickets.id_flight,route,id_route from tickets inner join flights on tickets.id_flight = flights.id_flight inner join route on flights.route = route.id_route

select id_route,s1.town,s2.town,length_of_route from route   inner join airport s1  on s1.nameAirport = route.id_airport_from 
                 inner join  airport s2 on s2.nameAirport = route.id_airport_to


select date_from,id_airport_from ,id_airport_to, company, price, class from flights inner join  route on flights.route = route.id_route inner join tickets on  tickets.id_flight = flights.id_flight 

select userMail, Surname,Name, date_from,date_to, s1.town,s2.town, id_ticket,price from  tickets inner join users on tickets.id_customer = users.userId 
                inner join flights on tickets.id_flight = flights.id_flight inner join route on flights.route = route.id_route  inner join airport s1  on s1.nameAirport = route.id_airport_from 
                 inner join  airport s2 on s2.nameAirport = route.id_airport_to

