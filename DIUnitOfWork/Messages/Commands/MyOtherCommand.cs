namespace Messages.Commands
{
	using System;

	public class MyOtherCommand :IMyOtherCommand
    {
	    public Guid IdGuid { get; set; }

		public string Name { get; set; }
	    
        public bool Throw { get; set; }
    }

	public interface IMyOtherCommand
	{
		Guid IdGuid { get; set; }
		
        string Name { get; set; }
	    
        bool Throw { get; set; }
	}
}
