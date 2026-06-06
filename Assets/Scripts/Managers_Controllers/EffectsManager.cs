using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager I;

    //Text color flash
    private Color textFlashColor = new Color(1f, 0.3f, 0.3f);
    private float flashDuration = 0.2f;
    private Dictionary<TextMeshProUGUI, Coroutine> activeFlashing = new();

    private void Awake()
    {
        if(I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
    }

    public void TextFlash(TextMeshProUGUI text)
    {
        if(activeFlashing.TryGetValue(text, out Coroutine coroutine))
            StopCoroutine(coroutine);

        activeFlashing[text] = StartCoroutine(TextFlashRoutine(text));
    }

    IEnumerator TextFlashRoutine(TextMeshProUGUI text)
    {
        text.color = textFlashColor;
        yield return new WaitForSeconds(flashDuration);
        text.color = Color.white;

        activeFlashing.Remove(text);
    }
}
