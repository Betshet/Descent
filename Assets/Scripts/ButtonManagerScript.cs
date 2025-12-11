using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerScript : MonoBehaviour
{


    void Start()
    {

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
