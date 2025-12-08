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

[CreateAssetMenu(fileName = "Location", menuName = "ScriptableObjects/MapLocation", order = 1)]
public class MapLocation : ScriptableObject
{
    private MapLocation[] links = new MapLocation[8];
    private VideoClip[] clockwiseTurns = new VideoClip[8];
    private VideoClip[] anticlockwiseTurns = new VideoClip[8];
    private VideoClip[] transitions = new VideoClip[8];
    private GameObject[] viewCanvases = new GameObject[8];
    private bool[] lookableDirections = new bool[8]{true, false, true, false, true, false, true, false};
    
    public MapLocation[] Links => links;
    public VideoClip[] ClockwiseTurns => clockwiseTurns;
    public VideoClip[] AnticlockwiseTurns => anticlockwiseTurns;
    public VideoClip[] Transitions => transitions;
    public GameObject[] ViewCanvases => viewCanvases;
    public bool[] LookableDirections => lookableDirections;
}
