using UnityEngine;

public enum Portrait
{
    None,
    Char1,
    Pain
}

[CreateAssetMenu(fileName = "DialogOption", menuName = "ScriptableObjects/DialogOption")]
public class DialogOption : ScriptableObject
{
    public string action;
    public string dialogText;
    public CharacterDialogOption[] dialogChoices;
    public Sprite portrait;
}
