using UnityEngine;

[CreateAssetMenu(fileName = "Scene", menuName = "ScriptableObjects/Scene", order = 1)]
public class Scene : ScriptableObject
{
    [SerializeField] private MapLocation startLocation;
    [SerializeField] private Direction startDirection = Direction.N;
    [SerializeField] private Scene nextScene;
    
    public MapLocation StartLocation => startLocation;
    public Direction StartDirection => startDirection;
    public Scene NextScene => nextScene;
}
