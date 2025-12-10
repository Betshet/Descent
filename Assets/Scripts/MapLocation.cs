using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public enum Direction {
    N,
    NE,
    E,
    SE,
    S,
    SW,
    W,
    NW,
}

public enum DisplayType {
    Image,
    Video,
    Gif
}

public enum LocationEffect {
    None,
    Quai,
    Eyes
}

[CreateAssetMenu(fileName = "Location", menuName = "ScriptableObjects/MapLocation", order = 1)]
public class MapLocation : ScriptableObject
{
    [SerializeField] private LocationEffect locationEffect;
    [SerializeField] private MapLocation[] links = new MapLocation[8];
    [SerializeField] private VideoClip[] clockwiseTurns = new VideoClip[8];
    [SerializeField] private VideoClip[] anticlockwiseTurns = new VideoClip[8];
    [SerializeField] private VideoClip[] transitions = new VideoClip[8];
    [SerializeField] private GameObject[] viewCanvases = new GameObject[8];
    [SerializeField] private bool[] lookableDirections = new bool[8]{true, false, true, false, true, false, true, false};
    
    public LocationEffect LocationEffect => locationEffect;
    public MapLocation[] Links => links;
    public VideoClip[] ClockwiseTurns => clockwiseTurns;
    public VideoClip[] AnticlockwiseTurns => anticlockwiseTurns;
    public VideoClip[] Transitions => transitions;
    public GameObject[] ViewCanvases => viewCanvases;
    public bool[] LookableDirections => lookableDirections;
    
    public int currentQuaiWave;

    void OnEnable() {
        currentQuaiWave = 0;
    }
}
