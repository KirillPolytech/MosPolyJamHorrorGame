using UnityEngine;

public class Oleg : MonoBehaviour
{
    private GameObject __player;
    private void Awake()
    {
        __player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        //transform.LookAt(__player.transform.position);

        //transform.rotation = Quaternion.LookRotation(- (__player.transform.position - transform.position));
    }
}
