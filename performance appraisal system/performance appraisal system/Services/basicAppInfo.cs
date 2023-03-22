namespace performance_appraisal_system.Services
{
    public class basicAppInfo : _basicAppInfo
    {
        //dependency injection for the 
        private readonly IConfiguration _configuration;

        public basicAppInfo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string AppName()
        {

            return _configuration["AppName"];

        }
    }
}
