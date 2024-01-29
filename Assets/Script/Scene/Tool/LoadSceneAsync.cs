using UnityEngine;
using UnityEngine.UI;

public class LoadSceneAsync  :MonoBehaviour
{
    public Text text;
    public Slider slider;

    private void Start()
    {
        GameRoot.Instance.LoadingPanel = this;
    }

    private void OnDestroy()
    {
        GameRoot.Instance.LoadingPanel = null;
    }
}
