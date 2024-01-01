using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [SerializeField] Weapon gun;

    public Weapon GetWeapon() {
        return gun;
    }

    public void DestroyPickup() {
        Destroy(gameObject);
    }
}
