using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vizion.Dtos;
using Vizion.Services;

namespace Vizion.Controllers
{
    public class ApplicantController : BaseController
    {
        private readonly IApplicantService _applicantService;
        private readonly IBlobService _blobService;
        private readonly string[] _supportedTypes = new[] { "txt", "doc", "docx", "pdf", "xls", "xlsx" };

        public ApplicantController(
            IApplicantService applicantService,
            IBlobService blobService
            )
        {
            _applicantService = applicantService;
            _blobService = blobService;
        }
        [HttpGet]
        public IQueryable Get()
        {
            var query = this._applicantService.Get();
            var result = query.OrderByDescending(x => x.ApplyDate).Select(item => ApplicantDto.MapToDto(item)).AsQueryable();

            return result;
        }
        
        [HttpGet("{id}")]
        public ApplicantDetailDto Get(string id)
        {
            var query = this._applicantService.Get(id);

            if (query == null) throw new KeyNotFoundException();

            var result = ApplicantDetailDto.MapToDto(query);
    
            return result;
        }

        [HttpPost]
        public async Task<object> Create(CreateApplicantInput model)
        {
            var entity = model.MapToEntity();

            this._applicantService.Create(entity);
            
            IFormFile file = model.Resume ?? null;
            
            if (file != null && !_supportedTypes.Contains(System.IO.Path.GetExtension(file.FileName).Substring(1)))
            {
                throw new NotSupportedException("File Extension Is InValid - Only Upload WORD/PDF File");
            }

            if (file != null && _supportedTypes.Contains(System.IO.Path.GetExtension(file.FileName).Substring(1)))
            {
                var result = await this._blobService.UploadFileBlobAsync($"applicants/{entity.Id}", 
                    file.OpenReadStream(),
                    file.ContentType,
                    file.FileName);

                entity.ResumeUrl = result.AbsoluteUri;
            }
            
            this._applicantService.Update(entity.Id, entity);

            return null;
        }
        
        [HttpPut]
        public async Task<object> Update(string id, UpdateApplicantInput model)
        {
            var entity = this._applicantService.Get(id);

            if (entity == null) throw new KeyNotFoundException();
            
            IFormFile file = model.Resume ?? null;
            
            if (file != null && !_supportedTypes.Contains(System.IO.Path.GetExtension(file.FileName).Substring(1)))
            {
                throw new NotSupportedException("File Extension Is InValid - Only Upload WORD/PDF File");
            }

            if (file != null && _supportedTypes.Contains(System.IO.Path.GetExtension(file.FileName).Substring(1)))
            {
                await this._blobService.DeleteBlobFile($"applicants/{entity.Id}", Path.GetFileName(entity.ResumeUrl));
                var result = await this._blobService.UploadFileBlobAsync($"applicants/{entity.Id}", 
                    file.OpenReadStream(),
                    file.ContentType,
                    file.FileName);

                entity.ResumeUrl = result.AbsoluteUri;
            }
            this._applicantService.Update(entity.Id, entity);
            
            return null;
        }

        [HttpDelete("{id}")]
        public object Delete(string id)
        {
            var model = this._applicantService.Get(id);

            if (model == null) throw new KeyNotFoundException();
            
            this._applicantService.Remove(model.Id);
            return null;
        }
    }
}