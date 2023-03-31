using Sirenix.OdinInspector;
using UnityEngine;

public class BreakableObject : DamageableObject
{
    [TabGroup("General")]
    [SerializeField] GameObject _asset;
    [TabGroup("General")]
    [SerializeField] GameObject _brokenAssetPrefab;
    GameObject _instantiatedObject;
    BrokenAsset _brokenAsset;
    [TabGroup("General")]
    [SerializeField] bool _isOneTime;
    [TabGroup("Explosion")]
    [SerializeField] float _brokenAssetlifetime = 1;
    [TabGroup("Explosion")]
    [SerializeField] bool _exploadeRight;
    [TabGroup("Explosion")]
    [SerializeField] Transform _rightExplosionLocation;
    [TabGroup("Explosion")]
    [SerializeField] Transform _leftExplosionLocation;
    [TabGroup("Explosion")]
    [SerializeField] float _explosionRadius;
    [TabGroup("Explosion")]
    [SerializeField] float _explosionForce;


    public void ExplodeParts()
    {
        var explosionDirection = Vector3.zero;
        if (_exploadeRight)
        {
            explosionDirection = _rightExplosionLocation.position;
        }
        else
        {
            explosionDirection = _leftExplosionLocation.position;
        }
        foreach (var part in _brokenAsset.AssetParts)
        {

            part.AddExplosionForce(_explosionForce, explosionDirection, _explosionRadius);
        }
    }

    public void BreakAsset()
    {
        _asset.SetActive(false);
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
        if (_isOneTime)
        {
            return;
        }
        _asset.SetActive(true);
    }
    private void OnDisable()
    {
        if (_isOneTime)
        {
            return;
        }
        //logic if needed
    }

#if UNITY_EDITOR
    [Button("Test")]
    public void TestAsset()
    {
        BreakAsset();
    }
#endif
}
