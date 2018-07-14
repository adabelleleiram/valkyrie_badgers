using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public Canvas canvas;

    float startHeight;
    float endHeight;

    float speed;
    float currentTime;

    public void StartCredits(float aTime)
    {
        gameObject.SetActive(true);

        float halfHeight = GetComponent<RectTransform>().rect.height * 0.5f;
        float halfParentHeight = canvas.GetComponent<RectTransform>().rect.height * 0.5f;

        endHeight = halfParentHeight + halfHeight;
        startHeight = -endHeight;

        speed = (endHeight - startHeight) / aTime;

        transform.localPosition = new Vector3(
            transform.localPosition.x,
            startHeight,
            transform.localPosition.z);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        float height = startHeight + speed * currentTime;

        transform.localPosition = new Vector3(
            transform.localPosition.x,
            height,
            transform.localPosition.z);
    }
}
