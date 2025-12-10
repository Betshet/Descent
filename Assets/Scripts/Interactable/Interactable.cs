using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public enum InteractableType {
    Dialog,
    Movement,
    Other
}

public enum InteractableEffect {
    None,
    HandInHole,
    QuaiPeople
}

public class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    
    [SerializeField] private InteractableType interactableType;
    [SerializeField] private MovementDirection movementDirection;
    [SerializeField] private InteractableEffect interactableEffect;
    [SerializeField] private GameObject highlight;
    [SerializeField] private IteractibleData interactibleData;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Canvas canvas;
    
    private bool _isHovered = false;
    private bool _isInteractable = true;
    private List<GameObject> _buttons = new();
    
    void Start() {
        if(highlight) highlight.SetActive(false);
    }
    
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            
            if (_isInteractable) {
                
                switch (interactableType) {
                    case InteractableType.Dialog: 
                        if (_isHovered && _buttons.Count == 0) {
                            OpenMenu();
                        }
                        else if (_buttons.Count > 0 && !_isHovered) {
                            CloseMenu();
                        }
                        
                        // if effect after dialogue do here
                        
                        break;
                    case InteractableType.Movement:
                        if (_isHovered) {
                            if (interactableEffect != InteractableEffect.None) {
                                ImageMovement.Instance.ApplyEffect(interactableEffect);
                                Debug.Log("do stuff pls");
                            }
                            switch (movementDirection) {
                                case MovementDirection.Left:
                                    ImageMovement.Instance.Turn(MovementDirection.Left);
                                    break;
                                case MovementDirection.Right:
                                    ImageMovement.Instance.Turn(MovementDirection.Right);
                                    break;
                                case MovementDirection.Forward:
                                    ImageMovement.Instance.TransitionLocation();
                                    break;
                            }

                            
                        }
                        break;
                    case InteractableType.Other:
                        if (_isHovered && interactableEffect != InteractableEffect.None) {
                            ImageMovement.Instance.ApplyEffect(interactableEffect);
                        }
                        break;
                }
            }
            
        }

    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (_isInteractable) {
            if(highlight) highlight.SetActive(true);
            _isHovered = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (_isHovered) {
            if(highlight) highlight.SetActive(false);
            _isHovered = false;
        }
    }

    private void OpenMenu() {
        
        for (int i = 0; i < interactibleData.dialogOptions.Length; i++) {

            // create button
            var button = Instantiate(buttonPrefab, transform);
            Button buttonScript = button.GetComponent<Button>();
            String text = interactibleData.dialogOptions[i].dialogText;

            // add button effect linked to dialog
            DialogOption dialogOption = interactibleData.dialogOptions[i];
            buttonScript.onClick.AddListener(() => UseButton(text, dialogOption));

            // set button text
            TMP_Text textScript = button.GetComponentInChildren<TMP_Text>();
            textScript.text = interactibleData.dialogOptions[i].action;

            // set button position à la rache pas ouf à refaire
            button.transform.Translate(transform.position);
            button.transform.Translate(new Vector3(0, i * 50f, 0));
            _buttons.Add(button);
        }
    }

    private void UseButton(String text, DialogOption doption) {
        Debug.Log(text);
        CloseMenu();
        DialogManager.Instance.SetDialog(doption);
    }

    private void CloseMenu() {
        foreach (var button in _buttons) {
            Destroy(button);
        }
        _buttons.Clear();
    }
}
