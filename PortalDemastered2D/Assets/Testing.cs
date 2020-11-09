using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils; 
public class Testing : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private PlayerAimWeapon playerAimWeapon;

    private void Start()
    {
        playerAimWeapon.OnShoot += PlayerAimWeapon_OnShoot;
    }

    private void PlayerAimWeapon_OnShoot(object sender, PlayerAimWeapon.OnShootEventArgs e)
    {
        UtilsClass.ShakeCamera(1f, .2f);
        WeaponTracer.Create(e.gunEndPointPosition, e.shootPosition);
        Shoot_Flash.AddFlash(e.gunEndPointPosition);
    }
}
