using LasmartTest.Data;
using LasmartTest.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LasmartTest.Services
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private LasmartTestContext _context;

        public EquipmentRepository(LasmartTestContext ctx)
        {
            _context = ctx;
        }

        public async Task<bool> EquipmentExists(Guid equipmentId)
        {
            return  await _context.Equipments.AnyAsync(e => e.Id == equipmentId);
        }

        public async Task<Equipment> GetEquipmentAsync(Guid equipmentId)
        {
            return await _context
                .Equipments
                .FirstOrDefaultAsync(e => e.Id == equipmentId);
        }

        public async Task<IEnumerable<Equipment>> GetEquipmentsAsync()
        {
            return await _context
                        .Equipments
                        .ToListAsync();
        }

        public void UpdateEquipment(Equipment equipment)
        {
            // no code in this implementation
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

    }
}
