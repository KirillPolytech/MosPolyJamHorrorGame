using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UnderWater : MonoBehaviour
{
    private PostProcessVolume Volume;
    void Start()
    {
        Volume = GetComponent<PostProcessVolume>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Volume.priority = 5;
    }
    private void OnTriggerExit(Collider other)
    {
        Volume.priority = -1;
    }
}
