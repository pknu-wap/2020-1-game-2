using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour
{    
    

    void Start()
    {
        LocatePlayer();                
    }

    void LocatePlayer()
    {
        int columns = PlayerPrefs.GetInt("columns");
        float size = PlayerPrefs.GetFloat("size");
        int xrand = (int)Random.Range(0, (columns * size) / 2);
        int zrand = (int)Random.Range(0, (columns * size) / 2);
        int xquotient = (int)(xrand / size);
        int zquotient = (int)(zrand / size);        
        float xlocation = xquotient * size;
        float zlocation = zquotient * size;
        transform.position = new Vector3(xlocation, 0.5f, zlocation);
    }
}
