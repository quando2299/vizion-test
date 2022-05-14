using Microsoft.AspNetCore.Http;
using Vizion.Models;

namespace Vizion.Dtos
{
    public class CreateApplicantInput
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Note { get; set; }
        public IFormFile Resume { get; set; }
        public string Profile { get; set; }

        public Applicant MapToEntity()
        {
            return new Applicant()
            {
                Name = this.Name,
                Phone = this.Phone,
                Email = this.Email,
                ApplyPosition = this.Position,
                Note = this.Note,
                Profile = this.Profile
            };
        }
    }
    
    public class UpdateApplicantInput : CreateApplicantInput
    {
        
    }
}