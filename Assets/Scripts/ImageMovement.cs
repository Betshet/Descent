using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public enum FMVState {
    Idle,
    Transitioning,
}

public enum MovementDirection {
    Left,
    Right,
    Forward,
    Backward,
}

public class ImageMovement : MonoBehaviour
{
    public static ImageMovement Instance { get; private set; }

    private void Awake() 
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    
    [SerializeField] private Image imageDisplay;
    [SerializeField] private VideoPlayer videoDisplay0;
    [SerializeField] private Scene startScene;
    [SerializeField] private RawImage rawImage;
    [SerializeField] private VideoClip startClip;
    [SerializeField] private Image transitionImage;
    [SerializeField] private Material imageMaterial;
    
    [SerializeField] private MapLocation locationAfterHole;
    [SerializeField] private VideoClip handVideo;

    private Scene _currentScene;
    private MapLocation _currentLocation;
    private Direction _currentDirection = Direction.N;
    private FMVState _currentState = FMVState.Idle;
    private VideoPlayer _currentVideoPlayer;
    private GameObject _currentCanvas;

    private int _footstepType = 0;
    
    public RenderTexture persistentRT;
    
    void Start() {
        _currentScene = startScene;
        _currentDirection = _currentScene.StartDirection;
        _currentLocation = _currentScene.StartLocation;
        if(_currentScene.Music) SoundManager.Instance.PlayMusic(_currentScene.Music);
        
        _currentVideoPlayer  = videoDisplay0;
        videoDisplay0.isLooping = false;
        videoDisplay0.playbackSpeed = 2f;
        videoDisplay0.renderMode = VideoRenderMode.RenderTexture;
        
        persistentRT = new RenderTexture(1920, 1080, 0, RenderTextureFormat.ARGB32);
        persistentRT.Create();
        
        rawImage.texture = persistentRT;
        
        videoDisplay0.targetTexture = persistentRT;
        videoDisplay0.sendFrameReadyEvents = false;
        videoDisplay0.skipOnDrop = false;
        
        _currentCanvas = Instantiate(_currentLocation.ViewCanvases[(int)_currentDirection]);

        if (startClip) StartCoroutine(PlayClip(startClip));
        
        transitionImage.color = Color.black;
        transitionImage.CrossFadeAlpha(0, 0.5f, false);
        
        imageMaterial.SetTexture("_texture_A", persistentRT);
        imageMaterial.SetTexture("_texture_B", persistentRT);
        SetShaderValues();
    }
    
