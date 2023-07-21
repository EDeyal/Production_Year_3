using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenuCinematicHandler : MonoBehaviour
{
    static bool _wasActivate = false;
    bool _isPlaying;
    [SerializeField] VideoPlayer _cinematicVideoPlayer;
    [SerializeField] GameObject _cinematicGameObject;
    [SerializeField] float _skipTime;
    [SerializeField] Image _skipImage;
    [SerializeField] CanvasGroup _skipOptionCanvasGroup;
    float _skipCounter = 0;
    float _showSkipOptionCounter = 0;
    private void OnEnable()
    {
        _cinematicVideoPlayer.loopPointReached += EndCinematic;
    }
    private void OnDisable()
    {
        _cinematicVideoPlayer.loopPointReached -= EndCinematic;
    }
    private void Start()
    {
        if (_wasActivate)
            return;
        //Debug.Log("Main menu cinematic handler playing on start");
        PlayCimenatic();
    }
    private void Update()
    {
        if (_isPlaying)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _skipOptionCanvasGroup.alpha = 1;
                _skipCounter += Time.deltaTime;
                if (_skipCounter >= _skipTime)
                {
                    SkipCinematic();
                    //Debug.Log("Main menu cinematic handler skips cinematic");
                }
            }
            else
            {
                if(_skipCounter >0)
                    _skipCounter-= Time.deltaTime;
                else
                {
                    _skipOptionCanvasGroup.alpha = 0;
                }
            }
                _skipImage.fillAmount = _skipCounter / _skipTime;
        }
    }
    public void PlayCimenatic()
    {

        //Debug.Log("Main menu cinematic handler playing cinematic");
        _isPlaying = true;
        _cinematicGameObject.SetActive(true);
        _cinematicVideoPlayer.Play();
        _skipCounter = 0;
        _skipImage.gameObject.SetActive(true);
        _skipOptionCanvasGroup.alpha = 0;
        GameManager.Instance.InputManager.SetCurserVisability(false);
    }
    public void SkipCinematic()
    {
        EndCinematic(_cinematicVideoPlayer);
    }
    private void EndCinematic(VideoPlayer videoPlayer)
    {
        _isPlaying = false;
        _cinematicGameObject.SetActive(false);
        videoPlayer.Stop();
        _wasActivate = true;
        _skipImage.gameObject.SetActive(false);
        GameManager.Instance.InputManager.SetCurserVisability(true);
    }
}
