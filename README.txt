This project is to easier use IoC with:
	
	* ADO dbconnection
	* Dapper
	* Dapper.FastCRUD 
	* Any other Dapper extension yhou like, or even a framework that uses IDbConnect.
	* And IoC, so far supported / Documented:
		* Castle Windsor 
	* A repository pattern (base classes)
	* The IoC can resolve a IDbFactory e.g. via constructor injection. That can create connections and transactions wrapped as Sessions and UnitOrWork's
	* Use the factory to "spawn" sessions in your methods. The creation will connect to the database, the disposal will close the session.
	* The session can spawn Unit of work transactions. The transaction will begin when the UoW is created. The disposal will commit it.

Nice and smooth like.

You are welcom to look at the test cases how to use.
Checkout the Castle project for the castle windsor installer... More IoC will come.

#### Code examples ####:

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
