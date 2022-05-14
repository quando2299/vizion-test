using System;
using System.Linq.Expressions;
using Vizion.Models;

namespace Vizion.Dtos
{
    public class ApplicantDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public DateTime ApplyDate { get; set; }
        public string Note { get; set; }

        public static ApplicantDto MapToDto(Applicant entity = null)
        {
            entity ??= new Applicant();

            return new ApplicantDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Phone = entity.Phone,
                Email = entity.Email,
                Position = entity.ApplyPosition,
                ApplyDate = entity.ApplyDate,
                Note = entity.Note
            };
        }
    }
    
    public class ApplicantDetailDto : ApplicantDto
    {
        public string Profile { get; set; }
        public string Resume { get; set; }
        
        public static ApplicantDetailDto MapToDto(Applicant entity = null)
        {
            entity ??= new Applicant();
            
            return new ApplicantDetailDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Phone = entity.Phone,
                Email = entity.Email,
                Position = entity.ApplyPosition,
                ApplyDate = entity.ApplyDate,
                Note = entity.Note,
                Profile = entity.Profile,
                Resume = entity.ResumeUrl
            };
        }
    }
}