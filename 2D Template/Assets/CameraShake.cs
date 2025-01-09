using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    Camera cam;

    float shakeDur = 0f;
    float shakeMag = 0.6f;
    float damingSpeed = 1f;

    Vector3 initalPos;

    private void Awake()
    {
        instance = this;
        cam = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        initalPos = transform.localPosition;
    }

    private void Update()
    {
        if(shakeDur > 0f)
        {
            transform.localPosition = initalPos + Random.insideUnitSphere * shakeMag;
            shakeDur -= Time.deltaTime * damingSpeed;
        }
        else
        {
            shakeDur = 0f;
            transform.localPosition = initalPos;
        }
    }
    public void Shake(float sDur, float sMag, float dSpeed)
    {
        shakeDur = sDur;
        shakeMag = sMag;
        damingSpeed = dSpeed;
    }
}


//public IEnumerator Shake(float duration, float magnitude)
//{
//    Vector3 originalPos = transform.localPosition;

//    float elapsed = 0.0f;

//    while (elapsed < duration)
//    {
//        float x = Random.Range(-1f, 1f) * magnitude;
//        float y = Random.Range(-1f, 1f) * magnitude;

//        transform.localPosition = new Vector3(x, y, originalPos.z);

//        elapsed += Time.deltaTime;

//        yield return null;
//    }

//    transform.localPosition = originalPos;
//}