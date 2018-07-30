using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        if (player != null)
            offset = transform.position - player.transform.position;
    }


    void LateUpdate()
    {
        if(player != null)
        transform.position = player.transform.position + offset;
    }
}