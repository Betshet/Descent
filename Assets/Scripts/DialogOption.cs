using UnityEngine;

public enum Portrait
{
    None,
    Char1
}

[CreateAssetMenu(fileName = "DialogOption", menuName = "Scriptable Objects/DialogOption")]
public class DialogOption : ScriptableObject
{
    public string action;
    public string dialogText;
    public string[] dialogChoices;
    public Portrait portrait;
}
