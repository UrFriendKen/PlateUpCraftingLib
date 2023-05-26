using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using static CraftingLibParts.FixedPrefabType;

namespace CraftingLibParts
{
    public static class FixedPrefabType
    {
            public interface IBar { }   
            public interface IBlock { }
            public interface IPlate { }
            public interface IRod { }
            public interface IShard { }
            public interface ICrystal { }
            public interface IFaceted { }
            public interface ILog { }
            public interface IStick { }
    }

    public class PrefabRegistry
    {
        private static GameObject _prefabHider;
        private static Dictionary<Type, GameObject> _prefabs;

        private static Dictionary<Type, string> _interfaceToPrefabNameMap = new Dictionary<Type, string>()
        {
            { typeof(IBlock), "Block" },
            { typeof(IStick), "Stick" },
            { typeof(ILog), "Log" },
            { typeof(IShard), "Shard" },
            { typeof(ICrystal), "Crystal" },
            { typeof(IFaceted), "FacetedGem" },
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
                _prefabs = new Dictionary<Type, GameObject>();
            }

            if (_prefabHider == null)
            {
                _prefabHider = new GameObject("CraftingLibPrefab Container");
                _prefabHider.SetActive(false);
            }

            if (!_prefabs.TryGetValue(T, out GameObject prefab))
            {
                prefab = GetCorrectAssetBundlePrefabCopy(T);
                prefab.transform.SetParent(_prefabHider.transform);
                Transform anchor = prefab.transform.Find("Anchor");
                if (anchor != null)
                {
                    anchor.localScale = new Vector3(scaleX, scaleY, scaleZ);

                    if (material == null)
                    {
                        material = MaterialUtils.GetExistingMaterial("Metal Dark");
                    }
                    MaterialUtils.ApplyMaterial(prefab, "Anchor/Scale/Model", new Material[] { material });
                }

                _prefabs[T] = prefab;
            }

            return prefab;
        }

        public static GameObject GetCorrectAssetBundlePrefabCopy<T>()
        {
            return GetCorrectAssetBundlePrefabCopy(typeof(T));
        }

        public static GameObject GetCorrectAssetBundlePrefabCopy(Type T)
        {
            string prefabName = FALLBACK_ASSET_BUNDLE_PREFAB_NAME;
            foreach (KeyValuePair<Type, string> item in _interfaceToPrefabNameMap)
            {
                if (item.Key.IsAssignableFrom(T))
                {
                    prefabName = item.Value;
                    break;
                }
            }
            GameObject gO = GameObject.Instantiate(Main.Bundle.LoadAsset<GameObject>(prefabName));
            gO.name = $"{T.Name}_{prefabName}";

            return gO;
        }
    }
}
