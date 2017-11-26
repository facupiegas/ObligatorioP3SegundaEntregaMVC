insert into Usuarios values('admin','q29hHeIOfhphUo7XE+okCwd6baA2vhPaRTuMVrekHRM=',0,'tyWfxHTW')
insert into Usuarios values('administrador','q29hHeIOfhphUo7XE+okCwd6baA2vhPaRTuMVrekHRM=',0,'tyWfxHTW')
insert into Usuarios values('car@litos.com','q29hHeIOfhphUo7XE+okCwd6baA2vhPaRTuMVrekHRM=',2,'tyWfxHTW')
insert into Usuarios values('pe@drito.com','q29hHeIOfhphUo7XE+okCwd6baA2vhPaRTuMVrekHRM=',2,'tyWfxHTW')

insert into Organizadores values('carlitos','car@litos.com','099','car@litos.com')
insert into Organizadores values('pedrito','pe@drito.com','098','pe@drito.com')

--Eventos de carlitos
insert into Eventos values('Asado Dominguero','12/12/2015','18 de Julio 1818',1,'carlitos','Asado')
insert into Eventos values('Casorio','06/10/2017','Colonia 1943',1,'carlitos','Casamiento')
insert into Eventos values('Cumpleños de Pepe','03/07/2018','Mercedes 1621',0,'carlitos','Cumpleaños Infantil')

--Eventos de pedrito
insert into Eventos values('Bautismo De Juan','11/12/2013','Colonia 1223',1,'pedrito','Bautismo')
insert into Eventos values('Los Quince De Anita','09/09/2017','Paysandu 1999',1,'pedrito','Fiesta de quince')
insert into Eventos values('Casamiento Laura y Marcelo','05/02/2016','Artigas 2216',1,'pedrito','Casamiento')

--Servicios Contratados para Eventos de carlitos
insert into ServicioContratados values('12/12/2015','210000123400','Asado del Pepe','Asado Dominguero',1,'Asado Dominguero')

insert into ServicioContratados values('06/10/2017','210000123402','Fotasas','Casorio',0,'Casorio')
insert into ServicioContratados values('06/10/2017','210000123401','Decora2','Casorio',0,'Casorio')
insert into ServicioContratados values('06/10/2017','210000123477','El Gloton','Casorio',0,'Casorio')
insert into ServicioContratados values('06/10/2017','210000123403','Que burguer','Casorio',0,'Casorio')

insert into ServicioContratados values('03/07/2018','210000123403','Que burguer','Cumpleños de Pepe',0,'Cumpleños de Pepe')
insert into ServicioContratados values('03/07/2018','210000123401','Decora2','Cumpleños de Pepe',0,'Cumpleños de Pepe')
insert into ServicioContratados values('03/07/2018','210000123402','Reboton','Cumpleños de Pepe',0,'Cumpleños de Pepe')
insert into ServicioContratados values('03/07/2018','210000123402','Payasada','Cumpleños de Pepe',0,'Cumpleños de Pepe')


--Servicios Contratados para Eventos de pedrito
insert into ServicioContratados values('11/12/2013','210000123401','Decora2','Bautismo De Juan',1,'Bautismo De Juan')
insert into ServicioContratados values('11/12/2013','210000123402','Fotasas','Bautismo De Juan',1,'Bautismo De Juan')
insert into ServicioContratados values('11/12/2013','210000123402','Payasada','Bautismo De Juan',1,'Bautismo De Juan')

insert into ServicioContratados values('09/09/2017','210000123401','Decora2','Los Quince De Anita',0,'Los Quince De Anita')
insert into ServicioContratados values('09/09/2017','210000123402','Fotasas','Los Quince De Anita',0,'Los Quince De Anita')
insert into ServicioContratados values('09/09/2017','210000123403','Que burguer','Los Quince De Anita',0,'Los Quince De Anita')
insert into ServicioContratados values('09/09/2017','210000123403','Pizzas XXL','Los Quince De Anita',0,'Los Quince De Anita')
insert into ServicioContratados values('09/09/2017','210000123477','El Gloton','Los Quince De Anita',0,'Los Quince De Anita')
insert into ServicioContratados values('09/09/2017','210000123477','EL Lunch','Los Quince De Anita',0,'Los Quince De Anita')

insert into ServicioContratados values('05/02/2016','210000123477','EL Lunch','Casamiento Laura y Marcelo',1,'Casamiento Laura y Marcelo')
insert into ServicioContratados values('05/02/2016','210000123402','Fotasas','Casamiento Laura y Marcelo',1,'Casamiento Laura y Marcelo')
insert into ServicioContratados values('05/02/2016','210000123403','Pizzas XXL','Casamiento Laura y Marcelo',1,'Casamiento Laura y Marcelo')



--Comentarios ingresados para Eventos de carlitos
insert into CalificacionProveedores values('210000123400','Asado Dominguero',4,'Muy bueno!')


--Comentarios ingresados para Eventos de pedrito
insert into CalificacionProveedores values('210000123402','Bautismo De Juan',3,'Poco divertido.')
insert into CalificacionProveedores values('210000123401','Bautismo De Juan',5,'Excelente!')

insert into CalificacionProveedores values('210000123477','Casamiento Laura y Marcelo',5,'Maravilloso!')
insert into CalificacionProveedores values('210000123402','Casamiento Laura y Marcelo',2,'Horrible.')
insert into CalificacionProveedores values('210000123403','Casamiento Laura y Marcelo',3,'Mucho para mejorar')

update Proveedores
set CantVecesElegido = 2
where Rut = '210000123402'

update Proveedores
set CantVecesElegido = 1
where Rut in('210000123400','210000123401','210000123403','210000123477')

update Proveedores
set CalificacionGeneral = 5
where Rut in ('210000123401','210000123477')


update Proveedores
set CalificacionGeneral = 4
where Rut = '210000123400'

update Proveedores
set CalificacionGeneral = 2
where Rut = '210000123402'

update Proveedores
set CalificacionGeneral = 3
where Rut = '210000123403'