using performance_appraisal_system.Services;
using performance_appraisal_system.Models;
using performance_appraisal_system.Data;
using System.Threading.Tasks.Dataflow;
using performance_appraisal_system.Migrations;

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

        public dynamic CheckNewAppraisalForEmployee(int id,string status)
        {
            //first check by id...
           var appraisalForm = _app.AppraiselForm.Where(emp => emp.EmployeeID == id && emp.Status == status);

            if(appraisalForm.Any()) return appraisalForm;
            else {//if no appraisal form exist for the current employee
                return null;
            }


        }

        //return current appraisal form based on the appraisal ID

        public Appraiselform GetCurrentAppraisalForm(int ID)
        {
            var appraisalForm = _app.AppraiselForm.Where(emp => emp.AID== ID).FirstOrDefault();

            if(appraisalForm == null)
                return null;
            else
            return appraisalForm;
        }

        //return the current compitencies take appid  only compitency id

        public List<AspprasalAndCompetencies> GetCompetencies(int ID)
        {
            return _app.AspprasalAndCompetencies.Where(m=>m.AppID==ID).ToList();
        }
        //return compitency Name take compitency id and length of total compitencies exists
        public competencies GetCompitencyName(int id)
        {
            //compitencies id will store in the k
            var k = _app.AspprasalAndCompetencies.Where(m => m.AppID == id).ToList();

            //return comptencies object
            var c = _app.competencies.Where(m=>m.ID == id).FirstOrDefault();

            return c;


        }
        //return the appraisal form on the basis of given status and the id
        public List<Appraiselform> GetAppraisalFormHavingStatus(string status,int empid)
        {
            return _app.AppraiselForm.Where(m=>m.ManagerID == empid && m.Status == status).ToList();
        }


        //this will change the status from new TO given status

        public void ChnageAppraisalStatus(int aid,string status)
        {
            var k =_app.AppraiselForm.Where(m=>m.AID ==aid).FirstOrDefault();

            k.Status = status;

           _app.SaveChanges();



        }


        //save the first appraisal from submitted by Emplyee and change the status i.e SelfRated..

        public void SaveFirstAppraisalFormDetails(AppraisalDetails ad,string status,int appid)
        {
            //list of the compitencies (ID)
            var compitenciesList = GetCompetencies(appid);

            //for each comment we will save in appraisal database
            for (int i =0; i<ad.EComments.Length;i++)
            {
              
                AppraisalDetails appraisal = new AppraisalDetails()
                {
                    EmployeeRating = ad.ERating[i],
                    AppraisalID = appid,
                    EmployeeComment = ad.EComments[i],
                    ManagerComment = ad.ManagerComment,
                    ManagerRating = ad.ManagerRating,
                    Competency = compitenciesList[i].Compitency,


                };

                _app.appraisalDetails.Add(appraisal);
                _app.SaveChanges();
            }

            //now changing the appraisal status

            Appraiselform? af=  _app.AppraiselForm.Where(m=>m.AID == appid).FirstOrDefault();

            af.Status = status;

            _app.SaveChanges();


        }

        public List<AppraisalDetails> GetAppraisalFormDetails(int aid)
        {
            return _app.appraisalDetails.Where(m=>m.AppraisalID == aid).ToList();
        }



        //saving information from manager side and status will be rated...

        public void SaveInformationFromManagerSide(int appid,AppraisalDetails ad,string status)
        {
            //get the appraisal form
            var k = _app.AppraiselForm.Where(m=>m.AID==appid).FirstOrDefault();

            if (k != null)
            {

         //   k.Status = status;
         //get the related compitencies
                var compi = _app.AspprasalAndCompetencies.Where(m=>m.AppID==appid).ToList();

                int len = compi.Count;

            for(int i=0;i<len;i++)
                {

                    var apd = _app.appraisalDetails.Where(m => m.AppraisalID == appid && m.Competency == compi[i].Compitency).FirstOrDefault();

                    if(apd != null)
                    {
                        apd.ManagerComment = ad.MComments[i];
                        apd.ManagerRating = ad.MRating[i];


                        _app.SaveChanges();
                    }

                }
        

            k.Status =status;
                
                
                _app.SaveChanges();
                 

                   



            

            }

            

        }

        //give the appraisal form only for current login user or the user belongs to this form
        public Appraiselform GetCurrentAppraisalFormForCurrentEmployee(int ID,int Empid,string status)
        {
            var appraisalForm = _app.AppraiselForm.Where(emp => emp.AID == ID && emp.EmployeeID == Empid && emp.Status==status).FirstOrDefault();

            if (appraisalForm == null)
                return null;
            else
                return appraisalForm;
        }


        public Appraiselform GetCurrentAppraisalFormForCurrentManager(int ID, int Mid, string status)
        {
            {
                var appraisalForm = _app.AppraiselForm.Where(emp => emp.AID == ID && emp.ManagerID == Mid && emp.Status == status).FirstOrDefault();

                if (appraisalForm == null)
                    return null;
                else
                    return appraisalForm;
            }
        }

        //return all appraisal form having given status

        public List<Appraiselform> AllApraisalHavingStatus(string status)
        {
            return _app.AppraiselForm.ToList();
        }


        public Appraiselform GetCurrentAppraisalFormHR(int aid, string status)
        {

            return _app.AppraiselForm.Where(m=>m.AID == aid && m.Status == status).FirstOrDefault();
        }
    }
}
