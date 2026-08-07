using UnityEngine;

public class InitChild : MonoBehaviour
{
    void Awake()
    {
        IInit[] initComponents = GetComponentsInChildren<IInit>(true);
        foreach (var initComponent in initComponents)
        {
            initComponent.Init();
        }
    }
}
