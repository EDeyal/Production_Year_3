using System.Collections.Generic;
using UnityEngine;

public class BrokenAsset : MonoBehaviour
{
    [SerializeField] List<Rigidbody> _assetParts;
    public List<Rigidbody> AssetParts => _assetParts;
}
