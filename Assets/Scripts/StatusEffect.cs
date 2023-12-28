using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour {
    Enemy enemy;
    SpriteRenderer renderer;
    float originalSpeed;
    float slowTimer;
    float slowMaxDuration = 2;
    Color originalColor;
    void Start() {
        enemy = GetComponent<Enemy>();
        if(enemy != null) {
            originalSpeed = enemy.speed;
            renderer = GetComponent<SpriteRenderer>();
            originalColor = renderer.color;
        } else {
            enabled = false;
        }
    }

    public void Slow() {
        enemy.speed = originalSpeed / 2;
        slowTimer = slowMaxDuration;
        renderer.color = Color.blue;
        Debug.Log("Slow");
    }

    private void Update() {
        if (slowTimer > 0) {
            slowTimer -= Time.deltaTime;
        } else if (slowTimer < 0) {
            slowTimer = 0;
            enemy.speed = originalSpeed;
            renderer.color = originalColor;
        }
    }

}
