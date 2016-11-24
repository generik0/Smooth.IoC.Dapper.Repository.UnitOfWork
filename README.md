![Project Icon](logo.jpg) Smooth.IoC.Dapper.Repository.UnitOfWork
===========================================

[![generik0 MyGet Build Status](https://www.myget.org/BuildSource/Badge/smooth-ioc-dapper-repository-unitofwork?identifier=55e88617-10c7-431e-ad25-9c1d4296ecbd)](https://www.myget.org/)
[![NuGet](https://img.shields.io/nuget/v/Smooth.IoC.Dapper.Repository.UnitOfWork.svg)](http://www.nuget.org/packages/Smooth.IoC.Dapper.Repository.UnitOfWork)

#Why

I made this project to fix the contradictory concepts behind the Repository and UnitOfWork patterns together with using inversition of control / dependancy injection. Also i wanted to make the resolving of sessions (IDbConnection) and UnitOFWork's (IDbTransaction) automatically connection / begin transaction on creation, and disconnect/commit on disposal.  
Also i wanted the usage of the session and uow to become nice and simple.  
This covers 97% of these needs. But i have also insured the the Session and UoW types extend the ADO base interfaces, so you can basically do anything you like with the session / uow, because they are IDbConnection and IDbTransaction. 

##The problem

All of the repository and UoW pattern examples i could find online did not include the usage of a factory for registration and injection. The session would typically be added to the constructor meaning when the session was disposed by one method, another method in the class could not use it any more. The examples with IoC used some very complex registration and multithreading code. But there really isn't a need for this!  
Basically something didn't seam to fix with the typical UoW and Repository patterns together with IoC.

I also found that injecting a simple factory that could create simple IDbConnections and IDbTransactions was not good enough. Because more intelegnce/help was needed.
Hence the IDbFactory, ISession, IUnitOfWork, IRepository interfaces and logic was born...

NB. I also feel it is important that it is possible to use one connection for production code and another for unit testing (e.g. MsSql for production and Sqlite for testing).
This design allows for this. As your custom session interface is used as the generic for the repository, not the session class allowing for different connection strings. 
You can even use the same database migrations if you have done code first. I have used [SimpleMigrations](https://github.com/canton7/Simple.Migrations) as it allows both console running for the production code / installer and inproc for unit testing.

You are welcome to look at the unit tests for examples or look below in this readme.

#What this the pachkage include and do?

So what i have done/created is this:

1. **IDbFactory** is a simple interface that you register with your IoC. It can create/spwan ISession's and IUntOfWork's. But primary used in code to spawn sessions.
2. **ISession<TDatabase>** extends IDbConnection. You use it to extend your Database connection / Session type. Yours session classes and interfaces require a connection string. So If you have multiple database connections, you need 1 ISession and Session extended Interface and class per database. When the session is created by the factory it connects to the database, when it disposes it discontects and disposes. For Castle Windsor it also untracks the object. You can use the session for any IDbConnection or dapper (or extension) framework you like, as ISession extends IDbConnection ;-). 
3. **IUnitOfWork** extends IDbTransaction. You dont need to extend anything with this. When you have created a session in you code, you can create a uow from the session. Then the session is created by the factory it begins a transaction (isolation i a parameter), when it disposes it commits (roleback on exception) and disposes. For Castle Windsor it also untracks the object. You can use the transaction for any IDbTransaction work you like as IUnitOfWork extends IDbConnection ;-).
4. **IRepository<TSession, TEntity, TPk>** is a default repository that you extend with your own repository for each of the entities you want a repository for. There as some built in methods for GetAll, Get, and SaveOrUpdate. You can add the methods you need for your entity using any IDbConnection framework. I have used [dapper-dot-net](https://github.com/StackExchange/dapper-dot-net) and [dapper.FastCRUD](https://github.com/MoonStorm/Dapper.FastCRUD) for the quering.

So far added examples om Castle.Windsor, StructureMap injection.

##Code examples

### Session and ISession

Below is an example of a session / dbconnection class.
Creating a your custom session and interface type, Extend the session base with your dbconnection type. Remmeber to use default convensions for your interface, you need to pass the connection string into the Session base class.

*You can inject a setting/config interface for this injected into your session class and then pass the connection setting (not shown here)*:
**Generic is a ADO DbConnection and supports with Dapper.FastCRUD MsSql, MySql, SQLite, PostgreSql.**

<pre><code>public class TestSession : Session<SQLiteConnection>, ITestSession {
    public TestSession(IDbFactory session)
        : base(session, "Data Source=:memory:;Version=3;New=True;")
    {
    }
}</code></pre>


### Repository and IRepository

Below is an example of a repository class that extends Repository. It uses your cusotm session(s) interface to understand the database to connect to. 
Creating a Repository interface, Add the IRepository to your Repository interface and give it the Entity and Pk generics.

**Rememeber when you unit test you can use your ISession inteface on another session class in your test project and that way use another database for testing than the production code** 

<pre><code>public class BraveRepository : Repository<ITestSession,Brave, int>, IBraveRepository
{
    public BraveRepository(IDbFactory factory) : base(factory)
    {
        ublic BraveRepository(IDbFactory factory) : base(factory)
        {
        }
    }
}</code></pre>

### Using Session and UnitOFWork in a class/method
Below is an examples of a the factory spawning a session and the session (using its injected factory) to spawn a UoW.  
Here we create a session to get data, and create a uow to save data.  
*The connection and begin transaction will happen on create and close and commit will happen @disposal*

<pre><code>public class MyClass : IMyClass
{
	private readonly IDbFactory _factory;
    private readonly IBraveRepository _repository;

    public MyClass(IDbFactory factory, IBraveRepository braveRepository)
    {
		_factory = factory;
        _repository = braveRepository;
    }

	publiv void DoSomething()
	{
		using (var session = _factory.CreateSession<ITestSession>())
        {
			var myItem = _repository.GetKey(1, session);
            using (var uow = session.UnitOfWork())
			{
				_repository.SaveOrUpdate(myItem, uow);
			}
        }
	}
}</code></pre>

Here is the simple version where we just want to get some data and the repository will created and close the connection it self.  
*The connection and commit will happen @disposal*

<pre><code>public class MyClass : IMyClass
{
	private readonly IDbFactory _factory;
    private readonly IBraveRepository _repository;

    public MyClass(IDbFactory factory, IBraveRepository braveRepository)
    {
		_factory = factory;
        _repository = braveRepository;
    }

	publiv void DoSomething()
	{
        var myItem = _repository.GetKey(1, session);
	}
}</code></pre>


#That simple. That smooth.#


#IoC registration

You need to register your own repository and session classes yourself. But using default convensions this should happen automatically in you bootsrapper, right?

## Castle Windsor Installer:

You need to register the factory and UnitofWork
<pre><code>public class SmoothIoCDapperRepositoryUnitOfWorkInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (container.Kernel.GetFacilities().ToList().FirstOrDefault(x => x.GetType() == typeof(TypedFactoryFacility)) == null)
            {
                container.Kernel.AddFacility<TypedFactoryFacility>();
            }
            container.Register(Component.For<IDbFactory>().AsFactory().IsFallback().LifestyleSingleton());
            container.Register(Component.For<IUnitOfWork>()
                .ImplementedBy<Data.UnitOfWork>().IsFallback().LifestyleTransient());
        }
    }
</code></pre>