using CraftingLib.Customs.VendingMachine;
using Kitchen;
using KitchenLib.Utils;
using KitchenMods;
using Unity.Entities;
using UnityEngine;

namespace CraftingLib.Systems
{
    public class CreateVendingMachine : NightSystem, IModSystem
    {
        public struct SVendingMachine : IComponentData, IModComponent { }

        protected override void Initialise()
        {
            base.Initialise();
        }

        protected override void OnUpdate()
        {
            if (!HasSingleton<SVendingMachine>())
            {
                EntityContext ctx = new EntityContext(EntityManager);

                Vector3 frontDoor = GetFrontDoor(true);
                Entity entity = ctx.CreateEntity();
                ctx.Set(entity, new SVendingMachine());
                ctx.Set(entity, new CCreateAppliance
                {
                    ID = GDOUtils.GetCustomGameDataObject<VendingMachine>().GameDataObject.ID
                });
                int num = ((!(frontDoor.x > 0f)) ? 1 : (-1));
                ctx.Set(entity, new CPosition(frontDoor + new Vector3(num * 5, 0f, 0f)));
                ctx.Set(entity, new CDoNotPersist());
                ctx.Set(entity, new RefreshVendingMachineOptions.SRefreshOptions());
            }
        }
    }
}
