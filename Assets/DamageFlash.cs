using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] Material flashMaterial;
    [SerializeField] float duration = 0.1f;

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

        flashRoutine = StartCoroutine(FlashRoutine(duration));
    }

    IEnumerator FlashRoutine(float timer) {
        renderer.material = flashMaterial;

        yield return new WaitForSeconds(duration);

        renderer.material = originalMaterial;

        flashRoutine = null;
    }
}
