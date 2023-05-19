using Kitchen;
using KitchenLib.Utils;
using KitchenMods;
using Unity.Entities;
using UnityEngine;

namespace CraftingLibParts.Customs.VendingMachine
{
    public class CreateBasicPartsVendingMachine : NightSystem, IModSystem
    {
        public struct SBasicPartsVendingMachine : IComponentData, IModComponent { }

        protected override void OnUpdate()
        {
            if (!HasSingleton<SBasicPartsVendingMachine>())
            {
                Vector3 frontDoor = GetFrontDoor(true);
                Entity entity = EntityManager.CreateEntity(typeof(CCreateAppliance), typeof(CPosition), typeof(CPartsVendor), typeof(SBasicPartsVendingMachine));
                Set(entity, new CCreateAppliance
                {
                    ID = GDOUtils.GetCustomGameDataObject<BasicPartsVendingMachine>().GameDataObject.ID
                });
                int num = ((!(frontDoor.x > 0f)) ? 1 : (-1));
                Set(entity, new CPosition(frontDoor + new Vector3(num * 2, 0f, 0f)));
            }
        }
    }
}
