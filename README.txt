This project is to easier use IoC with:

* Dapper
* Dapper.FastCRUD
* And IoC, so far supported:
** Castle Windsor
* A repository pattern (base classes)
* A UnitOfWork Pattern, NB
** The Unit of work will control the session and a session facotry
** Created using an UnitOfWork factory
** A Session base class,
** Created using an session factory

You are welcom to look at the test cases how to use.
But simply add the Repository abstract class to you repository classes
Add Your session / connection class inherited from Session. Adding the DbConnection tyhoe as a generic.
Use the IDbFactory injected into your classes
Create a session in your code using (var session = IdbFactory.Create<MySession>())
If you need a transaction then using(var uow = session.UnitOfWork)

Smooth.IoC.Dapper.Repository.UnitOfWork.Castle: Has example of Castle windsor registration and
Smooth.IoC.Dapper.Repository.UnitOfWork.Tests has example of usage. The test does not constructor inject, which you ofcause should...
