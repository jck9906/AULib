using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using AULib;
using UnityEngine.AddressableAssets;
using System;

namespace AULib
{
    [CreateAssetMenu(fileName = "NewAssetPreLoadTable", menuName = "AULib/Create Asset preLoad table", order = 0)]
    public class AssetPreloadTable : ScriptableObject
    {
        [Multiline]
        public string Guide;

        [SerializeField] private List<string> Addresses;
        [SerializeField] private List<AssetLabelReference> Labels;

        public List<string> GetKeys()
        {
            List<string> result = new(Addresses);           
            result.AddRange(Labels.Select(item => item.labelString));
            return result;
        }
        
    }
}
