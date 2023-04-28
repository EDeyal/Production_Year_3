using Sirenix.OdinInspector;
using UnityEngine;

public class BreakableObject : DamageableObject
{
    [TabGroup("General")]
    [SerializeField] GameObject _asset;
    [TabGroup("General")]
    [SerializeField] GameObject _brokenAssetPrefab;
    [TabGroup("General")]
    [SerializeField] GameObject _damageableColliderGameObject;

    GameObject _instantiatedObject;
    BrokenAsset _brokenAsset;
    //[TabGroup("General")]
    //[SerializeField] bool _isOneTime;
    [TabGroup("Explosion")]
    [SerializeField] float _brokenAssetlifetime = 1;
    [TabGroup("Explosion")]
    [SerializeField] bool _exploadeRight;
    [TabGroup("Explosion")]
    [SerializeField] Transform _explosionLocation;
    [TabGroup("Explosion")]
    [SerializeField] float _explosionRadius;
    [TabGroup("Explosion")]
    [SerializeField] float _explosionForce;

    public void ExplodeParts()
    {
        var explosionPosition = _explosionLocation.position;
        foreach (var part in _brokenAsset.AssetParts)
        {
            part.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
        }
    }

    public void BreakAsset()
    {
        _asset.SetActive(false);
        _damageableColliderGameObject.SetActive(false);
        _instantiatedObject = Instantiate(_brokenAssetPrefab, transform);
        _brokenAsset = _instantiatedObject.GetComponent<BrokenAsset>();
        ExplodeParts();
        Destroy(_instantiatedObject, _brokenAssetlifetime);
    }
    public override void Awake()
    {
        base.Awake();
        Damageable.OnDeath.AddListener(BreakAsset);
    }

    private void OnEnable()
    {
        //if (_isOneTime)
        //{
        //    return;
        //}
        _asset.SetActive(true);
        _damageableColliderGameObject.SetActive(true);
    }

#if UNITY_EDITOR
    [Button("Test")]
    public void TestAsset()
    {
        BreakAsset();
    }
    [Button("Reset")]
    public void ResetAsset()
    {
        _asset.SetActive(true);
    }
#endif
}
