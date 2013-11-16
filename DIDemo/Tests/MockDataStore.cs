namespace Tests
{
	using Interfaces;

	public class MockDataStore : IDataStore
	{
		public string ConnectionString
		{
			get { return "mock string"; }
			set { }
		}

		public int NumberOfUsagesOfThisInstance { get; set; }
    }
}
