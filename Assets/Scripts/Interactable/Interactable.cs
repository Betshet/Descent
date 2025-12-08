using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    
    [SerializeField] private GameObject highlight;
    [SerializeField] private Dialog interactibleData;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Canvas canvas;
    
    private bool _isHovered = false;
    private bool _isInteractable = true;
    private List<GameObject> _buttons = new();
    
    void Start() {
        highlight.SetActive(false);
    }
    
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (_isHovered && _isInteractable && _buttons.Count == 0) {
                OpenMenu();
            }
            else if (_buttons.Count > 0 && !_isHovered) {
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
        for (int i = 0; i < interactibleData.dialogOptions.Length; i++) {
            var button = Instantiate(buttonPrefab, transform);
            Button buttonScript = button.GetComponent<Button>();
            String text = interactibleData.dialogOptions[i].dialogText;
            buttonScript.onClick.AddListener(() => UseButton(text));
            TMP_Text textScript = button.GetComponentInChildren<TMP_Text>();
            textScript.text = interactibleData.dialogOptions[i].action;
            button.transform.Translate(transform.position);
            button.transform.Translate(new Vector3(0, i * 50f, 0));
            _buttons.Add(button);
        }
    }

    private void UseButton(String text) {
        Debug.Log(text);
        CloseMenu();
    }

    private void CloseMenu() {
        foreach (var button in _buttons) {
            Destroy(button);
        }
        _buttons.Clear();
    }
}
