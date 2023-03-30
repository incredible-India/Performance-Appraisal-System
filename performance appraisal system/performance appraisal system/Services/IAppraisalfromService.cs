using performance_appraisal_system.Migrations;
using performance_appraisal_system.Models;
namespace performance_appraisal_system.Services
{
    public interface IAppraisalfromService
    {

        //save the data in the appraisefrom database and appraisalAndcompetency database when manager first time create the appraisal from

        public void SaveDataInAppraiselDB(Appraiselform fm);

        //this will check any new appraisal form exists or not

        public dynamic CheckNewAppraisalForEmployee(int id, string status);

        //this will return the single  appraisal form based on given  appraisal ID
        public Appraiselform GetCurrentAppraisalForm(int ID);


        //return the is of the competencies 

        public List<AspprasalAndCompetencies> GetCompetencies(int ID);

        //return compitency Name take compitency id and length of total compitencies exists
        public competencies GetCompitencyName(int id);

        //return the appraisal form having the status as given in parameter

        public List<Appraiselform> GetAppraisalFormHavingStatus(string status, int empid);

        //change appraisal status
        public void ChnageAppraisalStatus(int aid, string status);

        //save the first appraisal form by user and staus will be selfrated
        public void SaveFirstAppraisalFormDetails(AppraisalDetails ad, string status, int appid);
    }
}
