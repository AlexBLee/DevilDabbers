using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private void Awake()
    {
        EventManager em = ServiceLocator.GetService<EventManager>();
        em.TriggerEvent(ConstManager.EVENT_PLAY_BGM);
    }
    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
