using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum popupType
{
    none,
    explain,
    recipe,
    weapon,
    enforce,
    autoCraft
}
public abstract class Popup : MonoBehaviour
{
    [SerializeField] popupType popupType;
    GameObject popupObject;

    protected Animator animator;

    private void Start()
    {
        popupObject = gameObject;
        animator = GetComponent<Animator>();
        Close();
    }

    #region ÆË¾÷ ²ô°í ´Ý±â
    public void Open()
    {
        popupObject.SetActive(true);
    }
    public void Close()
    {
        popupObject.SetActive(false);
    }
    public void CloseStart()
    {
        animator.SetTrigger("close");
    }
    public void ResetCloseStart()
    {
        animator.SetTrigger("resetClose");
    }
    public void ResetClose()
    {
        if (EquipmentSlot.currentSelectedSlot != null)
        {
            EquipmentSlot.currentSelectedSlot = null;
            //EnforceManager.Instance.FreshSlot();
            EquipmentManager.instance.SetEquipManager();
            DataManager.instance.JsonSave();
        }
        Close();
    }
    #endregion
}
