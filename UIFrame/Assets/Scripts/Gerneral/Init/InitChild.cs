using UnityEngine;
namespace General.Init
{
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
}