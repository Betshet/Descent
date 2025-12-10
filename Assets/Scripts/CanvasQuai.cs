using UnityEngine;
using UnityEngine.Video;

public class CanvasQuai : MonoBehaviour
{
    [SerializeField] private GameObject[] wave1;
    [SerializeField] private GameObject[] wave2;
    [SerializeField] private GameObject[] wave3;

    public void SetWave(int wave) {
        switch (wave) {
            case 0:
                // nothing
                break;
            case 1:
                foreach (var person in wave1) {
                    Destroy(person);
                }
                break;
            case 2:
                foreach (var person in wave1) {
                    Destroy(person);
                }
                foreach (var person in wave2) {
                    Destroy(person);
                }
                break;
            case 3:
                foreach (var person in wave1) {
                    Destroy(person);
                }
                foreach (var person in wave2) {
                    Destroy(person);
                }
                foreach (var person in wave3) {
                    Destroy(person);
                }
                break;
            default:
                foreach (var person in wave1) {
                    Destroy(person);
                }
                foreach (var person in wave2) {
                    Destroy(person);
                }
                foreach (var person in wave3) {
                    Destroy(person);
                }
                break;
        }
    }
}
