using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Quits the player when the user hits escape

public class Menu : MonoBehaviour
{  
    public Button buttonClose;
    public Button buttonInfo;
    public GameObject panelInfo;
    public Button buttonSelectProduct;
    public GameObject panelProducts;

    // Function that listen the exit event
    public void OnHandleLogout()
    {
        Application.Quit();
    }

    public void OnHandleInfo()
    {
        ChangeStatusMenu(false, true, true, false, false);
    }

    public void OnHandleClose()
    {
        ChangeStatusMenu(true, false, false, true, false);
    }

    private void ChangeStatusMenu(bool statusButtonInfo, bool statusButtonClose,
        bool statusPanelInfo, bool statusButtonSelectProduct, bool statusPanelProducts)
    {
        buttonInfo.gameObject.SetActive(statusButtonInfo);
        buttonClose.gameObject.SetActive(statusButtonClose);
        panelInfo.SetActive(statusPanelInfo);
        buttonSelectProduct.gameObject.SetActive(statusButtonSelectProduct);
        panelProducts.SetActive(statusPanelProducts);
    }

}