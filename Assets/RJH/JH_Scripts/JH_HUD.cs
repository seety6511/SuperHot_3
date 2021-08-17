using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JH_HUD : MonoBehaviour
{
    [SerializeField]
    private SH_Player theGunController;

    [SerializeField]
    private Text[] text_bullet;
    public void CheckBullet()
    {
        text_bullet[0].text = theGunController.reloadBulletCount.ToString();
        text_bullet[1].text = theGunController.currentBulletCount.ToString();
    }
}
