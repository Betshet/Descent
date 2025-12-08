using UnityEngine;

public enum InteractibleObject
{
    None,
    Chair,
    Character_Comfort
}


[CreateAssetMenu(fileName = "IteractibleData", menuName = "ScriptableObjects/IteractibleData")]
public class IteractibleData : ScriptableObject
{
    public InteractibleObject iobject;
    public DialogOption[] dialogOptions;
    public bool character = false;
}
