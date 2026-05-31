using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrame;
public class UIFrameTest : MonoBehaviour
{
    public PopupWindow popupWindow1;
    public PopupWindow popupWindow2;
    public PopupWindow popupWindow3;
    public PopupWindow popupWindow4;
    public PopupWindow popupWindow5;
    public string testPrefabName;
    public GameObject testLoadedPrefab;
    public PopupWindowManager popupWindowManager;

    public GameObject GetPrefabFromResources(string prefabName)
    {
        if (string.IsNullOrWhiteSpace(prefabName))
        {
            Debug.LogWarning("Prefab name is empty.", this);
            return null;
        }

        GameObject prefab = Resources.Load<GameObject>(prefabName.Trim());
        if (prefab == null)
        {
            Debug.LogWarning($"Prefab '{prefabName}' was not found in Resources.", this);
        }

        return prefab;
    }

    [ContextMenu("Load Test Prefab From Resources")]
    public void LoadTestPrefabFromResources()
    {
        testLoadedPrefab = GetPrefabFromResources(testPrefabName);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !Input.GetKey(KeyCode.H))
        {
            popupWindow1.Open();
        }
        if (Input.GetKeyDown(KeyCode.S) && !Input.GetKey(KeyCode.H))
        {
            popupWindow2.Open();
        }
        if (Input.GetKeyDown(KeyCode.D) && !Input.GetKey(KeyCode.H))
        {
            popupWindow3.Open();
        }
        if (Input.GetKeyDown(KeyCode.F) && !Input.GetKey(KeyCode.H))
        {
            popupWindow4.Open();
        }
        if (Input.GetKeyDown(KeyCode.G) && !Input.GetKey(KeyCode.H))
        {
            popupWindow5.Open();
        }
        if (Input.GetKey(KeyCode.H) && Input.GetKeyDown(KeyCode.A))
        {
            popupWindow1.Close();
        }
        if (Input.GetKey(KeyCode.H) && Input.GetKeyDown(KeyCode.S))
        {
            popupWindow2.Close();
        }
        if (Input.GetKey(KeyCode.H) && Input.GetKeyDown(KeyCode.D))
        {
            popupWindow3.Close();
        }
        if (Input.GetKey(KeyCode.H) && Input.GetKeyDown(KeyCode.F))
        {
            popupWindow4.Close();
        }
        if (Input.GetKey(KeyCode.H) && Input.GetKeyDown(KeyCode.G))
        {
            popupWindow5.Close();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadTestPrefabFromResources();
            if (testLoadedPrefab != null)
            {
                GameObject instantiatedPrefab = Instantiate(testLoadedPrefab);
                PopupWindow popupWindowComponent = instantiatedPrefab.GetComponent<PopupWindow>();
                if (popupWindowComponent != null)
                {
                    popupWindowManager.AddPopupWindow(popupWindowComponent);
                }
                else
                {
                    Debug.LogWarning("The instantiated prefab does not have a PopupWindow component.", this);
                }
                popupWindowComponent.Open();
            }
        }
    }

}
