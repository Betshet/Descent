using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public enum DialogType
    {
        Portrait,
        NoPortrait,
        Choice
    }

    [SerializeField]
    GameObject DialogBox_Portrait;

    [SerializeField]
    GameObject DialogBox_NoPortrait;

    [SerializeField]
    GameObject DialogBoxText_Portrait;

    [SerializeField]
    GameObject DialogBoxText_NoPortrait;

    [SerializeField]
    GameObject DialogBox_Choice;

    public static DialogManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayDialogBox(DialogType dtype)
    {
        switch(dtype)
        {
            case DialogType.Portrait:
                DialogBox_Portrait.SetActive(true);
                break;
            case DialogType.NoPortrait:
                DialogBox_NoPortrait.SetActive(true);
                break;
            case DialogType.Choice:
                DialogBox_Choice.SetActive(true);
                break;
        }
    }

    public void HideDialogBox()
    {
        DialogBox_Portrait.SetActive(false);
        DialogBox_NoPortrait.SetActive(false);
        DialogBox_Choice.SetActive(false);
    }

    public void SetDialogBoxText(string TextToDisplay, bool portrait = false)
    {
        GameObject dboxtext;

        if (portrait) dboxtext = DialogBoxText_Portrait;
        else dboxtext = DialogBoxText_NoPortrait;

        dboxtext.GetComponent<TextMeshProUGUI>().text = TextToDisplay;
    }

    public void SetDialog(Dialog dialog)
    {

    }
}
