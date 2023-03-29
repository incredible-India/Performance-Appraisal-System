using performance_appraisal_system.Services;
using performance_appraisal_system.Models;
using performance_appraisal_system.Data;
using System.Threading.Tasks.Dataflow;

namespace performance_appraisal_system.Repository
{
    public class AppraisalFormService : IAppraisalfromService
    {

        private readonly EmployeeContext _app;



        public AppraisalFormService(EmployeeContext app)
        {
            _app = app;
            
        }



        //this will save the information when manager create first appraisal form to employee 
        public void SaveDataInAppraiselDB(Appraiselform fm) { 
        
            Appraiselform appraiselform = new Appraiselform()
            {
                EmployeeID = fm.EmployeeID,
                EndDate = fm.EndDate,
                StartDate = fm.StartDate,
                ManagerID = fm.ManagerID,
                objective = fm.objective,
                Status = fm.Status,
                
            };

            //saving in the appraisalfrom dbs
        _app.AppraiselForm.Add(appraiselform);
           _app.SaveChanges();

            //once data will store  neo we will store in the Appraisal and competency table...

            //first get the list of all competencies

                var  compi = fm.tempCompetency;
            foreach (var comp in compi)
            {
                AspprasalAndCompetencies ac = new AspprasalAndCompetencies()
                {

                    AppID = appraiselform.AID,
               Compitency = comp
               
            };
            _app.AspprasalAndCompetencies.Add(ac);
            _app.SaveChanges();

        }

        

        }


        //here we will check any new appraisal for employee

        public dynamic CheckNewAppraisalForEmployee(int id)
        {
            //first check by id...
           var appraisalForm = _app.AppraiselForm.Where(emp => emp.EmployeeID == id);

            if(appraisalForm.Any()) return appraisalForm;
            else {//if no appraisal form exist for the current employee
                return null;
            }


        }

    }
}
