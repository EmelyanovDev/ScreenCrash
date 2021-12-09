
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private SkinChange SkinChange;

    [SerializeField] private int numberOfSkin; //по счёту

    public void Click()
    {
       SkinChange.ChangeSkin(numberOfSkin);
    }
}
