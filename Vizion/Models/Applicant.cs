using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Vizion.Models
{
    public class Applicant
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [Required(ErrorMessage = AppConst.MessageFormat.RequiredMessage)]
        [StringLength(100, MinimumLength = 5,
            ErrorMessage = "Name should be between 5 and 100 characters")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = AppConst.MessageFormat.RequiredMessage)]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [StringLength(100, MinimumLength = 5,
            ErrorMessage = "Email should be between 5 and 100 characters")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid Phone")]
        [StringLength(20, MinimumLength = 10,
            ErrorMessage = "Phone should be between 10 and 20 characters")]
        public string Phone { get; set; }
        
        [Required(ErrorMessage = AppConst.MessageFormat.RequiredMessage)]
        public string ApplyPosition { get; set; }

        public string Profile { get; set; }
        
        public string ResumeUrl { get; set; }
        
        [Required]
        public DateTime ApplyDate { get; set; } = DateTime.Now;
        
        public string Note { get; set; }
    }
}