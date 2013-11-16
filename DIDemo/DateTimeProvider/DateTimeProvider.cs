namespace DateTimeProvider
{
	using System;
	using Interfaces;

	public class DateTimeProvider : ITimeProvider
	{
		public DateTime Now
		{
			get { return DateTime.Now; }
		}
		public DateTime UtcNow
		{
			get { return DateTime.UtcNow; }
		}
	}
}