    void Update()
    {
        if (_currentState == FMVState.Idle) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                //TransitionLocation();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                //Turn(MovementDirection.Right);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                //Turn(MovementDirection.Left);
            }
        }
    }

    public void TransitionLocation() {
        if (_currentLocation.Links[(int)_currentDirection] != null) {
            _currentState  = FMVState.Transitioning;
            Destroy(_currentCanvas);
            StartCoroutine(PlayClip(_currentLocation.Transitions[(int)_currentDirection]));
            SoundManager.Instance.PlayFootsteps(_footstepType);
            _currentVideoPlayer.loopPointReached += OnTransitionVideoFinished;
        }
    }
    
    private void OnTransitionVideoFinished(VideoPlayer source) {
        _currentLocation = _currentLocation.Links[(int)_currentDirection];
        _currentDirection = FindClosestDirection(_currentDirection, _currentLocation);
        _currentState  = FMVState.Idle;
        if (_currentLocation.ViewCanvases[(int)_currentDirection]) {
            _currentCanvas = Instantiate(_currentLocation.ViewCanvases[(int)_currentDirection]);
            var canvasQuai = _currentCanvas.GetComponent<CanvasQuai>();
            if (canvasQuai != null) {
                canvasQuai.SetWave(_currentLocation.currentQuaiWave);
            }
        }
        SoundManager.Instance.StopSound();
        _currentVideoPlayer.loopPointReached -= OnTransitionVideoFinished;
    }

    public void Turn(MovementDirection direction) {

        _currentState  = FMVState.Transitioning;
        Destroy(_currentCanvas);
        
        Direction newDir = FindNextDirection(direction);
        
        switch (direction) {
            case MovementDirection.Right:
                StartCoroutine(PlayClip(_currentLocation.ClockwiseTurns[(int)_currentDirection]));
                _currentVideoPlayer.loopPointReached += OnTurnVideoFinished;
                _currentDirection = newDir;
                break;
            case MovementDirection.Left:
                StartCoroutine(PlayClip(_currentLocation.AnticlockwiseTurns[(int)_currentDirection]));
                _currentVideoPlayer.loopPointReached += OnTurnVideoFinished;
                _currentDirection = newDir;
                break;
        }
        
    }
    
    private void OnTurnVideoFinished(VideoPlayer source) {
        _currentState  = FMVState.Idle;
        if (_currentLocation.ViewCanvases[(int)_currentDirection]) {
            _currentCanvas = Instantiate(_currentLocation.ViewCanvases[(int)_currentDirection]);
            var canvasQuai = _currentCanvas.GetComponent<CanvasQuai>();
            if (canvasQuai != null) {
                canvasQuai.SetWave(_currentLocation.currentQuaiWave);
            }
        }
        videoDisplay0.loopPointReached -= OnTurnVideoFinished;
    }

    private IEnumerator PlayClip(VideoClip clip) {
        _currentVideoPlayer.Pause();
        _currentVideoPlayer.clip = clip;
        _currentVideoPlayer.Prepare();
        yield return new WaitUntil(() => _currentVideoPlayer.isPrepared);
        _currentVideoPlayer.Play();
    }

    private Direction FindClosestDirection(Direction direction, MapLocation location) {
        Direction closestDirection = direction;
        for (int i = 0; i < 5; i++) {
            Direction rightDirection = direction + i;
            if (rightDirection > Direction.NW) {
                rightDirection = Direction.N;
            }
            if (location.LookableDirections[(int)rightDirection]) {
                closestDirection = rightDirection;
                break;
            }
            Direction leftDirection = direction - i;
            if (leftDirection < Direction.N) {
                leftDirection = Direction.NW;
            }
            if (location.LookableDirections[(int)leftDirection]) {
                closestDirection = leftDirection;
                break;
            }
        }
        return closestDirection;
    }

    private Direction FindNextDirection(MovementDirection movement) {
        Direction newDir = _currentDirection;
        switch (movement) {
            case MovementDirection.Right:
                newDir++;
                if (newDir > Direction.NW) {
                    newDir = Direction.N;
                }
                while (!_currentLocation.LookableDirections[(int)newDir]) {
                    newDir++;
                    if (newDir > Direction.NW) {
                        newDir = Direction.N;
                    }
                }
                break;
            case MovementDirection.Left:
                newDir--;
                if (newDir < Direction.N) {
                    newDir = Direction.NW;
                }
                while (!_currentLocation.LookableDirections[(int)newDir]) {
                    newDir--;
                    if (newDir < Direction.N) {
                        newDir = Direction.NW;
                    }
                }
                break;
        }
        return newDir;
    }

    public void ApplyEffect(InteractableEffect effect) {
        switch (effect) {
            case InteractableEffect.HandInHole:
                _currentState  = FMVState.Transitioning;
                Destroy(_currentCanvas);
                StartCoroutine(PlayClip(handVideo));
                _currentVideoPlayer.loopPointReached += OnHandInHoleFinish;
                break;
            case InteractableEffect.QuaiPeople:
                if (_currentLocation.LocationEffect == LocationEffect.Quai) {
                    var canvasQuai = _currentCanvas.GetComponent<CanvasQuai>();
                    if (canvasQuai != null) {
                        Debug.Log("Quai effect");
                        _currentLocation.currentQuaiWave++;
                    }
                }
                break;
            case InteractableEffect.NextScene:
                // do transition logic

                float transitionTime = 1f;
                Debug.Log("Start transition");
                
                transitionImage.CrossFadeAlpha(1, transitionTime, false);
                StartCoroutine(LoadNextScene(transitionTime));
                
                break;
        }
    }

    private void OnHandInHoleFinish(VideoPlayer source) {
        _currentLocation = locationAfterHole;
        _currentState  = FMVState.Idle;
        if (_currentLocation.ViewCanvases[(int)_currentDirection] != null) {
            _currentCanvas = Instantiate(_currentLocation.ViewCanvases[(int)_currentDirection]);
        }
        _currentVideoPlayer.loopPointReached -= OnHandInHoleFinish;
    }

    private IEnumerator LoadNextScene(float transitionTime)
    {
        yield return new WaitForSeconds(transitionTime + 0.5f);
        _currentScene = _currentScene.NextScene;
        _currentDirection = _currentScene.StartDirection;
        _currentLocation = _currentScene.StartLocation;
        Destroy(_currentCanvas);
        SetShaderValues();
                
        if (_currentLocation.ViewCanvases[(int)_currentDirection] != null) {
            _currentCanvas = Instantiate(_currentLocation.ViewCanvases[(int)_currentDirection]);
        }
                
        if(_currentScene.Music) SoundManager.Instance.PlayMusic(_currentScene.Music);
        transitionImage.CrossFadeAlpha(0, transitionTime, false);
        Debug.Log("End transition");
    }

    private void SetShaderValues()
    {
        imageMaterial.SetColor("_ColorA", _currentScene.hueA);
        imageMaterial.SetColor("_ColorB", _currentScene.hueB);
        imageMaterial.SetFloat("_Offset", _currentScene.offset);
        imageMaterial.SetFloat("_Light_line_speed", _currentScene.lightLineSpeed);
        imageMaterial.SetFloat("_Saturation", _currentScene.Saturation);
        imageMaterial.SetFloat("_Hue", _currentScene.hue);
        imageMaterial.SetFloat("_Contrast", _currentScene.Contrast);
    }
}
