using UnityEngine;

public class SpikesCheckpoint : MonoBehaviour
{
    [SerializeField] LayerMask _targetLayer;
    int _targetlayerValue;

    void Start()
    {
        float rawLayerValue = _targetLayer.value;
        rawLayerValue = Mathf.Log(rawLayerValue, 2);
        _targetlayerValue = (int)rawLayerValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _targetlayerValue)
        {
            GameManager.Instance.SaveManager.RoomsManager.CurrentCheckpoint = this.transform;
        }
    }
}
