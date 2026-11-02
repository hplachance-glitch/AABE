using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//AI CODE
public class ViewmodelAnimation : MonoBehaviour
{
    [Header("References")]
    public RawImage targetRawImage; // Drag your RawImage UI component here[cite: 2]

    [Header("Animation States")]
    public Texture[] Reloadframes;
    public Texture[] shootframes;
    public Texture[] slideframes;

    // Temporary array used to hold whatever animation is currently active
    private Texture[] animationFrames; 

    [Header("Settings")]
    public float frameRate = 0.1f;    // Time delay between each frame[cite: 2]
    public bool loop = true;          // Should the animation loop?[cite: 2]

    public void Start()
    {
        if (targetRawImage == null)
        {
            targetRawImage = GetComponent<RawImage>(); //[cite: 2]
        }
    }

    // Call this to play the reload animation
    public void PlayReloadAnimation()
    {
        animationFrames = Reloadframes;
        PlayCustomAnimation();
    }

    // Call this to play the shooting animation
    public void PlayShootAnimation()
    {
        animationFrames = shootframes;
        PlayCustomAnimation();
    }

    // Call this to play the sliding animation
    public void PlaySlideAnimation()
    {
        animationFrames = slideframes;
        PlayCustomAnimation();
    }

    public void PlayCustomAnimation()
    {
        if (animationFrames != null && animationFrames.Length > 0)
        {
            // Stop any existing animation coroutines first if needed, then start[cite: 2]
            StopAllCoroutines();
            StartCoroutine(PlayAnimationCoroutine()); //[cite: 2]
        }
    }

    private IEnumerator PlayAnimationCoroutine()
    {
        int index = 0; //[cite: 2]

        while (true)
        {
            // Set the current texture
            targetRawImage.texture = animationFrames[index]; //[cite: 2]

            // Move to the next frame
            index++; //[cite: 2]

            // Handle looping or stopping at the end[cite: 2]
            if (index >= animationFrames.Length)
            {
                if (loop) //[cite: 2]
                {
                    index = 0; //[cite: 2]
                }
                else
                {
                    yield break; // Stop the coroutine if not looping[cite: 2]
                }
            }

            // Wait for the specified interval[cite: 2]
            yield return new WaitForSeconds(frameRate);
        }
    }
}