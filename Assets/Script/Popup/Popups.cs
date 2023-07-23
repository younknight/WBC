using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Popups : MonoBehaviour
{
    [SerializeField] GameObject PopupObj;
    //Animator animator;
    ////default
    //[Space(10f)]
    //[Header("default")]
    ////getItem
    //[Space(10f)]
    //[Header("getItem")]
    ////recipe
    //[Space(10f)]
    //[Header("recipe")]
    ////status
    //[Space(10f)]
    //[Header("status")]
    ////[SerializeField] TextMeshProUGUI attackSpeed;

    //[SerializeField] Inventory inventory;
    //[SerializeField] Player player;
    //[Space(10f)]
    //[Header("purchase")]
    //[Space(10f)]
    //[Header("Weapon")]
    //[SerializeField] GameObject Equip;
    //[SerializeField] GameObject Enforce;



    //#region 액티브 버튼
    //public void EquipRemove()//장비 해제ㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔ
    //{

    //    if (EquipmentSlot.currentSelectedSlot.Weapon != null)
    //    {
    //        InventoryManager.instance.AddItems<Weapon>(EquipmentSlot.currentSelectedSlot.Weapon, 1);
    //        EquipmentManager.instance.Unit.BuffStatusWithWeapon(false, EquipmentSlot.currentSelectedSlot.Weapon);
    //        EquipmentManager.EquipWeapon[EquipmentSlot.currentSelectedSlot.Id] = null;
    //        EquipmentSlot.currentSelectedSlot.FreashSlot();
    //        DataManager.instance.JsonSave();

    //    }
    //    PopupManager.instance.CloesPopup(popupType.weapon);
    //}
    //#endregion
    //#region 팝업 설정해주기


    //public void SetWeapon(bool isEnforce)
    //{
    //    Enforce.SetActive(isEnforce);
    //    Equip.SetActive(!isEnforce);
    //}
    //#endregion
}
