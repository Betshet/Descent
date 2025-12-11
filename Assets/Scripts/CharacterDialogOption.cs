using UnityEngine;


[CreateAssetMenu(fileName = "CharacterDialogOption", menuName = "ScriptableObjects/CharacterDialogOption")]
public class CharacterDialogOption : ScriptableObject
{
    public string dialogOptionText;
    public string[] dialogText;
    public Sprite[] dialogPortraits;
    public AudioClip[] dialogSounds;
}
