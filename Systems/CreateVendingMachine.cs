using CraftingLib.Customs.VendingMachine;
using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenLib.Utils;
using KitchenMods;
using System.Collections.Generic;
using System.Linq;
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
                ctx.Set(entity, new CPosition(frontDoor + new Vector3(num * 2, 0f, 0f)));
                ctx.Set(entity, new CDoNotPersist());
                IEnumerable<AppliancePart> parts = GameData.Main.Get<AppliancePart>();
                DynamicBuffer<CVendorOption> options = ctx.AddBuffer<CVendorOption>(entity);
                foreach (AppliancePart appliancePart in GameData.Main.Get<AppliancePart>())
                {
                    if (appliancePart.IsPurchasable)
                    {
                        options.Add(new CVendorOption()
                        {
                            ID = appliancePart.ID,
                            PurchaseCost = appliancePart.PurchaseCost
                        });
                    }
                }
            }
        }
    }
}
