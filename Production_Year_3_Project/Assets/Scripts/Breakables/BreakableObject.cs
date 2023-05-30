using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : DamageableObject,IRespawnable
{
    [TabGroup("General")]
    [SerializeField] GameObject _asset;
    [TabGroup("General")]
    [SerializeField] GameObject _brokenAssetPrefab;
    [TabGroup("General")]
    [SerializeField] GameObject _damageableColliderGameObject;
    [SerializeField] bool _isRespawnable;

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
    [TabGroup("Visuals")]
    [SerializeField] List<ParticleSystem> _particles;



    public void ExplodeParts()
    {
        var explosionPosition = _explosionLocation.position;
        foreach (var part in _brokenAsset.AssetParts)
        {
            part.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
        }
    }
    private void ResetParticles()
    {
        if (_particles!= null)
        {
            foreach (var particle in _particles)
            {
                particle.Stop();
            }
        }
    }
    private void PlayParticles()
    {
        if (_particles != null)
        {
            foreach (var particle in _particles)
            {
                particle.Play();
            }
        }
    }
    public void BreakAsset()
    {
        _asset.SetActive(false);
        _damageableColliderGameObject.SetActive(false);
        _instantiatedObject = Instantiate(_brokenAssetPrefab, transform);
        _brokenAsset = _instantiatedObject.GetComponent<BrokenAsset>();
        ExplodeParts();
        PlayParticles();
        Destroy(_instantiatedObject, _brokenAssetlifetime);
    }
    public override void Awake()
    {
        base.Awake();
        Damageable.OnDeath.AddListener(BreakAsset);//when player is dead it probably removes the asset from the list
    }
    private void Start()
    {
        if (_isRespawnable)
        {
            GameManager.Instance.SaveManager.SavePointHandler.RespawnAssets.Add(this);
        }
    }
    private void OnEnable()
    {
        Respawn();
    }
    private void OnDestroy()
    {
        //if (_isRespawnable)
        //{
        //    GameManager.Instance.SaveManager.SavePointHandler.RespawnAssets.Remove(this);
        //}
    }
    public void Respawn()
    {
        _asset.SetActive(true);
        _damageableColliderGameObject.SetActive(true);
        Damageable.Heal(new DamageHandler() { BaseAmount = Damageable.MaxHp });
        ResetParticles();
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ResetAsset();
        }
    }
    [Button("Test")]
    public void TestAsset()
    {
        BreakAsset();
    }
    [Button("Reset")]
    public void ResetAsset()
    {
        Respawn();
    }
#endif
}
