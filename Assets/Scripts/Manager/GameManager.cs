using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Easiest, laziest, and worst way to make a Singleton.
    public static GameManager instance;
    private float time1;
    private float time2;


    private void Awake()
    {
        instance = this;
        time1 = 0;


    }


    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        time1 = Time.timeSinceLevelLoad;
	}

    public float GetTime()
    {
        time2 = time1;
        return (time2 * 100) / 100;
    }
}
