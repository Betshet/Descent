using UnityEngine;

public enum InteractibleObject
{
    None,
    Chair
}

public enum Portrait
{
    None,
    Char1
}


[CreateAssetMenu(fileName = "Dialog", menuName = "Scriptable Objects/Dialog")]
public class Dialog : ScriptableObject
{
    public InteractibleObject objectGroup;
    public string actionText;
    public string dialogText;
    public Portrait portrait;
}
