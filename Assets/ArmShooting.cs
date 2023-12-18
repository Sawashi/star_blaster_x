using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmShooting : MonoBehaviour {

    private Camera cam;
    private Vector2 aimDir;
    private Renderer armRenderer;
    private Player player;
    private Renderer gunRenderer;
    [SerializeField] private GameObject shootingPoint;
    public float projectileSpeed = 5f;
    public Bolt ammo;

    // Start is called before the first frame update
    void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        armRenderer = GetComponent<Renderer>();
        gunRenderer = transform.GetChild(0).transform.GetComponent<Renderer>();
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update() {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        aimDir = mousePos - transform.position;
        aimDir.y -= 0.15f;
        aimDir.Normalize();
        float rot_z = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        if (rot_z > 90 || rot_z < -90) {
            transform.rotation = Quaternion.Euler(0, 0, rot_z + 180);
            transform.localScale = new Vector3(-1, 1, 1);
            if (player.isFacingRight) {
                armRenderer.sortingOrder = 2;
                gunRenderer.sortingOrder = 1;
            } else {
                armRenderer.sortingOrder = -1;
                gunRenderer.sortingOrder = 0;
            }

        } else {
            transform.rotation = Quaternion.Euler(0, 0, rot_z);
            transform.localScale = new Vector3(1, 1, 1);
            if (player.isFacingRight) {
                armRenderer.sortingOrder = -1;
                gunRenderer.sortingOrder = 0;
            } else {
                armRenderer.sortingOrder = 2;
                gunRenderer.sortingOrder = 1;
            }

        }
    }

    public void Shoot() {
        Quaternion rot = transform.rotation;
        
        Bolt bullet = Instantiate<Bolt>(ammo, shootingPoint.transform.position, rot);
        if (aimDir.x > 0) {
            bullet.Flip();
        }
        bullet.Launch(aimDir, projectileSpeed);
    }
}
