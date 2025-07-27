using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiHybrid
{
    public class AlumnoService
    {
        private readonly AppDbContext _context;

        public AlumnoService()
        {
            _context = new AppDbContext();
            _context.Database.EnsureCreated();
        }

        public async Task AddAlumnoAsync(Alumno alumno)
        {
            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Alumno>> GetAlumnosAsync()
        {
            return await _context.Alumnos.ToListAsync();
        }

        public async Task<Alumno> GetAlumnoByIdAsync(int id)
        {
            return await _context.Alumnos.FindAsync(id);
        }

        public async Task UpdateAlumnoAsync(Alumno alumno)
        {
            var local = _context.Alumnos.Local.FirstOrDefault(e => e.Id == alumno.Id);
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Alumnos.Update(alumno);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAlumnoAsync(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno != null)
            {
                _context.Alumnos.Remove(alumno);
                await _context.SaveChangesAsync();
            }
        }
    }
}
