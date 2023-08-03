using UnityEngine;

public class WorldMap : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform minimapPoint_1;
    [SerializeField] private RectTransform minimapPoint_2;
    [SerializeField] private Transform worldPoint_1;
    [SerializeField] private Transform worldPoint_2;
    [SerializeField] private RectTransform savePointIcon;

    [Header("Player")]
    [SerializeField] private RectTransform playerMinimap;
    [SerializeField] private Transform playerWorld;


    [Header("UI")]
    [SerializeField] private GameObject mapView;

    private float minimapRatio;


    /* private void OnEnable()
     {
         Time.timeScale = 0f;
     }
     private void OnDisable()
     {
         Time.timeScale = 1f;
     }*/

    private void Awake()
    {
        CalculateMapRatio();
    }
    private void Start()
    {
        GameManager.Instance.UiManager.CacheWorldMap(this);
        playerWorld = GameManager.Instance.PlayerManager.transform;
        GameManager.Instance.InputManager.OnToggleMap.AddListener(ToggleMap);
        ToggleMap();
    }


    private void Update()
    {
        playerMinimap.anchoredPosition = minimapPoint_1.anchoredPosition + new Vector2((playerWorld.position.x - worldPoint_1.position.x) * minimapRatio, (playerWorld.position.y - worldPoint_1.position.y) * minimapRatio);
    }

    public void PlaceNewSavePointIcon(Vector3 worldPosition)
    {
        RectTransform newIcon = Instantiate(savePointIcon, transform.parent);
        newIcon.anchoredPosition = minimapPoint_1.anchoredPosition + new Vector2((worldPosition.x - worldPoint_1.position.x) * minimapRatio, (worldPosition.y - worldPoint_1.position.y) * minimapRatio);
    }



    public void ToggleMap()
    {
        mapView.SetActive(!mapView.activeInHierarchy);
    }

    public void CalculateMapRatio()
    {
        //distance world ignoring Y axis
        Vector3 distanceWorldVector = worldPoint_1.position - worldPoint_2.position;
        distanceWorldVector.z = 0f;
        float distanceWorld = distanceWorldVector.magnitude;

        //distance minimap (muffs)
        float distanceMinimap = Mathf.Sqrt(
            Mathf.Pow((minimapPoint_1.anchoredPosition.x - minimapPoint_2.anchoredPosition.x), 2) +
            Mathf.Pow((minimapPoint_1.anchoredPosition.y - minimapPoint_2.anchoredPosition.y), 2));


        minimapRatio = distanceMinimap / distanceWorld;
    }

}
