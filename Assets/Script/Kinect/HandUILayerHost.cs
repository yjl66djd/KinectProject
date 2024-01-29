using CustomFrame;
using UnityEngine;

public class HandUILayerHost : MonoBehaviour
{
    
    void Start()
    {
        this.AddUpdate(onUpdate);
    }
     
    void onUpdate()
    {
        transform.SetAsLastSibling();
    }

    private void OnDestroy()
    {
        this.RemoveUpdate(onUpdate);
    }
}
