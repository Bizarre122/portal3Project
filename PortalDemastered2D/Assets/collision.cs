using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit");
        switch (other.gameObject.tag)
        {
            case "OrangeToBlue":
                {
                    transform.position = other.transform.GetChild(0).position;
                    break;
                }
            case "BlueToOrange":
                {
                    transform.position = other.transform.GetChild(0).position;
                    break;
                }
        }

    }
}

