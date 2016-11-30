![Project Icon](https://raw.githubusercontent.com/Generik0/Smooth.IoC.Dapper.Repository.UnitOfWork/master/logo.jpg) Smooth.IoC.Dapper.Repository.UnitOfWork
===========================================

[![generik0 MyGet Build Status](https://www.myget.org/BuildSource/Badge/smooth-ioc-dapper-repository-unitofwork?identifier=55e88617-10c7-431e-ad25-9c1d4296ecbd)](https://www.myget.org/)
[![NuGet](https://img.shields.io/nuget/v/Smooth.IoC.Dapper.Repository.UnitOfWork.svg)](http://www.nuget.org/packages/Smooth.IoC.Dapper.Repository.UnitOfWork)

**Table of Contents**  *generated with [DocToc](http://doctoc.herokuapp.com/)*
- [ Smooth.IoC.Dapper.Repository.UnitOfWork](#)
- [Why](#)  
	- [What are the features of the library?](#)  
	- [The problem, why use this library?](#)  
	- [What this the package include?](#)  
- [About Dapper and Dapper.FastCRUD](#)
- [Code Examples: Sessions, Repositories and UnitOfWork](#)  
    - [Session and ISession](#)  
    - [Repository and IRepository](#)  
    - [Using Session and UnitOFWork in a class/method](#)  
- [That simple. That smooth.](#)
- [Code Examples: IoC registration](#)
	- [Autofac registration](#)  
	- [Castle Windsor Installer](#)  
	- [Ninject registration](#)  
	- [Autofac registration](#)  
	- [Structure Map registration](#)  
	- [Unity registration](#)  
	- [Version History](#)

# Why
I made this project to fix the contradictory concepts behind the Repository and UnitOfWork patterns together with using inversition of control
 / dependancy injection. Also i wanted to make the creation of sessions (IDbConnection) and UnitOFWork's (IDbTransaction) by the injected factory automatically 
 connection / begin transaction on creation, and disconnect/commit on disposal.  
Also i wanted the usage of the session and uow to become nice and smooth like....  

*So far there are examples of Autofact, Castle.Windsor, Ninject, Simpleinjector, StructureMap, and Unity.*

I also want to be able to do unit testing with e.g. a Sqlite and production with e.g. a MsSQl. This is possible now...

This should cover 97% of your needs. But i have also insured that the Session and UoW types extend the ADO base interfaces, so you can basically
 do anything you like with the session / uow, because they are IDbConnection and IDbTransaction. 

I have tried to insure it is as bare bones as possible. Only adding the system libraries needed and Dapper + Dapper.FastCRUD.
The IoC of your choice is not included in the package,  but i have made example registrations for the "majors" look at: [Code Examples: IoC registration](#Code-Examples:-IoC-registration).

## What are the features of the library?
The library gives you the building blocks to:
* Create Sessions from the IDbFactory in your methods, IDbFactory should be injected into your class's. As Session extends IDbConnection and will Open on the factory spawning the session and dispose the connection on disposal of the connection.
* Your Sessions can create UnitOfWork's. As Session extends IDbConnection and will Open on the factory spawning the session and dispose the connection on disposal of the connection.
* The IRepository and abstract concrete class should be used on your individual repository classes to provide simple and basic calls. But ofcasue you can add all the queries you want into your 
Repositories and use the dapper and dapper.FastCRUD (or any other extensions) functionality provided to you.
* This library does not lock you to using dapper and FastCRUD, you can use any library you like that extends IDbConnection and IDbTransation, and still use the IDbFactory, ISession and IUnitOrWork.  
* Implemented for .net 4.5.2, .net 4.6.1+, .net 1.6 standard+.

**You will have to register the IDbFactory and IUnitOfWork manually, along with your other registrations. But below in this readme and in the test examples i have added examples for: Autofac, Castle.Windsor, StructureMap, 
Ninject, Unity.  I did not want to add al sorts of IoC containers to the package, so please look at the examples. And If you have better knowledge of an IoC framework, please let me know if it 
can be done better, and smoother...**

## The problem, why use this library?
All of the repository and UoW pattern examples i could find online did not include the usage of a factory for registration and injection. The session would typically be added to the constructor meaning when the session was disposed by one method, another method in the class could not use it any more. The examples with IoC used some very complex registration and multithreading code. But there really isn't a need for this!  
Basically something didn't seam to fix with the typical UoW and Repository patterns together with IoC.  
I also found that injecting a simple factory that could create simple IDbConnections and IDbTransactions was not good enough. Because more intelegence/help was needed.
Hence the IDbFactory, ISession, IUnitOfWork, IRepository interfaces and logic was born...  

NB. I also feel it is important that it is possible to use one connection for production code and another for unit testing (e.g. MsSql for production and Sqlite for testing).
This design allows for this. As your custom session interface is used as the generic for the repository, not the session class allowing for different connection strings. 
You can even use the same database migrations if you have done code first. I have used [SimpleMigrations](https://github.com/canton7/Simple.Migrations) as it allows both console running for the production code / installer and inproc for unit testing.

You are welcome to look at the unit tests for examples or look below in this readme.

## What this the package include?
So what i have done/created is this:

1. **IDbFactory** is a simple interface that you register with your IoC. It can create/spwan ISession's and IUntOfWork's. But primary used in code to spawn sessions.
2. **ISession&lt;TDatabase&gt;** (and Session&lt;TDatabase&gt; abstraction):	Extends IDbConnection. You use it to extend your Database connection / Session type. Yours session classes 
and interfaces require a connection string. So If you have multiple database connections, you need 1 ISession and Session extended Interface and class per database. When the session is 
created by the factory it connects to the database, when it disposes it discontects and disposes. For Castle Windsor it also untracks the object. You can use the session for any IDbConnection or dapper (or extension) framework you like, as ISession extends IDbConnection ;-). 
3. **IUnitOfWork** (and UnitOfWork): Extends IDbTransaction. You don't need to extend anything with this. When you have created a session in you code, you can create a uow from the session. Then the session is created by the factory it begins a transaction (isolation i a parameter), when it disposes it commits (roleback on exception) and disposes. For Castle Windsor it also untracks the object. You can use the transaction for any IDbTransaction work you like as 
IUnitOfWork extends IDbTransaction ;-).
4. **IRepository&lt;TSession, TEntity, TPk&gt;** (Repository&lt;TSession, TEntity, TPk&gt; abstraction):	Is a default repository that you extend with your own repository for each of 
the entities you want a repository for. There as some built in methods for GetAll, Get, and SaveOrUpdate. You can add the methods you need for your entity using any IDbConnection framework. 
 have used [dapper-dot-net](https://github.com/StackExchange/dapper-dot-net) and [dapper.FastCRUD](https://github.com/MoonStorm/Dapper.FastCRUD) for the quering.
5. **IEntity&lt;TPk&gt;**: An interface for your Entities so FastCRUD GetKey works in Repositories. To use this your table / entity should always have and Id column as Pk in 
what every type you like (its a generic :-).
6. **IRepositoryBase** (and RepositoryBase&lt;TEntity&gt; abstraction):	This is a vanilla base repository, you can use it if you do not want to use Dapper or Dapper.FastCRUD 
and/or IEntity interface. It includes an protected method to set the dialect which you will need to do, if you want to use FastCRUD  but without the IEntity interface.

# About Dapper and Dapper.FastCRUD
I use Dapper and Dapper.FastCRUD for my sql work.  
Dapper is a micro ORM data does only what you ask of it through queries. [Dapper](https://github.com/StackExchange/dapper-dot-net)  
There is an extension to Dapper called Dapper.FastCRUD. This adds fluentness to dapper. [Dapper.FastCRUD](https://github.com/MoonStorm/Dapper.FastCRUD).  

The drawback with Dapper.FastCRUD is it may fail if you don't give it the wrong SqlDialect.
So i have extended FastDappers IDbConnection extensions so the projects own ISession and IUnitOfWork are extended. Then the FastCRUD IDbConnection extension method extended by ISession or IUnitOfWork extensions in
the package. This insures that the dialogue is set correct, if needed. This means that your Entity can only be used for one database type per ioc container / executing assembily. So you can also use a different databae for your tests than your production code.
* This will only effect FastCRUD calls using ISession or IUnitOrWork instances. Not IDbConnection instances.
* If you want your entity to span across more than one database, you can use the RepositoryBase to extend from bypassing the Repository abstraction.

You can do a lot fluently with FastCRUD. Check out there wiki:
- [Home](https://github.com/MoonStorm/Dapper.FastCRUD/wiki)
- [Entity registration](https://github.com/MoonStorm/Dapper.FastCRUD/wiki/Entity-registration)
- [Default library conventions](https://github.com/MoonStorm/Dapper.FastCRUD/wiki/Default-library-conventions)
- [JOINS](https://github.com/MoonStorm/Dapper.FastCRUD/wiki/JOINs)
- [SQL statements and clauses](https://github.com/MoonStorm/Dapper.FastCRUD/wiki/SQL-statements-and-clauses)

Or as i have already menioned use dapper or any other extension utilizing IDbConnection And IDbTransaction...


# Code Examples: Sessions, Repositories and UnitOfWork
Below is examples of using the package with Sessions, UnitOfWork and repositories.

### Session and ISession
Below is an example of a session / dbconnection class.
Creating a your custom session and interface type, Extend the session base with your DbConnection type. Remember to use default convensions for your interface, you need to pass the connection string into the Session base class.
**NB the generic is a ADO IDbConnection, For Dapper.FastCRUD is supporst MsSql, MySql, SQLite, PostgreSql with the SqlDialect**

*You can inject a setting/config interface for this injected into your session class and then pass the connection setting*:
<pre><code>public class TestSession : Session&lt;SQLiteConnection&gt;, ITestSession {
    public TestSession(IDbFactory session, IMyDatabaseSettings settings)
            : base(session, settings.ConnectionString)
    {
    }
}</code></pre>

*You can manually add your connection string to the base class, but i don't recommend this*:
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

# Code Examples: IoC registration
You need to register your own repository and session classes yourself. But using default convensions this should happen automatically in you bootsrapper, right?

## Autofac registration
Autofac does have a factory using delegates but this does not fit the same pattern as all the other IoC. 
So one has to wrap the factory in a concrete implementation. Luckely the concrete implementation can be internal (or even private if you like).
Registration examples:	

<pre><code>public class AutofacRegistrar
{
public void Register(ContainerBuilder builder)
{
    builder.Register(c=&gt; new AutofacDbFactory(c)).As&lt;IDbFactory&gt;().SingleInstance();
    builder.RegisterType&lt;Dapper.Repository.UnitOfWork.Data.UnitOfWork&gt;().As&lt;IUnitOfWork&gt;();

}
internal class AutofacDbFactory : IDbFactory
{
    private readonly IComponentContext _container;

    public AutofacDbFactory(IComponentContext container)
    {
        _container = container;
    }

    public T Create&lt;T&gt;() where T : class, ISession
    {
        return _container.Resolve&lt;T&gt;();
    }

    public T CreateSession&lt;T&gt;() where T : class, ISession
    {
        return _container.Resolve&lt;T&gt;();
    }

    public T Create&lt;T&gt;(IDbFactory factory, ISession session) where T : IUnitOfWork
    {
        return _container.Resolve&lt;T&gt;(new NamedParameter("factory", factory),
            new NamedParameter("session", session));
    }

    public T Create&lt;T&gt;(IDbFactory factory, ISession session, IsolationLevel isolationLevel) where T : IUnitOfWork
    {
        return _container.Resolve&lt;T&gt;(new NamedParameter("factory", factory),
            new NamedParameter("session", session), new NamedParameter("isolationLevel", isolationLevel));
    }

    public void Release(IDisposable instance)
    {
        ;//do nothing
    }
}</code></pre>

## Castle Windsor Installer
You need to register the factory and UnitofWork for castle to work. Castle has its own factory implemenation that creates and releases the instances for you. It's great!

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
Ninject like Castle has a good factory. Unfortunately the factory does not have a Release. 
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
	internal sealed class DbFactory : IDbFactory
    {
        private readonly IResolutionRoot _resolutionRoot;
        private readonly INinjectDbFactory _factory;

        public DbFactory(IResolutionRoot resolutionRoot)
        {
            _resolutionRoot = resolutionRoot;
            _factory = resolutionRoot.Get&lt;INinjectDbFactory&gt;();
        }

        public T Create&lt;T&gt;() where T : class, ISession
        {
            return _factory.Create&lt;T&gt;();
        }

        public T CreateSession&lt;T&gt;() where T : class, ISession
        {
            return _factory.Create&lt;T&gt;();
        }

        public T Create&lt;T&gt;(IDbFactory factory, ISession session) where T : class, IUnitOfWork
        {
            return _factory.CreateUnitOwWork&lt;T&gt;(factory, session);
        }

        public T Create&lt;T&gt;(IDbFactory factory, ISession session, IsolationLevel isolationLevel)
            where T : class, IUnitOfWork
        {
            return _factory.CreateUnitOwWork&lt;T&gt;(factory, session);
        }

        public void Release(IDisposable instance)
        {
            _resolutionRoot.Release(instance);
        }
    }
}</code></pre>

## Simple Injector registration
I am not very happy about the Simpleinjector example. But it works. However the concrete factory implemenation include the word "new". Simple Injector 
does not like passing runtime arguements for the constructor and as the UnitOrWork requier the session instance it is a problem. Otherwise we get a new instance.
I decided to make a concrete factory, and for the UoW it "new's" the UnitOfWork. I looked at the delegate factory pattern in simple injector, this may be a better solution for the future
But the online help on the subject was incorrect and i could not replicate.

<pre><code>public class SimpleInjectorRegistrar
{
    public void Register(Container container)
    {
        container.RegisterSingleton&lt;IDbFactory&gt;(new SimpleInjectorDbFactory&lt;ISession&gt;(container));
    }

    public static void RegisterDisposableTransient(Container container , Type service, Type implementation )
    {
        var reg = Lifestyle.Transient.CreateRegistration(implementation, container);
        reg.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "suppressed.");
        container.AddRegistration(service, reg);
    }

    [NoIoCFluentRegistration]
    internal sealed class SimpleInjectorDbFactory&lt;TSession&gt; : IDbFactory where TSession : class, ISession
    {
        private readonly Container _container;
        private readonly Func&lt;TSession&gt; _sessionFactory;
        public SimpleInjectorDbFactory(Container container)
        {
            _container = container;
        }

        public T Create&lt;T&gt;() where T : class, ISession
        {
            return _container.GetInstance&lt;T&gt;();
        }

        public T CreateSession&lt;T&gt;() where T : class, ISession
        {
            return _container.GetInstance&lt;T&gt;();
        }

        public T Create&lt;T&gt;(IDbFactory factory, ISession session) where T : class, IUnitOfWork
        {

            return new Dapper.Repository.UnitOfWork.Data.UnitOfWork(factory, session) as T;
        }

        public T Create&lt;T&gt;(IDbFactory factory, ISession session, IsolationLevel isolationLevel) where T : class, IUnitOfWork
        {
            return new Dapper.Repository.UnitOfWork.Data.UnitOfWork(factory, session, isolationLevel) as T;
        }

        public void Release(IDisposable instance)
        {
            instance?.Dispose();
        }
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

    internal class StructureMapDbFactory : IDbFactory
    {
        private IContainer _container;

        public StructureMapDbFactory(IContainer container)
        {
            _container = container;
        }

        public T Create&lt;T&gt;() where T : class, ISession
        {
            return _container.GetInstance&lt;T&gt;();
        }

        public T CreateSession&lt;T&gt;() where T : class, ISession
        {
            return _container.GetInstance&lt;T&gt;();
        }

        public T Create&lt;T&gt;(IDbFactory factory, ISession session) where T : class, IUnitOfWork
        {
            return _container.With(factory).With(session).GetInstance&lt;T&gt;();
        }

        public T Create&lt;T&gt;(IDbFactory factory, ISession session, IsolationLevel isolationLevel) where T : class, IUnitOfWork
        {
            return _container.With(factory).With(session).With(isolationLevel).GetInstance&lt;T&gt;();
        }

        public void Release(IDisposable instance)
        {
            _container.Release(instance);
        }
    }
}</code></pre>

 
## Unity registration
Unity does not appear to have a very good factory. So one has to wrap the factory in a concrete implementation. Luckely the concrete 
implementation can be internal (or even private if you like).
Unfortunately Unity could not figure out when i tried to override only 2 paramateres, that it should use a diffent constructor. So the UnitOfWork 
Constructor with 3 parameters is always called.

<pre><code>public void Register(IUnityContainer container)
{
    container.RegisterType&lt;IDbFactory, UnityDbFactory&gt;(new ContainerControlledLifetimeManager(), new InjectionConstructor(container));
    container.RegisterType&lt;IUnitOfWork, Dapper.Repository.UnitOfWork.Data.UnitOfWork&gt;();
}

class UnityDbFactory : IDbFactory
{
    private readonly IUnityContainer _container;

    public UnityDbFactory(IUnityContainer container)
    {
        _container = container;
    }
    public T Create&lt;T&gt;() where T : class, ISession
    {
        return _container.Resolve&lt;T&gt;();
    }
    public T Create&lt;T&gt;() where T : class, ISession
    {
        return _container.Resolve&lt;T&gt;();
    }
    public T Create&lt;T&gt;(IDbFactory factory, ISession session) where T : IUnitOfWork
    {
        return _container.Resolve&lt;T&gt;(new ParameterOverride("factory", factory), 
            new ParameterOverride("session", session), new ParameterOverride("isolationLevel", IsolationLevel.Serializable));
    }
    public T Create&lt;T&gt;(IDbFactory factory, ISession session, IsolationLevel isolationLevel) where T : IUnitOfWork
    {
        return (T)Activator.CreateInstance(typeof(T), factory, session, isolationLevel);
    }
    public void Release(IDisposable instance)
    {
        _container.Teardown(instance);
    }
}</code></pre>


# Version History
- 0.0.x	
	- Created Session, UnitOfWork, IDBFactory, Repository Getters and SaveOrUpdate 
	- Castle Windsor integration
- 0.1.x	
	- Added examples and test for Autofac, Ninject, StructureMap, SimpleInjector and Unity. 
	- Bug fixes. 
	- Extended IUnitOFWork and Session for FastCRUD
- 0.2.x	(Current) 
	- Bug fix with transactions and UoW extensions (0.2.69)
	- Add UnitOfWork Creation from Factory (In Progress)
- 0.3.x (Future)
	- Add more tests
	- Add more FastCRUD standard calls in the repository	

