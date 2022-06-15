using Leopotam.Ecs;
using Rts.Models.Configs;
using Rts.Models.Item;

namespace Rts.Services.Inventory
{
    public interface IInventoryService
    {
        public void AddToInventory(EcsEntity entity, ItemConfig config);
        
        public bool RemoveFromInventory(EcsEntity entity, int index);
        
        public bool Equip(EcsEntity entity, int index);
        
        public bool UnEquip(EcsEntity entity, EEquipSlot slot);
    }
}