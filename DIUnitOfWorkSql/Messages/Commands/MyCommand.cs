namespace Messages.Commands
{
	using System;

	public class MyCommand :IMyCommand
    {
	    public Guid IdGuid { get; set; }

		public string Name { get; set; }
	    
        public bool Throw { get; set; }
    }

	public interface IMyCommand
	{
		Guid IdGuid { get; set; }
		
        string Name { get; set; }
	    
        bool Throw { get; set; }
	}
}
