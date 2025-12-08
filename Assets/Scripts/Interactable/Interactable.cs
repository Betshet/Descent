using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    
    [SerializeField] private GameObject highlight;
    [SerializeField] private ScriptableObject interactableData;
    
    private bool _isHovered = false;
    private bool _isInteractable = true;
    private CanvasGroup _buttonGroup;
    private List<Button> _buttons = new();
    
    void Start() {
        highlight.SetActive(false);
    }
    
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (_isHovered && _isInteractable && !_buttonGroup) {
                OpenMenu();
            }
            if (_buttonGroup) {
                CloseMenu();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (_isInteractable) {
            highlight.SetActive(true);
            _isHovered = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (_isHovered) {
            highlight.SetActive(false);
            _isHovered = false;
        }
    }

    private void OpenMenu() {
        _buttonGroup = Instantiate(new CanvasGroup(), transform);
        
    }

    private void CloseMenu() {
        Destroy(_buttonGroup.gameObject);
    }
}
