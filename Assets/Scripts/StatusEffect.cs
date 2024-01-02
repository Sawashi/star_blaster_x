using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour {
    Enemy enemy;
    EnemyController enemyController;
    SpriteRenderer renderer;
    float originalSpeed;
    float slowTimer;
    float slowMaxDuration = 1;
    bool isMegamanEnemy;
    Color originalColor;
    void Start() {
        enemy = GetComponent<Enemy>();
        enemyController = GetComponent<EnemyController>();
        renderer = GetComponent<SpriteRenderer>();
        originalColor = renderer.color;

        if (enemy != null) {
            originalSpeed = enemy.speed;
            isMegamanEnemy = false;
        } else if(enemyController != null) {
            isMegamanEnemy = true;
        } else {
            enabled = false;
        }
    }

    public void Slow() {
        if (isMegamanEnemy) {
            enemyController.FreezeEnemy(true);
        } else {
            enemy.speed = originalSpeed / 2;
        }
        slowTimer = slowMaxDuration;
        renderer.color = Color.blue;
    }

    private void Update() {
        if (slowTimer > 0) {
            slowTimer -= Time.deltaTime;
        } else if (slowTimer < 0) {
            slowTimer = 0;
            renderer.color = originalColor;
            if (isMegamanEnemy) {
                enemyController.FreezeEnemy(false);
            } else {
                enemy.speed = originalSpeed;
            }
        }
    }

}
