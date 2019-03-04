using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAnimation : MonoBehaviour
{
    public RectTransform rect;
    public Image currentHealthImage;

    public void UpdateHealthUI()
    {
        Debug.Log("Updating Health");
        StartCoroutine(UpdateHealthUICoroutine());
    }

    public IEnumerator UpdateHealthUICoroutine()
    {
        RectTransform uiHealthAnimationRectTransform = this.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>();
        int healthChange = this.transform.parent.GetComponent<Character>().Health_Current / this.transform.parent.GetComponent<Character>().Health_Max;
        while (uiHealthAnimationRectTransform.offsetMax.x > healthChange * 5)
        {
            uiHealthAnimationRectTransform.offsetMax = new Vector2(healthChange * 5, -0);
            yield return new WaitForSecondsRealtime(0.2f);
        }
        this.transform.GetChild(0).GetChild(2).GetComponent<Image>().fillAmount = healthChange;
    }
}