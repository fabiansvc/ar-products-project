using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Products : MonoBehaviour
{
    private static Products instance;
    public Button buttonSelectProduct;
    public GameObject productsGameObject;
    private string productSelected;
    private GameObject product;
    
    private void Awake()
    {
        instance = this;
        product = null;
        productSelected = "None";
    }

    public static Products GetInstance()
    {   
        return instance == null? instance = new Products() : instance;
    }

    public void OnClickButtonSelectProduct()
    {
        productSelected = "None";
        DisposeProduct();
        ChangeVisibitilityButtons(false, true);
    }

    public void OnClickButtonProduct1()
    {
        productSelected = "Chair";
        ChangeVisibitilityButtons(true, false);
    }

    public void OnClickButtonProduct2()
    {
        productSelected = "Table";
        ChangeVisibitilityButtons(true, false);
    }

    public void OnClickButtonProduct3()
    {
        productSelected = "Watch";
        ChangeVisibitilityButtons(true, false);
    }

    public string GetProductSelected()
    {
        return productSelected;
    }

    public void SetProductSelected(string _productSelected)
    {
        productSelected = _productSelected;
    }

    public GameObject GetProduct()
    {
        return product;
    }

    public void SetProduct(GameObject _product)
    {
        product = _product;
    }

    public void DisposeProduct()
    {
        if(product != null)
        {
            Destroy(product);
        }
    }

    private void ChangeVisibitilityButtons(bool statusButtonSelectProduct, bool statusProductsGameObject)
    {
        buttonSelectProduct.gameObject.SetActive(statusButtonSelectProduct);
        productsGameObject.SetActive(statusProductsGameObject);
    }
}
