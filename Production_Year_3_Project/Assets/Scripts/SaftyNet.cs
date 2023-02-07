using UnityEngine;

public class SaftyNet : MonoBehaviour
{
    [SerializeField] GameObject _saftySpawnPoint;
    [SerializeField] LayerMask _playerLayer;
    int _playerLayerValue;
    private void Start()
    {
        float rawLayerValue = _playerLayer.value;
        rawLayerValue = Mathf.Log(rawLayerValue, 2);
        _playerLayerValue = (int)rawLayerValue;
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.LogError("Player safty net is activated");
        if (other.gameObject.layer == _playerLayerValue)
        {
            Debug.LogError("Reseting Player Position");
            other.transform.position = _saftySpawnPoint.transform.position;
        }
    }
}
