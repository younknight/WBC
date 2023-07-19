
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    Unit unit;
    Slider hpSlider;
    // Start is called before the first frame update
    
    public void Setup(Unit unit)
    {
        this.unit = unit;
        hpSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(unit != null)
        hpSlider.value = unit.Hp / unit.MaxHp;
    }
}
