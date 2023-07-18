using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponSlot : MonoBehaviour
{
    [SerializeField] Image image;
   // [SerializeField] Weapon weapon;//
    public void ClearSlot()
    {
       // weapon = null;
        image.color = new Color(1, 1, 1, 0);
    }
    
}
