using System;
namespace vanilla.Core.Services
{

    public interface ISimpleService
    {
        string GetString();
    }
    public class SimpleService : ISimpleService
    {
        public SimpleService()
        {

        }

        public string GetString()
        {
            return "Simple";
        }
    }
}
