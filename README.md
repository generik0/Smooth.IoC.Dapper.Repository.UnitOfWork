![Project Icon](https://raw.githubusercontent.com/Generik0/Smooth.IoC.Dapper.Repository.UnitOfWork/master/logo.jpg) Smooth.IoC.Dapper.Repository.UnitOfWork
===========================================

[![generik0 VSTS Build Status](https://generik0.visualstudio.com/_apis/public/build/definitions/97e62cdf-8c46-48a2-bf7a-d40bf05a53eb/2/badge)](https://www.visualstudio.com/)
[![NuGet](https://img.shields.io/nuget/v/Smooth.IoC.Dapper.Repository.UnitOfWork.svg)](http://www.nuget.org/packages/Smooth.IoC.Dapper.Repository.UnitOfWork)

This package it created to "FIX" the contradictory concepts behind the Repository and UnitOfWork patterns together with using inversition of control
 / dependancy injection.  
 Also i wanted to make the creation of sessions (IDbConnection) and UnitOFWork's (a Transaction) by the injected factory automatically 
 connection / begin transaction on creation, and disconnect/commit on disposal. Hence making database work become nice and smooth like....   
I also want to be able to do unit (integration) testing with e.g. a Sqlite but the production database engine could be e.g. a MsSQl. This is possible now... 

*So far there are examples of Autofact, Castle.Windsor, Ninject, Simpleinjector, StructureMap, and Unity.*

**WARNING: Until Smooth.IoC.Dapper.Repository.UnitOfWork reaches version 1.0, I reserve the right to make minor backwards-incompatible changes to the API.**

**Table of Contents**  *generated with [DocToc](http://doctoc.herokuapp.com/)*
- [What are the features of the library?](#)
- [About Dapper and Dapper.FastCRUD](#)
- [What does this libray do?](#)
	- [What this the package include?](#)
- [Code Examples: Sessions, Repositories and UnitOfWork](#)
		- [Session and ISession](#)
		- [Repository and IRepository](#)
		- [Using Session and UnitOFWork in a class/method](#)
- [Code Examples: IoC registration](#)
	- [Autofac registration](#Autofac-registration)  
	- [Castle Windsor Installer](#Castle-Windsor-Installer)  
	- [Ninject registration](#Ninject-registration)  
	- [Autofac registration](#Autofac-registration)  
	- [Structure Map registration](#Structure-Map-registration)  
	- [Unity registration](#Unity-registration)  
- [Version History](#Version-History)


# What are the features of the library?
The library gives you the building blocks to:
* Create Sessions from the IDbFactory in your methods, IDbFactory should be injected into your class's. As Session extends IDbConnection and will Open on the factory spawning the session and dispose the connection on disposal of the connection.
* Your Sessions can create UnitOfWork's. As Session extends IDbConnection and will Open on the factory spawning the session and dispose the connection on disposal of the connection.
* If your logic just needs "just a" UnitOfWork with a session that has the same scope, the factory can create it for you.
* The IRepository and abstract concrete class should be used on your individual repository classes to provide simple and basic calls. But ofcasue you can add all the queries you want into your 
Repositories and use the dapper and dapper.FastCRUD (or any other extensions) functionality provided to you.
* The repository abstract classes use Dapper.FastCRUD to give you a fluent ORM experience with the most common calls.
* This library does not lock you to using dapper and FastCRUD, you can use any library you like that extends IDbConnection and IDbTransation, and still use the IDbFactory, ISession and IUnitOrWork.  
* Implemented for .net 4.5.2, .net 4.6.1+, .net 1.6 standard+.

**You will have to register the IDbFactory and IUnitOfWork manually, along with your other registrations. But below in this readme and in the test examples i have added examples for: Autofac, Castle.Windsor, StructureMap, 
Ninject, Unity.  I did not want to add al sorts of IoC containers to the package, so please look at the examples. And If you have better knowledge of an IoC framework, please let me know if it 
can be done better, and smoother...**

# About Dapper and Dapper.FastCRUD
I use Dapper and Dapper.FastCRUD for my sql work. It is made by the very cool StackExchange mob.  
Dapper is a micro ORM data does only what you ask of it through queries. [Dapper](https://github.com/StackExchange/dapper-dot-net)  
There is an extension to Dapper called Dapper.FastCRUD. This adds fluentness to dapper also for complex joins etc. [Dapper.FastCRUD](https://github.com/MoonStorm/Dapper.FastCRUD).  

Dapper is not Entity, Linq2Sql nor NHibernate. It is a micro ORM and really really fast. Dapper.FastCRUD does not add the FULL fluent ORM experience. So you will need to think about what you do.  
But you are also in control of the code and optimization of calls. For me, i believe that the "big" ORMs do to much, and Dapper together with Dapper.FastCRUD is the perfect amount of just doing enough...  

The drawback with Dapper.FastCRUD is it may fail if you don't give it the wrong SqlDialect.
So i have extended FastDappers IDbConnection extensions so the projects own ISession and IUnitOfWork are extended. Then the FastCRUD IDbConnection extension method extended by ISession or IUnitOfWork extensions in
the package. This insures that the dialogue is set correct, if needed. This means that your Entity can only be used for one database type per ioc container / executing assembily. So you can also use a different databae for your tests than your production code.
* This will only effect FastCRUD calls using ISession or IUnitOrWork instances. Not IDbConnection instances.
* If you want your entity to span across more than one database, you can use the RepositoryBase to extend from bypassing the Repository abstraction.
* I have created a SqlDialectInstance (Singleton) expert that can help you set the dialogue. Please use it if you have issues with your session and sql dialect.
* I have created a SqlHelper, that insures FastCRUD's SqlDialect is set if you decide you need a FastCRUD "Sql" helper method. Please use my SqlHelper otherwise your SqlDialect might be frozen. It is available for Repositories as "Sql" a proctected property.
* **TA dialogue helper is also available in your repository through "SetDialogueOnce" method. You will need to use it for joins..** 
(Hint: if your dialogue gets stuck in the wrong state you can "reset" the FastCRUD mapping using OrmConfiguration.RegisterEntity<YouEnity>();)


You can do a lot fluently with FastCRUD. Check out there wiki:
- [Home](https://github.com/MoonStorm/Dapper.FastCRUD/wiki)
- [Entity registration](https://github.com/MoonStorm/Dapper.FastCRUD/wiki/Entity-registration)
- [Default library conventions](https://github.com/MoonStorm/Dapper.FastCRUD/wiki/Default-library-conventions)
- [JOINS](https://github.com/MoonStorm/Dapper.FastCRUD/wiki/JOINs)
- [SQL statements and clauses](https://github.com/MoonStorm/Dapper.FastCRUD/wiki/SQL-statements-and-clauses)

Or as i have already menioned use dapper or any other extension utilizing IDbConnection and IDbTransaction...

# What does this libray do?
All of the repository and UoW pattern examples i could find online did not include the usage of a factory for registration and injection. The session would typically be added to the constructor meaning when the session was disposed by one method, another method in the class could not use it any more. The examples with IoC used some very complex registration and multithreading code. But there really isn't a need for this!  
Basically something didn't seam to fix with the typical UoW and Repository patterns together with IoC.  
I also found that injecting a simple factory that could create simple IDbConnections and IDbTransactions was not good enough. Because more intelegence/help was needed.
Hence the IDbFactory, ISession, IUnitOfWork, IRepository interfaces and logic was born...  
At the same time it is very important that it be possible to use one connection for production code and another for unit testing (e.g. MsSql for production and Sqlite for testing).  
* Please note, that SqlLite only accepts IsolationLevel.Serializable for the uow. You will need to override the default IsolationLevel.RepeatableRead.
This design allows for this. As your custom session interface is used as the generic for the repository, not the session class allowing for different connection strings. 
You can even use the same database migrations if you have done code first. I have used [SimpleMigrations](https://github.com/canton7/Simple.Migrations) as it allows both console running for the production code / installer and inproc for unit testing.

You are welcome to look at the unit tests for examples or look below in this readme.

## What this the package include?
So what i have done/created is this:

1. **IDbFactory** is a simple interface that you register with your IoC. It can create/spwan ISession's and IUntOfWork's. But primary used in code to spawn Sessions or UnitofWork's with an attached session.
2. **ISession&lt;TDatabase&gt;** (and Session&lt;TDatabase&gt; abstraction):	Extends IDbConnection. You use it to extend your Database connection / Session type. Yours session classes 
and interfaces require a connection string. So If you have multiple database connections, you need 1 ISession and Session extended Interface and class per database. When the session is 
created by the factory it connects to the database, when it disposes it discontects and disposes. For Castle Windsor it also untracks the object. You can use the session for any IDbConnection or dapper (or extension) framework you like, as ISession extends IDbConnection ;-). 
The ISession also has a Dapper.FastCRUD extension so the SqlDialect is automatically set for the enitity depending on the connection.
3. **IUnitOfWork** (and UnitOfWork): Extends IDbTransaction. You don't need to extend anything with this. When you have created a session in you code, you can create a uow from the session. Then the session is created by the factory it begins a transaction (isolation i a parameter), when it disposes it commits (roleback on exception) and disposes. For Castle Windsor it also untracks the object. ;-).
The IUnitOfWork also has a Dapper.FastCRUD extension so the SqlDialect is automatically set for the enitity depending on the connection.
4. **IRepository&lt;TSession, TEntity, TPk&gt;** (Repository&lt;TSession, TEntity, TPk&gt; abstraction):	Is a default repository that you extend with your own repository for each of 
the entities you want a repository for. There as some builtin methods e.g. GetAll, Get, and SaveOrUpdate. You can add the methods you need for your entity using any IDbConnection framework. 
 have used [dapper-dot-net](https://github.com/StackExchange/dapper-dot-net) and [dapper.FastCRUD](https://github.com/MoonStorm/Dapper.FastCRUD) for the quering.
5. **IEntity&lt;TPk&gt;**: An interface for your Entities so you always have a Id key defined. You can make your entities as you please. this is only to help you.
6. **IRepositoryBase** (and RepositoryBase&lt;TEntity&gt; abstraction):	This is a vanilla base repository, you can use it if you do not want to use Dapper or Dapper.FastCRUD. 
It includes an protected method to set the dialect which you will need to do, if you want to use FastCRUD.

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
Below is an example of a repository class that extends Repository. Creating a Repository interface, Add the IRepository to your Repository interface and give it the Entity and Pk generics.
The IRepository interface and abstract class has a large number of the most primary calls to the database. E.g. 

* Get, 
* GetAll and 
* SaveOrUpdate. Both in sync and Async

 All the special calls you need in your repository can you can just add There are no restrictions ;-)

<pre><code>public interface IBraveRepository : IRepository&lt;Brave, int&gt;
{
}

public class BraveRepository : Repository&lt;Brave, int&gt;, IBraveRepository
{
    public BraveRepository(IDbFactory factory) : base(factory)
    {
        public BraveRepository(IDbFactory factory) : base(factory)
        {
        }

        //An example of a custom "join" call, if the abstract repository calls are not enough...
        public Brave GetWithInnerJoin(int key, ISession connection)
        {
            var entity = CreateInstanceHelper.Resolve&lt;Brave&gt;();
            entity.Id = key;
            return connection.Get(entity, statement =>
            {
                statement.Include&lt;New&gt;(join => join.InnerJoin())
                .Include&lt;World&gt;(join => join.InnerJoin());
            });
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

	public void DoSomething()
	{
        // Spawn a session and spawn 1-to-many UnitOFWork with the connection
		using (var session = _factory.Create&lt;ITestSession&gt;())
        {
			var myItem = _repository.GetKey(1, session);
            using (var uow = session.UnitOfWork())
			{
				_repository.SaveOrUpdate(myItem, uow);
			}
            var myItem = _repository.GetKey(myItem.Id, session);
        }

        // Spawn a session with a UnitOFWork. Their lifescope is the same..
        using (var uow = _factory.Create&lt;IUnitOFWork, ITestSession&gt;())
        {
			var myItem = _repository.GetKey(1, uow);
            _repository.SaveOrUpdate(myItem, uow);
        }
	}
}</code></pre>

Below is the simple version where we just want to get some data and the repository will created and close the connection it self.  
*This is not generally recommended*

<pre><code>public class MyClass : IMyClass
{
	private readonly IDbFactory _factory;
    private readonly IBraveRepository _repository;

    public MyClass(IDbFactory factory, IBraveRepository braveRepository)
    {
		_factory = factory;
        _repository = braveRepository;
    }

    // The base repository will Spawn a session of the generic type and dispose it after the call.
	public void DoSomething()
	{
        var myItem = _repository.GetKey&lt;ITestSession&gt;(1, session);
	}
}</code></pre>


# That simple. That smooth.#

# Code Examples: IoC registration
You need to register your own repository and session classes yourself. But using default conventions this should happen automatically in you bootsrapper, right?
Please look in the test project under "IoC_Example_Installers" for any changes or updates.

## Autofac registration
Autofac does have a factory using delegates but this does not fit the same pattern as all the other IoC. 
So one has to wrap the factory in a concrete implementation. Luckely the concrete implementation can be internal (or even private if you like).
Registration examples:	

<pre><code>public class AutofacRegistrar
{
    public void Register(ContainerBuilder builder)
    {
        builder.Register(c=&gt; new AutofacDbFactory(c.Resolve&lt;IComponentContext&gt;())).As&lt;IDbFactory&gt;().SingleInstance();
        builder.RegisterType&lt;Dapper.Repository.UnitOfWork.Data.UnitOfWork&gt;().As&lt;IUnitOfWork&gt;();
    }

    sealed class AutofacDbFactory : IDbFactory
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
        public TUnitOfWork Create&lt;TUnitOfWork, TSession&gt;(IsolationLevel isolationLevel = IsolationLevel.Serializable) where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession
        {
            return _container.Resolve&lt;TUnitOfWork&gt;(new NamedParameter("factory", _container.Resolve &lt;IDbFactory&gt;()),
                new NamedParameter("session", Create&lt;TSession&gt;()), new NamedParameter("isolationLevel", isolationLevel)
                , new NamedParameter("sessionOnlyForThisUnitOfWork", true));
        }
        public T Create&lt;T&gt;(IDbFactory factory, ISession session, IsolationLevel isolationLevel = IsolationLevel.Serializable) where T : class, IUnitOfWork
        {
            return _container.Resolve&lt;T&gt;(new NamedParameter("factory", factory),
                new NamedParameter("session", session), new NamedParameter("isolationLevel", isolationLevel));
        }
        public void Release(IDisposable instance)
        {
            instance.Dispose();
        }
    }
}</code></pre>

## Castle Windsor Installer
You need to register the factory and UnitofWork for castle to work. Castle has its own factory implemenation that creates and releases the instances for you.
However for the UnitOfWork that also creates a session, i.e. _dbFactory.Create&lt;IUnitOFWork, ISession&gt;() a custom componentSelector was needed. If you do not wish
to use the "_dbFactory.Create&lt;IUnitOFWork, ISession&gt;()" call, you can leave ther "DbFactoryComponentSelector" and registration out...

<pre><code>public class CastleWindsorInstaller : IWindsorInstaller
{
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
        if (FacilityHelper.DoesKernelNotAlreadyContainFacility&lt;TypedFactoryFacility&gt;(container))
        {
            container.Kernel.AddFacility&lt;TypedFactoryFacility&gt;();
        }
        container.Register(Component.For&lt;IUnitOfWork&gt;()
            .ImplementedBy&lt;Dapper.Repository.UnitOfWork.Data.UnitOfWork&gt;().IsFallback().LifestyleTransient());
        container.Register(Component.For&lt;DbFactoryComponentSelector, ITypedFactoryComponentSelector&gt;());
        container.Register(Component.For&lt;IDbFactory&gt;().AsFactory(c =&gt; c.SelectedWith&lt;DbFactoryComponentSelector&gt;()));
    }

    sealed class DbFactoryComponentSelector : DefaultTypedFactoryComponentSelector
    {
        private readonly IKernelInternal _kernel;

        public DbFactoryComponentSelector(IKernelInternal kernel)
        {
            _kernel = kernel;
        }
        protected override IDictionary GetArguments(MethodInfo method, object[] arguments)
        {
            var generics = method.GetGenericArguments();
            if (generics.Length != 2 || generics.First() != typeof(IUnitOfWork) || !method.Name.Equals("create", StringComparison.InvariantCultureIgnoreCase))
            {
                return base.GetArguments(method, arguments);
            }
            var uow = new Arguments
            {
                {"session", _kernel.Resolve(generics.Last())},
                {"isolationLevel", arguments[0]},
                {"sessionOnlyForThisUnitOfWork", true}
            };
            return uow;
        }
    }
}</code></pre>

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
    
    [NoIoCFluentRegistration]
    sealed class DbFactory : IDbFactory
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
        public TUnitOfWork Create&lt;TUnitOfWork, TSession&gt;(IsolationLevel isolationLevel = IsolationLevel.Serializable) where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession
        {
            return _factory.CreateUnitOwWork&lt;TUnitOfWork&gt;(this, Create&lt;TSession&gt;(), isolationLevel, true);
        }

        public T Create&lt;T&gt;(IDbFactory factory, ISession session, IsolationLevel isolationLevel = IsolationLevel.Serializable) where T : class, IUnitOfWork
        {
            return _factory.CreateUnitOwWork&lt;T&gt;(factory, session, isolationLevel);
        }
        public void Release(IDisposable instance)
        {
            _resolutionRoot.Release(instance);
        }
    }

    public interface INinjectDbFactory
    {
        T Create&lt;T&gt;() where T : ISession;
        T CreateUnitOwWork&lt;T&gt;(IDbFactory factory, ISession connection, IsolationLevel isolationLevel = IsolationLevel.Serializable, bool sessionOnlyForThisUnitOfWork = false) where T : IUnitOfWork;
    }
}</code></pre>

## Simple Injector registration
I am not very happy about the Simpleinjector example. But it works. However the concrete factory implemenation include the word "new". Simple Injector 
does not like passing runtime arguments for the constructor and as the UnitOrWork requier the session instance it is a problem. Otherwise we get a new instance.
I decided to make a concrete factory, and for the UoW it "new's" the UnitOfWork. I looked at the delegate factory pattern in simple injector, this may be a better solution for the future
But the online help on the subject was incorrect and i could not replicate.

<pre><code>public class SimpleInjectorRegistrar
{
    public void Register(Container container)
    {
        container.RegisterSingleton&lt;IDbFactory&gt;(new SimpleInjectorDbFactory(container));
    }
    public static void RegisterDisposableTransient(Container container , Type service, Type implementation )
    {
        var reg = Lifestyle.Transient.CreateRegistration(implementation, container);
        reg.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "suppressed.");
        container.AddRegistration(service, reg);
    }

    sealed class SimpleInjectorDbFactory : IDbFactory
    {
        private readonly Container _container;
        public SimpleInjectorDbFactory(Container container)
        {
            _container = container;
        }
        public T Create&lt;T&gt;() where T : class, ISession
        {
            return _container.GetInstance&lt;T&gt;();
        }
        public TUnitOfWork Create&lt;TUnitOfWork, TSession&gt;(IsolationLevel isolationLevel = IsolationLevel.Serializable) where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession
        {
            return new Dapper.Repository.UnitOfWork.Data.UnitOfWork(_container.GetInstance&lt;IDbFactory&gt;(), Create&lt;TSession&gt;(),
                isolationLevel, true) as TUnitOfWork;
        }
        public T Create&lt;T&gt;(IDbFactory factory, ISession session, IsolationLevel isolationLevel = IsolationLevel.Serializable) where T : class, IUnitOfWork
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
You need to create a concrete factory and register it, passing the container as an argument to the factory
<pre><code>public class StructureMapRegistration
{
    public void Register(IContainer container)
    {
        container.Configure(c=&gt;c.For&lt;IDbFactory&gt;()
            .UseIfNone&lt;StructureMapDbFactory&gt;().Ctor&lt;IContainer&gt;()
            .Is(container).Singleton());
    }

    sealed class StructureMapDbFactory : IDbFactory
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
        public TUnitOfWork Create&lt;TUnitOfWork, TSession&gt;(IsolationLevel isolationLevel = IsolationLevel.Serializable) where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession
        {
            return _container.With(_container.GetInstance&lt;IDbFactory&gt;()).With(Create&lt;TSession&gt;() as ISession)
                .With(isolationLevel).With(true).GetInstance&lt;TUnitOfWork&gt;();
        }
        public T Create&lt;T&gt;(IDbFactory factory, ISession session , IsolationLevel isolationLevel = IsolationLevel.Serializable) where T : class, IUnitOfWork
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

<pre><code>public class UnityRegistrar
{
    public void Register(IUnityContainer container)
    {
        container.RegisterType&lt;IDbFactory, UnityDbFactory&gt;(new ContainerControlledLifetimeManager(),
            new InjectionConstructor(container));
        container.RegisterType&lt;IRepositoryFactory, RepositoryFactory&gt;(new ContainerControlledLifetimeManager(),
            new InjectionConstructor(container));
        container.RegisterType&lt;IUnitOfWork, Dapper.Repository.UnitOfWork.Data.UnitOfWork&gt;();
    }

    sealed class UnityDbFactory : IDbFactory
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

        public TUnitOfWork Create&lt;TUnitOfWork, TSession&gt;(IsolationLevel isolationLevel = IsolationLevel.Serializable) where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession
        {
            return _container.Resolve&lt;TUnitOfWork&gt;(
                new ParameterOverride("factory", _container.Resolve&lt;IDbFactory&gt;()),
                new ParameterOverride("repositoryFactory", _container.Resolve&lt;IRepositoryFactory&gt;()),
                new ParameterOverride("session", Create&lt;TSession&gt;()),
                new ParameterOverride("isolationLevel", isolationLevel),
                new ParameterOverride("sessionOnlyForThisUnitOfWork", true));
        }
		
        public T Create&lt;T&gt;(IDbFactory factory, ISession session, IsolationLevel isolationLevel = IsolationLevel.Serializable) where T : class, IUnitOfWork
        {
            return _container.Resolve&lt;T&gt;(
                new ParameterOverride("factory", factory),
                new ParameterOverride("repositoryFactory", _container.Resolve&lt;IRepositoryFactory&gt;()),
                new ParameterOverride("session", session),
                new ParameterOverride("isolationLevel", isolationLevel),
                new ParameterOverride("sessionOnlyForThisUnitOfWork", false));
        }

        public void Release(IDisposable instance)
        {
        	_container.Teardown(instance);
        }
    }
	
    sealed class RepositoryFactory : IRepositoryFactory
    {
        private readonly IUnityContainer _container;

        public RepositoryFactory(IUnityContainer container)
        {
        	_container = container;
        }

        public TRepository GetRepository&lt;TRepository&gt;(IUnitOfWork uow) where TRepository : IRepository
        {
        	return _container.Resolve&lt;TRepository&gt;(
        		new ParameterOverride("uow", uow)
        	);
        }
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
- 0.2.x
	- Bug fix with transactions and UoW extensions (0.2.69)
	- Add UnitOfWork Creation from Factory  (0.2.73)	
    - Change so DbConnection does not have to be passed to the repo. Instead an adhoc session made by repo has the ISession as generic (0.2.73)	
    - Seperate Async and Sync calls (0.2.73)	
    - Remove need for IEntity to get and set key value (0.2.73)	
- 0.3.x (Done)
	- Add more tests for repository calls (0.3.4)
	- Change so it isn't allows async methods being called in repo (also for sync)  (0.3.4)
	- Add more FastCRUD standard calls (Delete and Count) with tests in the repository  (0.3.4)
	- Change all async tests to use AssetDoesNotThrowAsync  (0.3.4)
	- Fixed issue where factory.Create<IUnitOFWork, ISession>() did not set the sql dialect.(0.3.0)
	- Minimise the use of reflections in session and uow extensions  (0.3.4)
	- Add IComparable constraint to TPk (0.3.5)
	- Bug fixes and improved the collections not to be static (0.3.8)
	- For Asp.Net constructor injection of session insured that a uow in session or command reopened the connection if it is closed. (0.3.9)
	- Made UnitofWork default to IsolationLevel.RepeatableRead instead of IsolationLevel.Serializable (0.3.15)
	- Made all repository methods be virtual so the can be overriden. (0.3.15)
	- Add dictionaries to minimise reflections. (0.3.18)
- 0.4.x (Done)
	- Make plan IEntity queries use pure dapper but maybe use FastCRUD SQL builder? (0.4.0)
    	- Split nuspec up so Dapper and FastDapper are not resolved with Session, UnitOfWork, etc. (0.4.0)
	- Removed IDbTransaction from Uow as it only gave problems (0.4.0)
- 0.5.x (Started)
	- Update to Vs 2017 (0.5.1)
	- Add net40 support for UnitOfWork Package (done)
	- Make the Smooth repo use the uow nuget (done)
	- Add where and parameter paramateres into uow and session  extensions. And expand Repository. (In Progress)
    	- Add FastCRUD bulk methods with tests to repo.
	- Add more Xml Summaries for all used interfaces.
- 0.6.x (Future)
	- Look into making Session inherit DbConnection
    
