using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    
    private bool teleported = false;
    private bool triggerHasBeenLeft = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit");
        switch (other.gameObject.tag)
        {
            case "OrangeToBlue":
                {
                    if(teleported == false)
                    {
                        transform.position = GameObject.Find("BluePortal(Clone)").transform.position;
                        teleported = true;
                    }
                    break;
                }
            case "BlueToOrange":
                {
                    if(teleported == false)
                    {
                        transform.position = GameObject.Find("OrangePortal(Clone)").transform.position;
                        teleported = true;
                    }
                    break;
                }
        }

    }

    IEnumerator triggerLeft()
    {
        if (triggerHasBeenLeft == true)
        {

            yield return new WaitForSeconds(1);
            teleported = false;
            triggerHasBeenLeft = false;
        }    

    }

    void OnTriggerExit2D()
    {
        triggerHasBeenLeft = true;
        StartCoroutine(triggerLeft());
    }

}

