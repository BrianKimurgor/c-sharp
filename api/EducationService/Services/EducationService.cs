using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationService.DTOs;
using EducationService.Models;
using EducationService.Data;
using Microsoft.EntityFrameworkCore;


namespace EducationService.Services
{
    public class EducationService : IEducationService
    {
        private readonly EducationDbContext _context;

        public EducationService(EducationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EducationReadDto>> GetAllEducationsAsync()
        {
            var educations = await _context.Educations.ToListAsync();
            return educations.Select(e => new EducationReadDto
            {
                Id = e.Id,
                SchoolName = e.SchoolName,
                Degree = e.Degree,
                FieldOfStudy = e.FieldOfStudy,
                StartDate = e.StartDate,
                EndDate = e.EndDate
            });
        }

        public async Task<EducationReadDto> GetEducationByIdAsync(Guid id)
        {
            var education = await _context.Educations.FindAsync(id);
            if (education == null) return null;

            return new EducationReadDto
            {
                Id = education.Id,
                SchoolName = education.SchoolName,
                Degree = education.Degree,
                FieldOfStudy = education.FieldOfStudy,
                StartDate = education.StartDate,
                EndDate = education.EndDate
            };
        }

        public async Task<EducationReadDto> CreateEducationAsync(EducationCreateDto educationCreateDto)
        {
            var education = new EducationModel
            {
                Id = Guid.NewGuid(),
                SchoolName = educationCreateDto.SchoolName,
                Degree = educationCreateDto.Degree,
                FieldOfStudy = educationCreateDto.FieldOfStudy,
                StartDate = educationCreateDto.StartDate,
                EndDate = educationCreateDto.EndDate
            };

            _context.Educations.Add(education);
            await _context.SaveChangesAsync();

            return new EducationReadDto
            {
                Id = education.Id,
                SchoolName = education.SchoolName,
                Degree = education.Degree,
                FieldOfStudy = education.FieldOfStudy,
                StartDate = education.StartDate,
                EndDate = education.EndDate
            };
        }

        public async Task<EducationReadDto> UpdateEducationAsync(Guid id, EducationUpdateDto educationUpdateDto)
        {
            var education = await _context.Educations.FindAsync(id);
            if (education == null) return null;

            education.SchoolName = educationUpdateDto.SchoolName;
            education.Degree = educationUpdateDto.Degree;
            education.FieldOfStudy = educationUpdateDto.FieldOfStudy;
            education.StartDate = educationUpdateDto.StartDate;
            education.EndDate = educationUpdateDto.EndDate;

            await _context.SaveChangesAsync();

            return new EducationReadDto
            {
                SchoolName = education.SchoolName,
                Degree = education.Degree,
                FieldOfStudy = education.FieldOfStudy,
                StartDate = education.StartDate,
                EndDate = education.EndDate
            };
        }

        public async Task<bool> DeleteEducationAsync(Guid id)
        {
            var education = await _context.Educations.FindAsync(id);
            if (education == null) return false;

            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}