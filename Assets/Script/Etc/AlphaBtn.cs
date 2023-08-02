using UnityEngine;
using UnityEngine.UI;

public class AlphaBtn : MonoBehaviour
{
    public float AlphaThreshold = 0.1f;
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
    }
    public void ActiveBtn(bool Active) { button.interactable = Active; }
}