namespace DataStore
{
	using Interfaces;

	public class DataStore : IDataStore
	{
		public string ConnectionString { get; set; }

		public int NumberOfUsagesOfThisInstance { get; set; }
	}
}
