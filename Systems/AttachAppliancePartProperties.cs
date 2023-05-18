using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateInGroup(typeof(CreationGroup))]
    public class AttachAppliancePartProperties : GenericSystemBase, IModSystem
    {
        EntityQuery Attaches;
        protected override void Initialise()
        {
            base.Initialise();
            Attaches = GetEntityQuery(typeof(CAppliancePart), typeof(CAttachAppliancePartProperties));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> entities = Attaches.ToEntityArray(Allocator.Temp);
            using NativeArray<CAppliancePart> parts = Attaches.ToComponentDataArray<CAppliancePart>(Allocator.Temp);

            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                CAppliancePart part = parts[i];
                EntityManager.RemoveComponent<CAttachAppliancePartProperties>(entity);

                if (GameData.Main.TryGet(part.ID, out AppliancePart partGDO, warn_if_fail: true))
                {
                    foreach (IAppliancePartProperty property in partGDO.Properties)
                    {
                        Set(entity, (dynamic)property);
                    }
                }
            }
        }
    }
}
