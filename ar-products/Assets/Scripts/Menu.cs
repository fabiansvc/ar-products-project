using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Quits the player when the user hits escape

public class Menu : MonoBehaviour
{  
    public Button buttonInfoExit;
    public Button buttonInfoShow;
    public GameObject panelInfo;
    public Button buttonSelectProduct;
    public GameObject panelProducts;

    // Function that listen the exit event
    public void OnHandleOff()
    {
        Application.Quit();
    }

    public void OnHandleInfoShow()
    {
        ChangeStatusMenu(false, true, true, false, false);
    }

    public void OnHandleInfoExit()
    {
        ChangeStatusMenu(true, false, false, true, false);
    }

    private void ChangeStatusMenu(bool statusButtonInfoShow, bool statusButtonInfoExit,
        bool statusPanelInfo, bool statusButtonSelectProduct, bool statusPanelProducts)
    {
        buttonInfoShow.gameObject.SetActive(statusButtonInfoShow);
        buttonInfoExit.gameObject.SetActive(statusButtonInfoExit);
        panelInfo.SetActive(statusPanelInfo);
        buttonSelectProduct.gameObject.SetActive(statusButtonSelectProduct);
        panelProducts.SetActive(statusPanelProducts);
    }

}