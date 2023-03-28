using performance_appraisal_system.Models;

namespace performance_appraisal_system.Services
{
    public interface ICompitencies
    {

        public  void AddCompitency(competencies com);

        public List<competencies> ListCompetencies();

        public List<string> GetCompetenciesName();
    }
}
