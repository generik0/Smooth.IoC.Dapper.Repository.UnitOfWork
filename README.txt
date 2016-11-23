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
Checout the Castle project for the castle windsor installer... More IoC will come.


//Creating a session, Extend the session base with your dbconnection type:
public class TestSession : Session<SQLiteConnection>, ITestSession (Remember your default interface for the IoC)
{
    public TestSession(IDbFactory session)
        : base(session, "Data Source=:memory:;Version=3;New=True;")
    {
    }
}

//Creating a Repository interface, Add the IRepository to your Repository interface and give it the Entity and Pk generics
public interface IBraveRepository : IRepository<Brave, int>
{
}
//Creating a Repository , Add the Repository base to your Repository class and give it the Entity and Pk generics
public class BraveRepository : Repository<ITestSession,Brave, int>, IBraveRepository
{
    public BraveRepository(IDbFactory factory) : base(factory)
    {
    }
}


//Using an session in a class and getting unit of work. 
	//Creating the session will connect to the database. The disposal will close connection
	//Creating a unitofwork will create a transaction for the session. The disposal commit the transaction
public class MyClass : IMyClass
{
	private readonly IDbFactory _factory
    public MyClass(IDbFactory factory)
    {
		_factory = factory
    }

	publiv void DoWork()
	{
		using (var session = _factory.CreateSession<ITestSession>())
        {
			var myItem = session.GetKey(1);
            using (var uow = session.UnitOfWork())
			{
				uow.SaveOrUpdate(myItem);
			}
        }
	}
}
