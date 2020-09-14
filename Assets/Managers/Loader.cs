using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject managers;
    public GameObject camera;

    public void Start()
    {
        if (GameObject.Find(managers.name) == null){
            Instantiate(managers);
        }
        if (Camera.main == null)
        {
            Instantiate(camera);
        }
    }
}
