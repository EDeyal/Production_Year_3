using Unity.VisualScripting;
using UnityEngine;
using ColorUtility = UnityEngine.ColorUtility;

public enum ProgressionType
{
    None = 0,
    Damage = 1,
    Health = 2
}
public class ProgressionPopUp : MonoBehaviour, ICheckValidation
{
    [SerializeField] GameObject _holder;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] TMPro.TextMeshProUGUI _text;
    [SerializeField] Color _damageColor;
    [SerializeField] Color _healthColor;
    [SerializeField] float _transitionMultiplier = 1;
    [SerializeField] float _showTime = 1;
    float _currentShowTime;
    bool _isActive = false;
    bool _transitionIn = true;
    private void Awake()
    {
        CheckValidation();
    }
    private void Start()
    {
        GameManager.Instance.UiManager.CacheProgressionPopUp(this);
    }
    public void CollectNewProgression(ProgressionType type)
    {
        string textString;
        switch (type)
        {
            case ProgressionType.None:
                break;
            case ProgressionType.Damage:
                textString = "Increased Blade<b><color=#";
                textString += ColorUtility.ToHtmlStringRGB(_damageColor);
                textString += "> Damage</b></color>";
                _text.text = textString;

                break;
            case ProgressionType.Health:
                textString = "Increased Max<b><color=#";
                textString += ColorUtility.ToHtmlStringRGB(_healthColor);
                textString += "> Health</b></color>";
                _text.text = textString;
                break;
            default:
                break;
        }
        TransitionIn();
    }
    public void Update()
    {
        if (_isActive)
        {
            if (_transitionIn)
            {
                if (_canvasGroup.alpha < 1)
                {
                _canvasGroup.alpha += Time.deltaTime * _transitionMultiplier;
                }
                else
                {
                    _currentShowTime -= Time.deltaTime;
                    if (_currentShowTime <= 0)
                    {
                        TransitionOut();
                    }
                }
            }
            else
            {
                if (_canvasGroup.alpha > 0)
                {
                    _canvasGroup.alpha -= Time.deltaTime * _transitionMultiplier;
                }
                else
                {
                    //Debug.Log("Progression Pop Up completed transition");
                    _holder.SetActive(false);
                    _isActive = false;
                    _transitionIn = true;
                }
            }
        }
    }
    public void TransitionIn()
    {
        //Debug.Log("Progression Pop Up Transitioning In");
        _isActive = true;
        _currentShowTime = _showTime;
        _canvasGroup.alpha = 0;
        _holder.SetActive(true);
    }
    public void TransitionOut()
    {
        //Debug.Log("Progression Pop Up Transitioning Out");
        _transitionIn = false;
        _canvasGroup.alpha = 1;
    }

    public void CheckValidation()
    {
        if (_holder == null)
            throw new System.Exception("ProgressionPopUp has no holder");
        if (_canvasGroup == null)
            throw new System.Exception("ProgressionPopUp has no canvas group");
        if (_text == null)
            throw new System.Exception("ProgressionPopUp has no text");
    }
}
