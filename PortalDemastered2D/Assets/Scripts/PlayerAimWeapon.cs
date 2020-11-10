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

        Debug.DrawRay(aimGunEndPointTransform.position, aimDirection * 10, Color.yellow);
        RaycastHit2D hit = Physics2D.Linecast(aimGunEndPointTransform.position, mousePosition);

        if(hit.collider != null)
        {
            Debug.Log("Hit");
        }
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 aimDirection = (mousePosition - transform.position).normalized;

            Vector3 forward = aimDirection;

            RaycastHit hit;

            if(Physics.Raycast(aimTransform.position, forward, out hit, Mathf.Infinity))
            {
                Debug.Log("Did hit");
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
