using performance_appraisal_system.Migrations;
using performance_appraisal_system.Models;
namespace performance_appraisal_system.Services
{
    public interface IAppraisalfromService
    {

        //save the data in the appraisefrom database and appraisalAndcompetency database when manager first time create the appraisal from

        public void SaveDataInAppraiselDB(Appraiselform fm);

        //this will check any new appraisal form exists or not

        public dynamic CheckNewAppraisalForEmployee(int id);
    }
}
