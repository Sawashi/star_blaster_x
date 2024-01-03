using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] Material flashMaterial;
    [SerializeField] float duration = 0.1f;
    [SerializeField] float playerDuration = 0.5f;
    [SerializeField] bool isPlayer;

    private SpriteRenderer renderer;
    private Material originalMaterial;
    private Coroutine flashRoutine  ;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        originalMaterial = renderer.material;
    }

    public void Flash() {
        if(flashRoutine != null) {
            StopCoroutine(flashRoutine);
        }
        if (isPlayer) {
            flashRoutine = StartCoroutine(FlashPlayerRoutine(playerDuration));

        } else {
            flashRoutine = StartCoroutine(FlashRoutine(duration));

        }
    }

    IEnumerator FlashRoutine(float timer) {
        renderer.material = flashMaterial;

        yield return new WaitForSeconds(timer);

        renderer.material = originalMaterial;

        flashRoutine = null;
    }

    IEnumerator FlashPlayerRoutine(float timer) {
        renderer.material = flashMaterial;
        yield return new WaitForSeconds(timer * 0.4f);
        renderer.material = originalMaterial;
        yield return new WaitForSeconds(timer * 0.2f);
        renderer.material = flashMaterial;
        yield return new WaitForSeconds(timer * 0.4f);
        renderer.material = originalMaterial;

        flashRoutine = null;
    }
}
