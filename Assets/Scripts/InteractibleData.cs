using UnityEngine;

public enum InteractibleObject
{
    None,
    Chair,
    Character_Pain,
    Character_Common
}


[CreateAssetMenu(fileName = "IteractibleData", menuName = "ScriptableObjects/IteractibleData")]
public class IteractibleData : ScriptableObject
{
    public InteractibleObject iobject;
    public DialogOption[] dialogOptions;
    public bool character = false;
}
