using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject _weaponLogic;

    public void EnableWeapon()
    {
        _weaponLogic.gameObject.SetActive(true);
    }
    public void DisableWeapon()
    {
        _weaponLogic.gameObject.SetActive(false);
    }
}
