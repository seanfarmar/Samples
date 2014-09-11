namespace NSebHost
{
    using Nancy;

    public class MainNancyModule : NancyModule
    {
        public MainNancyModule()
        {
            Get["/"] = x => { return "Hello World"; };
        }
    }
}
