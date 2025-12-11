using UnityEngine;

[CreateAssetMenu(fileName = "Scene", menuName = "ScriptableObjects/Scene", order = 1)]
public class Scene : ScriptableObject
{
    [SerializeField] private MapLocation startLocation;
    [SerializeField] private Direction startDirection = Direction.N;
    [SerializeField] private Scene nextScene;
    [SerializeField] private AudioClip music;
    
    public MapLocation StartLocation => startLocation;
    public Direction StartDirection => startDirection;
    public Scene NextScene => nextScene;
    public AudioClip Music => music;

    public Color hueA = Color.orange;
    public Color hueB = Color.green;
    public float hue = 0;
    [Range(0.001f, 0.01f)] public float offset = 0.001f;
    [Range(0.1f, 2f)] public float lightLineSpeed = 0.4f;
    public float Saturation = 0.8f;
    public float Contrast = 1f;
}
