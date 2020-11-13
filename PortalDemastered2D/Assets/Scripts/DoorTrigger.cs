using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private int wilam = 14;
    public GameObject door;
    

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Pressure Switch")
        {
            door.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name == "Pressure Switch")
        {
            door.SetActive(true);
        }
    }

}
