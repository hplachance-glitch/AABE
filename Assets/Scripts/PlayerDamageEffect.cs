using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class PlayerDamageEffect : MonoBehaviour
{
    public Image damageOverlay;
    public Animator cameraAnimator;

    public float flashDuration = 0.3f;
    public float flashAlpha = 0.35f;

    public float shakeDuration = 0.5f;

    private Coroutine flashCoroutine;
    private Coroutine shakeCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Color color = damageOverlay.color;
        color.a = 0f;
        damageOverlay.color = color;
    }

    public void PlayDamageEffects()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }

        flashCoroutine = StartCoroutine(DamageFlash());
        shakeCoroutine = StartCoroutine(PlayShake());
    }

    IEnumerator DamageFlash()
    {
        Color color = damageOverlay.color;
        color.a = flashAlpha;
        damageOverlay.color = color;

        float elapsed = 0f;

        while (elapsed < flashDuration)
        {
            elapsed += Time.deltaTime;

            color.a = Mathf.Lerp(
                flashAlpha,
                0f,
                elapsed / flashDuration
            );

            damageOverlay.color = color;

            yield return null;
        }

        color.a = 0f;
        damageOverlay.color = color;
        flashCoroutine = null;
    }

    IEnumerator PlayShake()
    {
        cameraAnimator.Play("Shake", 0, 0f);

        yield return new WaitForSeconds(0.2f);

        cameraAnimator.Play("Idle");

        shakeCoroutine = null;
    }
}
