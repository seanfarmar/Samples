namespace Interfaces
{
	public interface  IDataStore
	{
		string ConnectionString { get; set; }

		int NumberOfUsagesOfThisInstance { get; set; }
	}
}