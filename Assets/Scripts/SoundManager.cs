using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioClip footstepSound;
    [SerializeField] private AudioClip bitCrushFootstepSound;
    [SerializeField] private AudioClip clickSound;
    
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource dialogueSource;
    [SerializeField] private AudioSource musicSource;

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

    public void PlaySound(AudioClip clip) {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayDialogue(AudioClip clip) {
        dialogueSource.PlayOneShot(clip);
    }

    public void PlayClickSound() {
        sfxSource.PlayOneShot(clickSound);
    }

    public void PlayFootsteps(int i) {
        sfxSource.pitch = 1.5f;
        switch (i) {
            case 0:
                Debug.Log("PlayFootsteps: " + i);
                sfxSource.PlayOneShot(footstepSound);
                break;
            case 1:
                sfxSource.PlayOneShot(bitCrushFootstepSound);
                break;
        }
    }

    public void StopSound() {
        Debug.Log("StopSound");
        if(sfxSource.isPlaying) sfxSource.Stop();
        sfxSource.pitch = 1f;
    }
    
}
