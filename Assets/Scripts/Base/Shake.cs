using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] private float duration, magnitude;

    public IEnumerator ShakeAnimation(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, originalPosition.y + y, originalPosition.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    public void StartShake(float _duration, float _magnitude)
    {
        StartCoroutine(ShakeAnimation(_duration, _magnitude));
    }

    public void StartShake()
    {
        StartCoroutine(ShakeAnimation(duration, magnitude));
    }
}