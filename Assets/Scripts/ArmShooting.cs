using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmShooting : MonoBehaviour
{
    [SerializeField] private GameObject gunSpawnpoint;
    private Camera cam;
    private Vector2 aimDir;
    private Renderer armRenderer;
    private Player player;
    private Weapon gun;


    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        armRenderer = GetComponent<Renderer>();
        gun = gunSpawnpoint.transform.GetComponentInChildren<Weapon>();
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        aimDir = mousePos - transform.position;
        aimDir.y -= 0.15f;
        aimDir.Normalize();
        float rot_z = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        if (rot_z > 90 || rot_z < -90)
        {
            transform.rotation = Quaternion.Euler(0, 0, rot_z + 180);
            transform.localScale = new Vector3(-1, 1, 1);
            if (player.isFacingRight)
            {
                armRenderer.sortingOrder = 2;
                gun.renderer.sortingOrder = 1;
            }
            else
            {
                armRenderer.sortingOrder = -1;
                gun.renderer.sortingOrder = 0;
            }

        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, rot_z);
            transform.localScale = new Vector3(1, 1, 1);
            if (player.isFacingRight)
            {
                armRenderer.sortingOrder = -1;
                gun.renderer.sortingOrder = 0;
            }
            else
            {
                armRenderer.sortingOrder = 2;
                gun.renderer.sortingOrder = 1;
            }

        }
    }

    public void SwitchWeapon(Weapon newWeapon) {

        Instantiate<GunPickup>(gun.GetPickup(), player.transform.position, Quaternion.identity);
        Destroy(gun.gameObject);



        Weapon newGun = Instantiate<Weapon>(newWeapon, gunSpawnpoint.transform, false);
        gun = newGun;
    }

    public void Shoot() {
        gun.Shoot(aimDir, transform.rotation);
    }

    public void StopShooting() {
        gun.Stop();
    }
}
