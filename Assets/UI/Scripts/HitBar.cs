using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBar : MonoBehaviour
{
    private RectTransform rTransform;

    private void Awake()
    {
        rTransform = GetComponent<RectTransform>();
    }

    public void SetPosition(float countFood)
    {
        rTransform.anchoredPosition = new Vector2(rTransform.anchoredPosition.x, -(100 - countFood) * rTransform.rect.height / 100);
    }
}
