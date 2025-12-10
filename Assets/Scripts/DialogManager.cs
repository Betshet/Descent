using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

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
    GameObject DialogBox_PortraitImage;

    [SerializeField]
    GameObject DialogBoxText_NoPortrait;

    [SerializeField]
    GameObject DialogBox_Choice;

    [SerializeField]
    GameObject[] ChoiceButtons;

    [SerializeField]
    GameObject[] ChoiceButtonsText;

    DialogOption currentCharDialog;
    CharacterDialogOption currentCharOption;
    int currentDialogLine = 0;
    bool inCharacterDialog = false;

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
        HideDialogBox();
        DisplayDialogBox(portrait ? DialogType.Portrait : DialogType.NoPortrait);

        GameObject dboxtext = portrait ? DialogBoxText_Portrait : DialogBoxText_NoPortrait;

        dboxtext.GetComponent<TextMeshProUGUI>().SetText(TextToDisplay);
    }

    public void SetDialog(DialogOption dialog)
    {
        if(dialog.dialogChoices.Length != 0)
        {
            SetupChoice(dialog);
        }
        else if(dialog.portrait)
        {
            SetDialogBoxText(dialog.dialogText, true);
        }
        else
        {
            SetDialogBoxText(dialog.dialogText, false);
        }
    }

    public void SetupChoice(DialogOption talkMenu)
    {
        currentCharDialog = talkMenu;

        HideDialogBox();
        DisplayDialogBox(DialogType.Choice);

        foreach (var button in ChoiceButtons)
        {
            button.SetActive(false);
        }

        for(int i = 0 ; i < talkMenu.dialogChoices.Length ; i++)
        {
            ChoiceButtons[i].SetActive(true);
            ChoiceButtonsText[i].GetComponent<TextMeshProUGUI>().text = talkMenu.dialogChoices[i].dialogOptionText;
        }
    }

    public void OnClick_SelectChoice1()
    {
        currentCharOption = currentCharDialog.dialogChoices[0];
        SetupNextLine();
    }

    public void OnClick_SelectChoice2()
    {
        currentCharOption = currentCharDialog.dialogChoices[1];
        SetupNextLine();
    }

    public void OnClick_SelectChoice3()
    {
        currentCharOption = currentCharDialog.dialogChoices[2];
        SetupNextLine();
    }

    public void OnClick_SelectChoice4()
    {
        currentCharOption = currentCharDialog.dialogChoices[3];
        SetupNextLine();
    }

    void SetupNextLine()
    {
        currentDialogLine = 0;
        inCharacterDialog = true;
        HideDialogBox();
        NextLine();
    }

    void NextLine()
    {
        if(currentDialogLine >= currentCharOption.dialogText.Length)
        {
            inCharacterDialog = false;
            HideDialogBox();
        }
        else
        {
            bool usePortait = currentCharOption.dialogPortraits[currentDialogLine] ? true : false;
            SetDialogBoxText(currentCharOption.dialogText[currentDialogLine], usePortait);


            if (usePortait)
            {
                DialogBox_PortraitImage.GetComponent<Image>().sprite = currentCharOption.dialogPortraits[currentDialogLine];
            }

            //SoundManager.Instance.PlayDialogue();

            currentDialogLine++;
        }
    }

    public void OnClick_TextBox()
    {
        if (inCharacterDialog)
        {
            NextLine();
        }
        else
        {
            HideDialogBox();
        }
    }
}
