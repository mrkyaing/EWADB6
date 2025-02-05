select * from aspnetUsers;
select * from AspNetRoles;
SELECT * FROM [AspNetUserClaims];
select * from aspnetuserroles;
insert into aspnetuserroles Values('64e8a026-de0e-4a3c-bebf-45b546fba58a',1);
insert into aspnetuserroles Values('9c0325b6-03f7-4897-9ff0-e270bb219742',2);

insert into AspNetRoles values('1',getdate(),'HR','this is HR role');
insert into AspNetRoles values('2',getdate(),'EMPLOYEE','this is EMPLOYEE role');
