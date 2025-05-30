using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationService.DTOs;
using EducationService.Models;

namespace EducationService.Repositories
{
    public interface IEducationRepository
    {
        Task<IEnumerable<EducationReadDto>> GetAllEducationsAsync();
        Task<EducationReadDto> GetEducationByIdAsync(Guid id);
        Task<EducationCreateDto> CreateEducationAsync(EducationCreateDto educationCreateDto);
        Task<EducationUpdateDto> UpdateEducationAsync(Guid id, EducationUpdateDto educationUpdateDto);
        Task<bool> DeleteEducationAsync(Guid id);
    }
}