using UnityEngine;

[CreateAssetMenu(fileName = "DialogOption", menuName = "Scriptable Objects/DialogOption")]
public class DialogOption : ScriptableObject
{
    public enum Portrait
    {
        None,
        Char1
    }

    [CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/Dialog")]
    public class Dialog : ScriptableObject
    {
        public string action;
        public string dialogText;
        public string[] dialogChoices;
        public Portrait portrait;
    }
}
