using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public enum FMVState {
    Idle,
    Transitioning,
}

public enum TurnDirection {
    Left,
    Right,
}

public class ImageMovement : MonoBehaviour
{
    [SerializeField] private Image imageDisplay;
    [SerializeField] private VideoPlayer videoDisplay0;
    [SerializeField] private MapLocation startLocation;
    [SerializeField] private RawImage rawImage;
    
    private MapLocation _currentLocation;
    private Direction _currentDirection = Direction.N;
    private FMVState _currentState = FMVState.Idle;
    private VideoPlayer _currentVideoPlayer;
    private bool _displayUsed = false;
    
    public RenderTexture persistentRT;
    
    void Start() {
        _currentLocation = startLocation;
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
        
    }
    
    void Update()
    {
        if (_currentState == FMVState.Idle) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                if (_currentLocation.links[(int)_currentDirection] != null) {
                    _currentState  = FMVState.Transitioning;
                    TransitionLocation();
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                _currentState  = FMVState.Transitioning;
                Turn(TurnDirection.Right);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                _currentState = FMVState.Transitioning;
                Turn(TurnDirection.Left);
            }
        }
    }

    private void TransitionLocation() {
        StartCoroutine(PlayClip(_currentLocation.Transitions[(int)_currentDirection]));
        _currentVideoPlayer.loopPointReached += OnTransitionVideoFinished;
    }
    
    private void OnTransitionVideoFinished(VideoPlayer source) {
        _currentLocation = _currentLocation.links[(int)_currentDirection];
        _currentState  = FMVState.Idle;
        _currentVideoPlayer.loopPointReached -= OnTransitionVideoFinished;
    }

    private void Turn(TurnDirection direction) {

        Direction newDir = FindNextDirection(direction);
        
        switch (direction) {
            case TurnDirection.Right:
                StartCoroutine(PlayClip(_currentLocation.ClockwiseTurns[(int)_currentDirection]));
                _currentVideoPlayer.loopPointReached += OnTurnVideoFinished;
                _currentDirection = newDir;
                break;
            case TurnDirection.Left:
                StartCoroutine(PlayClip(_currentLocation.AnticlockwiseTurns[(int)_currentDirection]));
                _currentVideoPlayer.loopPointReached += OnTurnVideoFinished;
                _currentDirection = newDir;
                break;
        }
        
    }
    
    private void OnTurnVideoFinished(VideoPlayer source) {
        _currentState  = FMVState.Idle;
        videoDisplay0.loopPointReached -= OnTurnVideoFinished;
    }

    private IEnumerator PlayClip(VideoClip clip) {
        _currentVideoPlayer.Pause();
        _currentVideoPlayer.clip = clip;
        _currentVideoPlayer.Prepare();   // PRELOAD NEW DECODED FRAME
        yield return new WaitUntil(() => _currentVideoPlayer.isPrepared);
        _currentVideoPlayer.Play();
    }

    private Direction FindNextDirection(TurnDirection turn) {
        Direction newDir = _currentDirection;
        switch (turn) {
            case TurnDirection.Right:
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
            case TurnDirection.Left:
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
    
}
