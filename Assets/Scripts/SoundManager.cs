using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioClip footstepSound;
    
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

    public void PlayFootsteps() {
        sfxSource.PlayOneShot(footstepSound);
    }

    public void StopSound() {
        if(sfxSource.isPlaying) sfxSource.Stop();
    }
    
}
