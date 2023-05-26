using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using static CraftingLibParts.FixedPrefabType;

namespace CraftingLibParts
{
    public abstract class BasePrefab
    {
        private GameObject _prefab;

        public GameObject Prefab
        {
            get
            {
                if (_prefab == null)
                {
                    _prefab = GameObject.Instantiate(Main.Bundle.LoadAsset<GameObject>(AssetBundleObjectName));
                }
                return _prefab;
            }
        }
        public string Name
        {
            get
            {
                return Prefab.name;
            }
            set
            {
                Prefab.name = value;
            }
        }

        private Material _material;
        public Material Material
        {
            get
            {
                return _material;
            }
            set
            {
                ApplyMaterial(Prefab, value);
                _material = value;
            }
        }

        private Vector3 _scale = Vector3.one;
        public Vector3 Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                ApplyScale(Prefab, _scale);
                _scale = value;
            }
        }

        public Transform Parent
        {
            get
            {
                return Prefab.transform.parent;
            }
            set
            {
                SetParent(value);
            }
        }
        public void SetParent(Transform p)
        {
            SetParent(p, false);
        }
        public void SetParent(Transform p, bool worldPositionStays)
        {
            Prefab.transform.SetParent(p, worldPositionStays);
        }

        public abstract string AssetBundleObjectName { get; }
        protected abstract void ApplyMaterial(GameObject gameObject, Material material);
        protected abstract void ApplyScale(GameObject gameObject, Vector3 scale);
    }

    public abstract class StandardMonolithicPrefab : BasePrefab
    {
        protected sealed override void ApplyMaterial(GameObject gameObject, Material material)
        {
            MaterialUtils.ApplyMaterial(gameObject, "Anchor/Scale/Model", new Material[] { material });
        }

        protected sealed override void ApplyScale(GameObject gameObject, Vector3 scale)
        {
            gameObject.transform.Find("Anchor").transform.localScale = scale;
        }
    }

    public class BlockPrefab : StandardMonolithicPrefab
    {
        public override string AssetBundleObjectName => "Block";
    }
    public class CrystalPrefab : StandardMonolithicPrefab
    {
        public override string AssetBundleObjectName => "Crystal";
    }
    public class FacetedGemPrefab : StandardMonolithicPrefab
    {
        public override string AssetBundleObjectName => "FacetedGem";
    }
    public class IngotPrefab : StandardMonolithicPrefab
    {
        public override string AssetBundleObjectName => "Ingot";
    }
    public class LogPrefab : StandardMonolithicPrefab
    {
        public override string AssetBundleObjectName => "Log";
    }
    public class NuggetPrefab : StandardMonolithicPrefab
    {
        public override string AssetBundleObjectName => "Nugget";
    }
    public class RodPrefab : StandardMonolithicPrefab
    {
        public override string AssetBundleObjectName => "Rod";
    }
    public class ShardPrefab : StandardMonolithicPrefab
    {
        public override string AssetBundleObjectName => "Shard";
    }
    public class StickPrefab : StandardMonolithicPrefab
    {
        public override string AssetBundleObjectName => "Stick";
    }

    public class PlatePrefab : BasePrefab
    {
        public override string AssetBundleObjectName => "Plate";

        protected override void ApplyMaterial(GameObject gameObject, Material material)
        {
            Material[] materialArr = new Material[] { material };
            MaterialUtils.ApplyMaterial(gameObject, "Anchor/Scale/Pattern", materialArr);
            MaterialUtils.ApplyMaterial(gameObject, "Anchor/Scale/Pattern.001", materialArr);
            MaterialUtils.ApplyMaterial(gameObject, "Anchor/Scale/Base", materialArr);
        }

        protected override void ApplyScale(GameObject gameObject, Vector3 scale)
        {
            gameObject.transform.Find("Anchor").transform.localScale = scale;
        }
    }






    public static class FixedPrefabType
    {
        // Powder?

        public interface INugget { }
        public interface IIngot { }
        public interface IBlock { }
        public interface IPlate { }
        public interface IRod { }
        public interface IShard { }
        public interface ICrystal { }
        public interface IFaceted { }
        public interface ILog { }
        public interface IStick { }

        // Bottle (For gases and liquids?
    }

    public class PrefabRegistry
    {
        private static GameObject _prefabHider;
        private static Dictionary<Type, BasePrefab> _prefabs;

        private static readonly Dictionary<Type, Type> _interfaceToPrefabNameMap = new Dictionary<Type, Type>()
        {
            { typeof(INugget), typeof(NuggetPrefab) },
            { typeof(IIngot), typeof(IngotPrefab) },
            { typeof(IBlock), typeof(BlockPrefab) },
            { typeof(IPlate), typeof(PlatePrefab) },
            { typeof(IRod), typeof(RodPrefab) },
            { typeof(IShard), typeof(ShardPrefab) },
            { typeof(ICrystal), typeof(CrystalPrefab) },
            { typeof(IFaceted), typeof(FacetedGemPrefab) },
            { typeof(IStick), typeof(StickPrefab) },
            { typeof(ILog), typeof(LogPrefab) }
        };
        private const string FALLBACK_ASSET_BUNDLE_PREFAB_NAME = "Block";

        /// <summary>
        /// Gets the cube prefab for a specified Type key. If prefab does not exist, creates a new cube prefab.
        /// </summary>
        /// <typeparam name="T">Type key. Usually your CustomGameDataObject type</typeparam>
        /// <param name="scaleX">X component of local scale</param>
        /// <param name="scaleY">Y component of local scale</param>
        /// <param name="scaleZ">Z component of local scale</param>
        /// <param name="material">Cube material</param>
        /// <returns>Prefab GameObject for instantiation</returns>
        public static GameObject GetPrefab<T>(float scaleX = 1f, float scaleY = 1f, float scaleZ = 1f, Material material = null)
        {
            return GetPrefab(typeof(T), scaleX: scaleX, scaleY: scaleY, scaleZ: scaleZ, material: material);
        }

        public static GameObject GetPrefab(Type T, float scaleX = 1f, float scaleY = 1f, float scaleZ = 1f, Material material = null)
        {
            if (_prefabs == null)
            {
                _prefabs = new Dictionary<Type, BasePrefab>();
            }

            if (_prefabHider == null)
            {
                _prefabHider = new GameObject("CraftingLibPrefab Container");
                _prefabHider.SetActive(false);
            }

            if (!_prefabs.TryGetValue(T, out BasePrefab basePrefab))
            {
                basePrefab = GetNewPrefabInstance(T);
                basePrefab.SetParent(_prefabHider.transform);
                basePrefab.Scale = new Vector3(scaleX, scaleY, scaleZ);
                if (material == null)
                {
                    material = MaterialUtils.GetExistingMaterial("Metal Dark");
                }
                basePrefab.Material = material;
                _prefabs[T] = basePrefab;
            }

            return basePrefab.Prefab;
        }

        private static BasePrefab GetNewPrefabInstance(Type T)
        {
            Type prefabType = null;
            foreach (KeyValuePair<Type, Type> item in _interfaceToPrefabNameMap)
            {
                if (item.Key.IsAssignableFrom(T))
                {
                    prefabType = item.Value;
                    break;
                }
            }

            if (prefabType == null)
                return null;

            BasePrefab prefab = Activator.CreateInstance(prefabType) as BasePrefab;
            prefab.Name = $"{T.Name}_{prefabType}";

            return prefab;
        }
    }
}
