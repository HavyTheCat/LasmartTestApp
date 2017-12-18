using LasmartTest.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LasmartTest.Services
{
    public interface IEquipmentRepository
    {
        Task<IEnumerable<Equipment>> GetEquipmentsAsync();
        Task<Equipment> GetEquipmentAsync(Guid equipmentId);
        Task<bool> EquipmentExists(Guid equipmentId);
        void UpdateEquipment(Equipment equipment);
        Task<bool> SaveAsync();
    }
}
