using KitchenLib.Customs;
using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenCraftingLibTest
{
    public static class TestCubeManager
    {
        private static GameObject _testCubesHider;
        private static Dictionary<Type, GameObject> _testCubePrefabs;

        public static GameObject GetPrefab<T>(float scale = 1f, Material material = null) where T : ICustomHasPrefab
        {
            Type type = typeof(T);
            if (_testCubePrefabs == null)
            {
                _testCubePrefabs = new Dictionary<Type, GameObject>();
            }

            if (_testCubesHider == null)
            {
                _testCubesHider = new GameObject("Test Cube Container");
                _testCubesHider.SetActive(false);
            }

            if (!_testCubePrefabs.TryGetValue(type, out GameObject prefab))
            {
                prefab = GameObject.Instantiate(Main.Bundle.LoadAsset<GameObject>("TestCube"));
                prefab.name = $"{type.Name}_TestCube";
                prefab.transform.SetParent(_testCubesHider.transform);
                Transform childCube = prefab.transform.Find("Cube");
                if (childCube != null)
                    childCube.localScale = scale * Vector3.one;

                if (material == null)
                {
                    material = MaterialUtils.GetExistingMaterial("Metal Dark");
                }
                MaterialUtils.ApplyMaterial(prefab, "Cube", new Material[] { material });

                _testCubePrefabs[type] = prefab;
            }

            return prefab;
        }
    }
}
