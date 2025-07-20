create database CNAP;
use CNAP;


create table Address
(ID int primary key,
City varchar(100),
Street varchar(100),
District varchar(100),
Zip_code varchar(5));


create table Services
(ID int primary key,
Name varchar(50),
deadline int, 
Category varchar (100),
Price numeric(10,2 )); 

create table CNAP
(ID int primary key,
AddressID int,  
Contact varchar(150),
Main_Affiliate bool,
foreign key (AddressID) references Address (ID)  
);

create table CNAP_Services
(
    CNAPID int,
    ServicesID int,
    primary key (CNAPID, ServicesID),
    foreign key (CNAPID) references CNAP (ID),
    foreign key (ServicesID) references Services (ID)
);

create table Client
(
ID int primary key,
Birthday varchar (100),
Surname varchar (100),
Name varchar (50),
Middle_name varchar(100),
PassportID varchar (100),
IdentificationID varchar (100),
Place_of_residence varchar (100),
Contact varchar (150)
) ;

create table Employee
(
ID int primary key,
Surname varchar (100),
Name varchar (50),
Middle_name varchar (100),
Office varchar (100),
Department varchar (100),
Contact varchar (150),
CNAPID int,
foreign key (CNAPID) references CNAP (ID)
);

create table TypeOfStatus
(
ID int primary key,
Name varchar (20)
);

create table Statement
(
ID int primary key,
ClientID int,
ServicesID int,
EmployeeID int,
Date_of_application varchar (100),
TypeOfStatusID int,
CNAPID int,
foreign key (ClientID) references Client (ID),
foreign key (ServicesID) references Services (ID),
foreign key (EmployeeID) references Employee(ID),
foreign key (TypeOfStatusID) references TypeOfStatus (ID),
foreign key (CNAPID) references CNAP(ID)
);

create table TypeOfDocument
(
ID int primary key,
Name varchar (100)
);

create table Document
(
ID int primary key,
StatementID int,
TypeOfDocumentID int,
foreign key (StatementID) references Statement (ID),
foreign key (TypeOfDocumentID) references TypeOfDocument (ID)
);

insert into Address (ID,City, Street,District,Zip_code) values
('1', 'Lviv', 'jakas tam', 'jakis rayon', '23451'),
('2', 'Lviv', 'jakas tam1', 'jakis rayon1', '23401'),
('3', 'Kyiv', 'jakas tam2', 'jakis rayon2', '03401'),
('4', 'Kyiv', 'jakas tam3', 'jakis rayon3', '03301'),
('5', 'Ternopil', 'jakas tam4', 'jakis rayon4', '43301');

insert into Services (ID,Name,deadline,Category,Price) values 
(1, 'Rejestracia ', 4, 'jakas Category', '23.45'),
(2, 'Rejestracia1 ',  6, 'jakas Category1', '20.45'),
(3, 'Rejestracia2 ', 2, 'jakas Category2', '2442'),
(4, 'Rejestracia3 ', 2, 'jakas Category3', '7452'),
(5, 'Rejestracia4 ', 3, 'jakas Category4', '1442');

insert into CNAP (ID,AddressID,Contact, Main_Affiliate) values
('1', 2, 'jakis Contact', '1'),
('2', 2, 'jakis Contact1', 0),
('3', 1, 'jakis Contact2', 0),
('4', 3, 'jakis Contact3', 1),
('5', 5, 'jakis Contact4', 1);

 insert into CNAP_Services (CNAPID, ServicesID) values
(1,3),
(2,1),
(3,3),
(4,2),
(5,5);

insert into Client (ID,Birthday,Surname,Name,Middle_name,PassportID,IdentificationID,Place_of_residence,Contact) values
('1', 'Birthday ', 'jakis Surname', 'jakIs Name', 'Jakis Middle_name', 'PassportID','IdentificationID','Place_of_residence','Contact'),
('2', 'Birthday1 ', 'jakis Surname1', 'jakIs Name1', 'Jjakis Middle_name1', 'PassportID1','IdentificationID1','Place_of_residence1','Contact1'),
('3', 'Birthday2 ', 'jakis Surname2', 'jakIs Name2', 'Jjakis Middle_name2', 'PassportID2','IdentificationID2','Place_of_residence2','Contact2'),
('4', 'Birthday3 ', 'jakis Surname3', 'jakIs Name3', 'Jjakis Middle_name3', 'PassportID3','IdentificationID3','Place_of_residence3','Contact3'),
('5', 'Birthday4 ', 'jakis Surname4', 'jakIs Name4', 'Jjakis Middle_name4', 'PassportID4','IdentificationID4','Place_of_residence4','Contact4');

 insert into Employee (ID,Surname,Name,Middle_name,Office,Department,Contact,CNAPID) values
 ('1', 'jakis Surname', 'jakIs Name', 'jakis Middle_name', 'Office','Department','Contact', 1 ),
('2', 'jakis Surname', 'jakIs Name', 'jakis Middle_name', 'Office','Department','Contact', 1 ),
('3', 'jakis Surname', 'jakIs Name', 'jakis Middle_name', 'Office','Department','Contact', 1 ),
('4', 'jakis Surname', 'jakIs Name', 'jakis Middle_name', 'Office','Department','Contact', 3 ),
('5', 'jakis Surname', 'jakIs Name', 'jakis Middle_name', 'Office','Department','Contact', 2 );

insert into TypeOfStatus (ID, Name) values
('1', 'Done'),
('2', 'Finishing'),
('3', 'Cancel'),
('4', 'Review');

 insert into Statement (ID, ClientID,ServicesID,EmployeeID,Date_of_application,TypeOfStatusID,CNAPID) values
(1, 1,1,1, 'Jakis Date_of_application', 1,1),
(2, 2,1,3, 'Jakis Date_of_applicatio1', 1,1),
(3, 1,3,3, 'Jakis Date_of_application2', 2,4),
(4, 4,2,3, 'Jakis Date_of_application3', 3,4),
(5, 5,2,5, 'Jakis Date_of_application5', 4,3);


insert into TypeOfDocument (ID,Name) values
('1','Jakis TypeOfDocument'),
('2','Jakis TypeOfDocument1'),
('3','Jakis TypeOfDocument1'),
('4','Jakis TypeOfDocument2'),
('5','Jakis TypeOfDocument3');


insert into Document (ID,StatementID,TypeOfDocumentID) values
(1,1,1),
(2,3,1),
(3,3,4),
(4,5,2),
(5,2,1);


SHOW COLUMNS FROM Address;
SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'CNAP' AND TABLE_NAME = 'Address';