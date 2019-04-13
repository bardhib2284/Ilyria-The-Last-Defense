using UnityEngine;

public abstract class Inventory : MonoBehaviour
{

    public virtual void ShowInventoryUI()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void HideInventoryUI()
    {
        this.gameObject.SetActive(false);
    }

}