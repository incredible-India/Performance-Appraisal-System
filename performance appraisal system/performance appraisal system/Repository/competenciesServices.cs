using performance_appraisal_system.Data;

using performance_appraisal_system.Models;
using performance_appraisal_system.Services;

namespace performance_appraisal_system.Repository
{
    public class competenciesServices: ICompitencies
    {

        private readonly EmployeeContext _comp;

        public competenciesServices(EmployeeContext comp)
        {
            
            _comp = comp;
        }



        //for adding compitecies

        public void AddCompitency(competencies com)
        {

            competencies compitencies = new competencies()
            {
                CompetencyName = com.CompetencyName,
                TypeC = com.TypeC

            };

            _comp.competencies.Add(compitencies);
            _comp.SaveChanges();

        }


        public List<competencies> ListCompetencies()
        {
            return _comp.competencies.ToList();
        }


        public List<string> GetCompetenciesName() { 
        
        return _comp.competencies.Select(m=> m.CompetencyName).ToList();    
                
                
                }


      
    }
}
