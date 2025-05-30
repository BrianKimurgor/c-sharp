using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationService.DTOs;
using EducationService.Models;
using EducationService.Data;
using Microsoft.EntityFrameworkCore;

namespace EducationService.Repositories
{
    public class EducationRepository : IEducationRepository
    {
        private readonly List<EducationReadDto> _educations = new List<EducationReadDto>();

        public Task<IEnumerable<EducationReadDto>> GetAllEducationsAsync()
        {
            return Task.FromResult(_educations.AsEnumerable());
        }

        public Task<EducationReadDto?> GetEducationByIdAsync(Guid id)
        {
            var education = _educations.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(education);
        }

        public Task<EducationCreateDto> CreateEducationAsync(EducationCreateDto educationCreateDto)
        {
            var newEducation = new EducationReadDto
            {
                Id = Guid.NewGuid(),
                SchoolName = educationCreateDto.SchoolName,
                Degree = educationCreateDto.Degree,
                FieldOfStudy = educationCreateDto.FieldOfStudy,
                StartDate = educationCreateDto.StartDate,
                EndDate = educationCreateDto.EndDate
            };
            _educations.Add(newEducation);
            return Task.FromResult(educationCreateDto);
        }


        public Task<EducationUpdateDto?> UpdateEducationAsync(Guid id, EducationUpdateDto educationUpdateDto)
        {
            var education = _educations.FirstOrDefault(e => e.Id == id);
            if (education != null)
            {
                education.SchoolName = educationUpdateDto.SchoolName;
                education.Degree = educationUpdateDto.Degree;
                education.FieldOfStudy = educationUpdateDto.FieldOfStudy;
                education.StartDate = educationUpdateDto.StartDate;
                education.EndDate = educationUpdateDto.EndDate;
            }
            return Task.FromResult(educationUpdateDto);
        }

        public Task<bool> DeleteEducationAsync(Guid id)
        {
            var education = _educations.FirstOrDefault(e => e.Id == id);
            if (education != null)
            {
                _educations.Remove(education);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

    }
}