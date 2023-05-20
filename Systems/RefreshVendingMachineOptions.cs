using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenMods;
using System.Collections.Generic;
using Unity.Entities;

namespace CraftingLib.Systems
{
    public class RefreshVendingMachineOptions : NightSystem, IModSystem
    {
        public struct SRefreshOptions : IComponentData, IModComponent { }

        private static RefreshVendingMachineOptions _instance;

        protected override void Initialise()
        {
            base.Initialise();
            _instance = this;
        }

        protected override void OnUpdate()
        {
            if (!TryGetSingletonEntity<CreateVendingMachine.SVendingMachine>(out Entity singletonEntity))
                return;

            EntityContext ctx = new EntityContext(EntityManager);
            if (Has<SRefreshOptions>())
            {
                IEnumerable<AppliancePart> parts = GameData.Main.Get<AppliancePart>();
                if (!RequireBuffer<CVendorOption>(singletonEntity, out DynamicBuffer<CVendorOption> options))
                {
                    options = ctx.AddBuffer<CVendorOption>(singletonEntity);
                }
                options.Clear();
                foreach (AppliancePart appliancePart in parts)
                {
                    if (appliancePart.IsPurchasable) // To add check for requirements (Possible attachment points/partial appliances exist?)
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

        public static void RequestRefresh()
        {
            Entity singletonEntity = default;
            bool? found = _instance?.TryGetSingletonEntity<CreateVendingMachine.SVendingMachine>(out singletonEntity);
            if (!found.HasValue || !found.Value)
                return;
            _instance.Set<SRefreshOptions>(singletonEntity);
        }
    }
}
