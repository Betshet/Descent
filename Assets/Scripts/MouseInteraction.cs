using UnityEngine;

public enum CursorType {
    Normal,
    Interact,
    Left,
    Right,
    Forward
}

public class MouseInteraction : MonoBehaviour {
    
    public static MouseInteraction Instance { get; private set; }
    
    [SerializeField] private Texture2D[] cursorTextures;

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
    
    void Start()
    {
        Cursor.SetCursor(cursorTextures[0], Vector2.zero, CursorMode.Auto);
    }

    public void SetCursor(CursorType cursorType) {
        Cursor.SetCursor(cursorTextures[(int)cursorType], Vector2.zero, CursorMode.Auto);
    }
}
