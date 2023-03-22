using performance_appraisal_system.Models;

namespace performance_appraisal_system.Services
{
    public interface _LoginValidator
    {
        //when user will login and click the login button retun 0 if fails
        public dynamic Validate(login logDetails);
    }
}
