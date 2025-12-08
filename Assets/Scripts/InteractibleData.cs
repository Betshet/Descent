using UnityEngine;

public enum InteractibleObject
{
    None,
    Chair
}


[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/Dialog")]
public class Dialog : ScriptableObject
{
    public InteractibleObject iobject;
    public DialogOption[] dialogOptions;
    public bool character = false;
}
