using System.Collections.Generic;
using Vizion.Models;

namespace Vizion.Services
{
    public interface IApplicantService
    {
        List<Applicant> Get();
        Applicant Get(string id);
        Applicant Create(Applicant model);
        void Update(string id, Applicant model);
        void Remove(string id);
    }
}