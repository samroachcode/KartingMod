using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenEffect : MonoBehaviour
{
    public float FadeTime = 0.5f;
    public Pumpkin[] Pumpkins;
    public Image ScreenEffectVisual; 
    private bool _screenEffectOn = false;
    private Vector3 _screenEffectStartPosition;
    private void Awake()
    {
        _screenEffectStartPosition = ScreenEffectVisual.transform.localPosition;
        for (int i = 0; i < Pumpkins.Length; i++)
        {
            Pumpkins[i].PumpkinHit.AddListener(BloodEffect);
        }
    }


    void BloodEffect()
    {
        if (!_screenEffectOn)
        {
            _screenEffectOn = true;
            StartCoroutine(MoveScreenEffect(0.0f,1.0f));
        }
    }


    private IEnumerator MoveScreenEffect(float alphaStart, float alphaend)
    {
        Image image = ScreenEffectVisual.GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = alphaStart;
        image.color = tempColor;
        float t = 0f;
        float at = 1.0f / FadeTime;
        while (t < 1f)
        {
            tempColor.a = Mathf.Lerp(alphaStart, alphaend, t);
            t += Time.deltaTime;
            image.color = tempColor;
            yield return null;
        }
        if (alphaend == 1.0f)
        {
            StartCoroutine(WaitCoroutine(0.5f));
        }
    }

    private IEnumerator WaitCoroutine(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        StartCoroutine(MoveScreenEffect(1, 0));
        yield return new WaitForSeconds(FadeTime);
        _screenEffectOn = false; 
    }
}
