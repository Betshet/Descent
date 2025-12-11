using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerScript : MonoBehaviour
{


    void Start()
    {
        Screen.SetResolution(1280, 960, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick_Quit()
    {
        Application.Quit();
    }

    public void OnClick_Start()
    {
        SceneManager.LoadScene("ClovieScene");
    }
}
