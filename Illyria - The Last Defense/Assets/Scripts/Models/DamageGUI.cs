using UnityEngine;
using System.Collections;
using TMPro;

public class DamageGUI : MonoBehaviour
{
    public GameObject damageIndicator;
    public GameObject temp;

    private void Start()
    {
        damageIndicator = Resources.Load<GameObject>("UI/DamageIndicator");
    }

    public void ShowDamageUI(Transform whereTo, int damage, Color color, int fontSize = 24)
    {
        temp = Instantiate(damageIndicator, whereTo.transform.position,Quaternion.identity);
        temp.transform.position = temp.transform.position + Vector3.up * 1.5f;
        TextMeshPro tempTMP = temp.GetComponent<TextMeshPro>();
        tempTMP.text = "-" + damage.ToString();
        tempTMP.fontSize = fontSize;
        Destroy(temp, 1.5f);
        if (color == Color.white)
        {
            return;
        }
        tempTMP.color = color;
       
    }
}
