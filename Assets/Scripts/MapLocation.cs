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
    public MapLocation[] links = new MapLocation[8];
    public VideoClip[] clockwiseTurns = new VideoClip[8];
    public VideoClip[] anticlockwiseTurns = new VideoClip[8];
    public VideoClip[] transitions = new VideoClip[8];
    public bool[] lookableDirections = new bool[8]{true, false, true, false, true, false, true, false};
    
    public MapLocation[] Links => links;
    public VideoClip[] ClockwiseTurns => clockwiseTurns;
    public VideoClip[] AnticlockwiseTurns => anticlockwiseTurns;
    public VideoClip[] Transitions => transitions;
    public bool[] LookableDirections => lookableDirections;
}
