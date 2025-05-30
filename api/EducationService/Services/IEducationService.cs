using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationService.DTOs;

namespace EducationService.Services
{
    public interface IEducationService
    {
        Task<IEnumerable<EducationReadDto>> GetAllEducationsAsync();
        Task<EducationReadDto> GetEducationByIdAsync(Guid id);
        Task<EducationReadDto> CreateEducationAsync(EducationCreateDto educationCreateDto);
        Task<EducationReadDto> UpdateEducationAsync(Guid id, EducationUpdateDto educationUpdateDto);
        Task<bool> DeleteEducationAsync(Guid id);
    }
}