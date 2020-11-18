using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAimWeapon : MonoBehaviour
{

    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }


    public GameObject OrangeToBlueWaypoint;
    public GameObject BlueToOrangeWaypoint;
    public GameObject OrangePortal;
    public GameObject BluePortal;
    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    private Transform aimShellPositionTransform;
    private Animator aimAnimator;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        aimAnimator = aimTransform.GetComponent<Animator>();
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
        aimShellPositionTransform = aimTransform.Find("ShellPosition");
    }

    private void Update()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 aimDirection = (mousePosition - transform.position).normalized;

        HandleAiming();
        HandleShooting();
        /*Vector3 forward = aimDirection;
        Debug.DrawRay(aimTransform.position, forward * 10.0f, Color.yellow);
        int layerMask = ~(LayerMask.GetMask("Unportalable Surfaces"));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, forward, Mathf.Infinity, layerMask);

        if (hit.collider != null)
        {
            Debug.Log("Did hit");
        */

    }

    private void HandleAiming()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        //Debug.DrawRay(aimGunEndPointTransform.position, aimDirection * 10, Color.yellow);
    }
        

    private void HandleShooting()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Quaternion myAngle = new Quaternion(0, 0, angle, 0);
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Linecast(aimGunEndPointTransform.position, mousePosition);

            if(hit.collider != null)
            {
                Vector2 portalPosition = hit.point;
                Debug.Log("Hit");
                Debug.Log(hit.fraction);
                Debug.DrawRay(aimGunEndPointTransform.position, aimDirection * hit.distance, Color.yellow);
                if (GameObject.Find("BluePortal(Clone)") != null)
                {
                    GameObject clone = GameObject.Find("BluePortal(Clone)");
                    GameObject waypointClone = GameObject.Find("OrangeToBlueWaypoint(Clone)");
                    Destroy(clone.gameObject);
                    Destroy(waypointClone.gameObject);
                    GameObject BluePortalNew = Instantiate(BluePortal, portalPosition, myAngle);
                    GameObject OrangeToBlueWayPoint = Instantiate(OrangeToBlueWaypoint, portalPosition, myAngle);
                    OrangeToBlueWayPoint.transform.parent = BluePortalNew.transform;


                }
                else
                {
                    GameObject BluePortalNew = Instantiate(BluePortal, portalPosition, myAngle);
                    GameObject OrangeToBlueWayPoint = Instantiate(OrangeToBlueWaypoint, portalPosition, myAngle);
                    OrangeToBlueWayPoint.transform.parent = BluePortalNew.transform;
                }
            }

            


            aimAnimator.SetTrigger("Shoot");
            OnShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
            });
        }

        if (Input.GetMouseButtonDown(1))
        {

            RaycastHit2D hit = Physics2D.Linecast(aimGunEndPointTransform.position, mousePosition);

            if (hit.collider != null)
            {
                Debug.Log("Hit");
                Vector2 portalPosition = hit.point;
                Debug.Log("Hit");
                Debug.Log(hit.fraction);
                Debug.DrawRay(aimGunEndPointTransform.position, aimDirection * hit.distance, Color.yellow);
                if (GameObject.Find("OrangePortal(Clone)") != null)
                {
                    GameObject clone = GameObject.Find("OrangePortal(Clone)");
                    GameObject waypointClone = GameObject.Find("BlueToOrangeWaypoint(Clone)");
                    Destroy(clone.gameObject);
                    Destroy(waypointClone);
                    GameObject OrangePortalNew = Instantiate(OrangePortal, portalPosition, myAngle);
                    GameObject NewBlueToOrangeWaypoint = Instantiate(BlueToOrangeWaypoint, portalPosition, myAngle);
                    NewBlueToOrangeWaypoint.transform.parent = OrangePortalNew.transform;
                }
                else
                {
                    GameObject OrangePortalNew = Instantiate(OrangePortal, portalPosition, myAngle);
                    GameObject NewBlueToOrangeWaypoint = Instantiate(BlueToOrangeWaypoint, portalPosition, myAngle);
                    NewBlueToOrangeWaypoint.transform.parent = OrangePortalNew.transform;
                }
            }




            aimAnimator.SetTrigger("Shoot");
            OnShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
            });
        }
    }
}
