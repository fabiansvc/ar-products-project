using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Products : MonoBehaviour
{
    private static Products instance;
    private GameObject product;
    public Button buttonSelectProduct;
    public GameObject panelProducts;
    private string productSelected;
    
    private void Awake()
    {
        instance = this;
    }

    public static Products GetInstance()
    {   
        return instance == null? instance = new Products() : instance;
    }

    public GameObject GetProduct()
    {
        return product;
    }

    public void SetProduct(GameObject _product)
    {
        product = _product;
    }
    
    public string GetProductSelected()
    {
        return productSelected;
    }

    public void SetProductSelected(string _productSelected)
    {
        productSelected = _productSelected;
    }

    public void DisposeProduct()
    {
        if(product != null)
        {
            Destroy(product);
        }
    }

    public void OnHandleSelectProduct()
    {
        productSelected = "None";
        DisposeProduct();
        ChangeStatusProducts(false, true);
    }

    public void OnHandleProduct1()
    {
        productSelected = "Chair";
        ChangeStatusProducts(true, false);
    }

    public void OnHandleProduct2()
    {
        productSelected = "Sword";
        ChangeStatusProducts(true, false);
    }

    public void OnHandleProduct3()
    {
        productSelected = "Ball";
        ChangeStatusProducts(true, false);
    }

    private void ChangeStatusProducts(bool statusButtonSelectProduct, bool statusPanelProducts)
    {
        buttonSelectProduct.gameObject.SetActive(statusButtonSelectProduct);
        panelProducts.SetActive(statusPanelProducts);
    }
}
