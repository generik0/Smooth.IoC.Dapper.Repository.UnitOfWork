![Project Icon](https://raw.githubusercontent.com/Generik0/Smooth.IoC.Dapper.Repository.UnitOfWork/master/logo.jpg) Smooth.IoC.Dapper.Repository.UnitOfWork
===========================================

[![generik0 MyGet Build Status](https://www.myget.org/BuildSource/Badge/smooth-ioc-dapper-repository-unitofwork?identifier=55e88617-10c7-431e-ad25-9c1d4296ecbd)](https://www.myget.org/)
[![NuGet](https://img.shields.io/nuget/v/Smooth.IoC.Dapper.Repository.UnitOfWork.svg)](http://www.nuget.org/packages/Smooth.IoC.Dapper.Repository.UnitOfWork)

# Why
 
I made this project to fix the contradictory concepts behind the Repository and UnitOfWork patterns together with using inversition of control / dependancy injection. Also i wanted to make the resolving of sessions (IDbConnection) and UnitOFWork's (IDbTransaction) automatically connection / begin transaction on creation, and disconnect/commit on disposal.  
Also i wanted the usage of the session and uow to become nice and simple.  
This should cover 97% of your needs. But i have also insured the the Session and UoW types extend the ADO base interfaces, so you can basically do anything you like with the session / uow, because they are IDbConnection and IDbTransaction. 

## The problem

All of the repository and UoW pattern examples i could find online did not include the usage of a factory for registration and injection. The session would typically be added to the constructor meaning when the session was disposed by one method, another method in the class could not use it any more. The examples with IoC used some very complex registration and multithreading code. But there really isn't a need for this!  
Basically something didn't seam to fix with the typical UoW and Repository patterns together with IoC.  
I also found that injecting a simple factory that could create simple IDbConnections and IDbTransactions was not good enough. Because more intelegnce/help was needed.
Hence the IDbFactory, ISession, IUnitOfWork, IRepository interfaces and logic was born...  

NB. I also feel it is important that it is possible to use one connection for production code and another for unit testing (e.g. MsSql for production and Sqlite for testing).
This design allows for this. As your custom session interface is used as the generic for the repository, not the session class allowing for different connection strings. 
You can even use the same database migrations if you have done code first. I have used [SimpleMigrations](https://github.com/canton7/Simple.Migrations) as it allows both console running for the production code / installer and inproc for unit testing.

You are welcome to look at the unit tests for examples or look below in this readme.

# What this the pachage include and do?

So what i have done/created is this:

1. **IDbFactory** is a simple interface that you register with your IoC. It can create/spwan ISession's and IUntOfWork's. But primary used in code to spawn sessions.
2. **ISession&lt;TDatabase&gt;** (and Session&lt;TDatabase&gt; abstraction):	Extends IDbConnection. You use it to extend your Database connection / Session type. Yours session classes and interfaces require a connection string. So If you have multiple database connections, you need 1 ISession and Session extended Interface and class per database. When the session is created by the factory it connects to the database, when it disposes it discontects and disposes. For Castle Windsor it also untracks the object. You can use the session for any IDbConnection or dapper (or extension) framework you like, as ISession extends IDbConnection ;-). 
3. **IUnitOfWork** (and UnitOfWork): Extends IDbTransaction. You don't need to extend anything with this. When you have created a session in you code, you can create a uow from the session. Then the session is created by the factory it begins a transaction (isolation i a parameter), when it disposes it commits (roleback on exception) and disposes. For Castle Windsor it also untracks the object. You can use the transaction for any IDbTransaction work you like as IUnitOfWork extends IDbConnection ;-).
4. **IRepository&lt;TSession, TEntity, TPk&gt;** (Repository&lt;TSession, TEntity, TPk&gt; abstraction):	Is a default repository that you extend with your own repository for each of the entities you want a repository for. There as some built in methods for GetAll, Get, and SaveOrUpdate. You can add the methods you need for your entity using any IDbConnection framework. I have used [dapper-dot-net](https://github.com/StackExchange/dapper-dot-net) and [dapper.FastCRUD](https://github.com/MoonStorm/Dapper.FastCRUD) for the quering.
5. **IEntity&lt;TPk&gt;**: An interface for your Entities so FastCRUD GetKey works in Repositories. To use this your table / entity should always have and Id column as Pk in what every type you like (its a generic :-).
6. **IRepositoryBase** (and RepositoryBase&lt;TEntity&gt; abstraction):	This is a vanilla base repository, you can use it if you do not want to use Dapper or Dapper.FastCRUD and/or IEntity interface. It includes an protected method to set the dialect which you will need to do, if not using FastCRUD without the IEntity interface.

So far added examples om Castle.Windsor, StructureMap, Ninjet injection, Unity and Autofac.

## Code examples

### Dapper and Dapper.FastCRUD
I use Dapper and Dapper.FastCRUD for my sql work.  
Dapper is a micro ORM data does only what you ask of it through queries. [Dapper](https://github.com/StackExchange/dapper-dot-net)  
There is an extension to Dapper called Dapper.FastCRUD. This adds ORM and fluent sql to dapper. [Dapper.FastCRUD](https://github.com/MoonStorm/Dapper.FastCRUD).  
The drawback with Dapper.FastCRUD is it may fail if you don't give it the SqlDialect.

So i have extended FastDappers IDbConnection extensions. I.e. ISession and IUnitOfWork are extended, so if the session or uow run a dapper query they set the dialogue if needed.
This means that your Entity can only be used for one database type. So if you want your entity to spand across more than one database, you need to make a entity abstraction class 
and create children of the extension. Also use the RepositoryBase to extend from bypassing the Repository abstraction.


### Session and ISession

Below is an example of a session / dbconnection class.
Creating a your custom session and interface type, Extend the session base with your dbconnection type. Remmeber to use default convensions for your interface, you need to pass the connection string into the Session base class.

*You can inject a setting/config interface for this injected into your session class and then pass the connection setting (not shown here)*:
**Generic is a ADO DbConnection and supports with Dapper.FastCRUD MsSql, MySql, SQLite, PostgreSql.**

<pre><code>public class TestSession : Session&lt;SQLiteConnection&gt;, ITestSession {
    public TestSession(IDbFactory session)
        : base(session, "Data Source=:memory:;Version=3;New=True;")
    {
    }
}</code></pre>


### Repository and IRepository

Below is an example of a repository class that extends Repository. It uses your cusotm session(s) interface to understand the database to connect to. 
Creating a Repository interface, Add the IRepository to your Repository interface and give it the Entity and Pk generics.

**Rememeber when you unit test you can use your ISession inteface on another session class in your test project and that way use another database for testing than the production code** 

<pre><code>public interface IBraveRepository : IRepository&lt;Brave, int&gt;
{
}

public class BraveRepository : Repository&lt;ITestSession,Brave, int&gt;, IBraveRepository
{
    public BraveRepository(IDbFactory factory) : base(factory)
    {
        public BraveRepository(IDbFactory factory) : base(factory)
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
		using (var session = _factory.Create&lt;ITestSession&gt;())
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


# That simple. That smooth.#


#IoC registration

You need to register your own repository and session classes yourself. But using default convensions this should happen automatically in you bootsrapper, right?

## Autofac registration

Autofac does have a factory using delegates but this does not fit the same pattern as all the other IoC. 
So one has to wrap the factory in a concrete implementation. Luckely the concrete implementation can be internal (or even private if you like).
Registration examples:	

<pre><code>public void Register(IUnityContainer container)
{
    container.RegisterType<IDbFactory, UnityDbFactory>(new ContainerControlledLifetimeManager(), new InjectionConstructor(container));
    container.RegisterType<IUnitOfWork, Dapper.Repository.UnitOfWork.Data.UnitOfWork>();
}

class UnityDbFactory : IDbFactory
{
    private readonly IUnityContainer _container;
	
	public UnityDbFactory(IUnityContainer container)
    {
        _container = container;
    }
	public T Create<T>() where T : ISession
    {
        return _container.Resolve<T>();
    }
	public T Create<T>() where T : ISession
    {
        return _container.Resolve<T>();
    }
	public T Create<T>(IDbFactory factory, ISession session) where T : IUnitOfWork
    {
        return _container.Resolve<T>(new ParameterOverride("factory", factory), 
            new ParameterOverride("session", session), new ParameterOverride("isolationLevel", IsolationLevel.Serializable));
    }
	public T Create<T>(IDbFactory factory, ISession session, IsolationLevel isolationLevel) where T : IUnitOfWork
    {
        return (T)Activator.CreateInstance(typeof(T), factory, session, isolationLevel);
    }
	public void Release(IDisposable instance)
    {
        _container.Teardown(instance);
    }
}</code></pre>

## Castle Windsor Installer

You need to register the factory and UnitofWork
<pre><code>public class SmoothIoCDapperRepositoryUnitOfWorkInstaller : IWindsorInstaller
{
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
        if (container.Kernel.GetFacilities().ToList().FirstOrDefault(x =&gt; x.GetType() == typeof(TypedFactoryFacility)) == null)
        {
            container.Kernel.AddFacility&lt;TypedFactoryFacility&gt;();
        }
        container.Register(Component.For&lt;IDbFactory&gt;().AsFactory().IsFallback().LifestyleSingleton());
        container.Register(Component.For&lt;IUnitOfWork&gt;()
            .ImplementedBy&lt;Data.UnitOfWork&gt;().IsFallback().LifestyleTransient());
    }
}
</code></pre>

## Ninject registration

Ninject like Castle, and unlike Structure Map has a good factory. Unfortunately the factory does not have a Release. 
So i decided to in the example to combine the use of the ninject's factory and a concrete factory.
*You need to install-package Ninject.Extensions.Factory for this to work*

When you register the binding, for this example to work, the Bind must be after your other registrations so that "Rebind" on the DbFactory works.
It is only so the DbFactory is singleton. It doesn't need to be, but i am hoping it is faster for the IoC to resolve.

<pre><code>public class NinjectBinder
{
    public void Bind(IKernel kernel)
    {
        kernel.Bind&lt;INinjectDbFactory&gt;().ToFactory(() =&gt; new TypeMatchingArgumentInheritanceInstanceProvider());
        kernel.Rebind&lt;IDbFactory&gt;().To&lt;DbFactory&gt;().InSingletonScope();
        kernel.Bind&lt;IUnitOfWork&gt;().To&lt;Dapper.Repository.UnitOfWork.Data.UnitOfWork&gt;()
            .WithConstructorArgument(typeof(IDbFactory))
            .WithConstructorArgument(typeof(ISession))
            .WithConstructorArgument(typeof(IsolationLevel));
    }
}
class DbFactory : IDbFactory
{
    private readonly IResolutionRoot _resolutionRoot;
    private INinjectDbFactory _factory;

    public DbFactory(IResolutionRoot resolutionRoot)
    {
        _resolutionRoot = resolutionRoot;
        _factory= resolutionRoot.Get&lt;INinjectDbFactory&gt;();
    }
    public T Create&lt;T&gt;() where T : ISession
    {
        return _factory.Create&lt;T&gt;();
    }
    public T Create&lt;T&gt;(IDbFactory factory, ISession connection) where T : IUnitOfWork
    {
        return _factory.Create&lt;T&gt;(factory, connection);
    }
    public T Create&lt;T&gt;(IDbFactory factory, ISession connection, IsolationLevel isolationLevel) where T : IUnitOfWork
    {
        return _factory.Create&lt;T&gt;(factory, connection);
    }
    public void Release(IDisposable instance)
    {
        _resolutionRoot.Release(instance);
    }
}</code></pre>

## Structure Map registration

You need to create a concrete factory and register it, passing the containter as an argurment to the factory
<pre><code>public class StructureMapRegistration
{
    public void Register(IContainer container)
    {
        container.Configure(c=&gt;c.For&lt;IDbFactory&gt;()
        .UseIfNone&lt;StructureMapDbFactory&gt;().Ctor&lt;IContainer&gt;()
        .Is(container).Singleton());
    }
}</code></pre>

The Concrete StructureMapDbFactory looks like this:
<pre><code>public class StructureMapDbFactory : IDbFactory
{
    private IContainer _container;

    public StructureMapDbFactory(IContainer container)
    {
        _container = container;
    }
	public T Create&lt;T&gt;() where T : ISession
    {
        return _container.GetInstance&lt;T&gt;();
    }
    public T Create&lt;T&gt;(IDbFactory factory, ISession connection) where T : IUnitOfWork
    {
        return  _container.With(factory).With(connection).GetInstance&lt;T&gt;();
    }
    public T Create&lt;T&gt;(IDbFactory factory, ISession connection, IsolationLevel isolationLevel) where T : IUnitOfWork
    {
        return  _container.With(factory).With(connection).With(isolationLevel).GetInstance&lt;T&gt;();
    }
    public void Release(IDisposable instance)
    {
        _container.Release(instance);
    }
}</code></pre>


## Unity registration

Unity does not appear to have a very good factory. So one has to wrap the factory in a concrete implementation. Luckely the concrete 
implementation can be internal (or even private if you like).
Ünfortuantely Unity could not figure out when i tried to override only 2 paramateres, that it should use a diffent constructor. So the UnitOfWork 
Constructor with 3 parameters is always called.

<pre><code>public void Register(IUnityContainer container)
{
    container.RegisterType<IDbFactory, UnityDbFactory>(new ContainerControlledLifetimeManager(), new InjectionConstructor(container));
    container.RegisterType<IUnitOfWork, Dapper.Repository.UnitOfWork.Data.UnitOfWork>();
}

class UnityDbFactory : IDbFactory
{
    private readonly IUnityContainer _container;

    public UnityDbFactory(IUnityContainer container)
    {
        _container = container;
    }
    public T Create<T>() where T : ISession
    {
        return _container.Resolve<T>();
    }
    public T Create<T>() where T : ISession
    {
        return _container.Resolve<T>();
    }
    public T Create<T>(IDbFactory factory, ISession session) where T : IUnitOfWork
    {
        return _container.Resolve<T>(new ParameterOverride("factory", factory), 
            new ParameterOverride("session", session), new ParameterOverride("isolationLevel", IsolationLevel.Serializable));
    }
    public T Create<T>(IDbFactory factory, ISession session, IsolationLevel isolationLevel) where T : IUnitOfWork
    {
        return (T)Activator.CreateInstance(typeof(T), factory, session, isolationLevel);
    }
    public void Release(IDisposable instance)
    {
        _container.Teardown(instance);
    }
}</code></pre>