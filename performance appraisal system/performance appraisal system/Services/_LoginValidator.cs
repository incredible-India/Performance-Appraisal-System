using performance_appraisal_system.Models;

namespace performance_appraisal_system.Services
{
    public interface _LoginValidator
    {
        //when user will login and click the login button retun 0 if fails
        public Employee Validate(login logDetails);
        //this will check the number of data inside the Employyee table
        public int CheckNoOfData();
        //this method will set the cookies and claims the identity
        public  void AuthProcess(Employee emp, login logDetails);


     


    }
}
