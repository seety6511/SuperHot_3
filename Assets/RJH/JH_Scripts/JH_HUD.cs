using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JH_HUD : MonoBehaviour
{

    [SerializeField]
    private SH_Player theGunController;
    private GameObject weapon;


    [SerializeField]
    GameObject go_BulletHUD;

    [SerializeField]
    private Text[] text_bullet;



    void Update()
    {
        CheckBullet();
    }

    private void CheckBullet()
    {
        weapon = theGunController.GetGun();
        text_bullet[0].text = theGunController.reloadBulletCount.ToString();
        text_bullet[1].text = theGunController.currentBulletCount.ToString();
    }
}
