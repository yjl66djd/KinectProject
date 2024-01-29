using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnDrugMovrAaA: MonoBehaviour, InteractionListenerInterface
{
 
	[Tooltip("Interaction manager instance, used to detect hand interactions. If left empty, the component will try to find a proper interaction manager in the scene.")]
	private InteractionManager interactionManager;

    [Tooltip("Index of the player, tracked by the respective InteractionManager. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
    public int playerIndex = 0;

    [Tooltip("Whether the left hand interaction is allowed by the respective InteractionManager.")]
	public bool leftHandInteraction = true;

	[Tooltip("Whether the right hand interaction is allowed by the respective InteractionManager.")]
	public bool rightHandInteraction = true;


	//private bool isLeftHandDrag = false;
	private InteractionManager.HandEventType lastHandEvent = InteractionManager.HandEventType.None;
	private Vector3 screenNormalPos = Vector3.zero;

	private GameObject selectedObject;

    public bool IsHide;
	void Start()
	{ 
		// get the interaction manager instance
		if (interactionManager == null) 
		{
			//interactionManager = InteractionManager.Instance;
            interactionManager = GetInteractionManager();
        }
        interactionManager.interactionListeners.Add(this);
        MarkPos = interactionManager.guiHandCursor.rectTransform;
        INDEX = int.Parse(this.name)-1;

    }
    // tries to locate a proper interaction manager in the scene
    private InteractionManager GetInteractionManager()
    {
        // find the proper interaction manager
        MonoBehaviour[] monoScripts = FindObjectsOfType(typeof(MonoBehaviour)) as MonoBehaviour[];

        foreach (MonoBehaviour monoScript in monoScripts)
        {
            if ((monoScript is InteractionManager) && monoScript.enabled)
            {
                InteractionManager manager = (InteractionManager)monoScript;

                if (manager.playerIndex == playerIndex && manager.leftHandInteraction == leftHandInteraction && manager.rightHandInteraction == rightHandInteraction)
                {
                    return manager;
                }
            }
        }

        // not found
        return null;
    }

    public OnDrugMovrAaA onDrug;
    void Update() 
	{
		if(interactionManager != null && interactionManager.IsInteractionInited())
		{
            if (0==interactionManager.GetUserID())
            {

            }

		}
	}
	public void HandGripDetected(long userId, int userIndex, bool isRightHand, bool isHandInteracting, Vector3 handScreenPos)
	{
        //if (GrupeOnline.grupeOnline.ONGJ != int.Parse(this.name))
        //    return;
        if (!isHandInteracting || !interactionManager)
			return;
		if (userId != interactionManager.GetUserID())
			return;

		lastHandEvent = InteractionManager.HandEventType.Grip;
        this.GetComponent<BoxCollider2D>().enabled = true;
        print("手势--------------------------------握拳");
	}

	public void HandReleaseDetected(long userId, int userIndex, bool isRightHand, bool isHandInteracting, Vector3 handScreenPos)
	{
        //if (GrupeOnline.grupeOnline.ONGJ!=int.Parse(this.name))
        //    return;
        if (!isHandInteracting || !interactionManager)
			return;
		if (userId != interactionManager.GetUserID())
			return;

		lastHandEvent = InteractionManager.HandEventType.Release;
       
        this.GetComponent<BoxCollider2D>().enabled = false;
        float distance = Vector2.Distance(MuBiaoDiDian.anchoredPosition, this.GetComponent<RectTransform>().anchoredPosition);
      
        if (distance <100)
        {
            Debug.LogError("------------------------------------------------");
          int a=  Random.Range(0, 10);
            if (a<5)
            {
                tishi.transform.localScale = new Vector3(-1,1,1);
                tishi.GetComponent<HideAuto>().state = "test_ni"; 
            }
            else
            {
                tishi.transform.localScale = new Vector3(1, 1, 1);
                tishi.GetComponent<HideAuto>().state = "Shun";
            }
            tishi.SetActive(true);
            if (onDrug!=null)
            {
                onDrug.gameObject.SetActive(true);
            }
            print("手势--------------------------------开启下一关" );
            GrupeOnline.grupeOnline.ONGJ=  int.Parse(this.name) + 1; 
            this.gameObject.SetActive(false);
            interactionManager = null;
            interactionManager.interactionListeners.Remove(this);
        }
      
    }
    public GameObject tishi;
    public bool HandClickDetected(long userId, int userIndex, bool isRightHand, Vector3 handScreenPos)
    {
        return true;
    }

    public RectTransform MarkPos;
    public RectTransform MuBiaoDiDian;

 
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (lastHandEvent == InteractionManager.HandEventType.Release) return;

        //if (GrupeOnline.grupeOnline.ONGJ != int.Parse(this.name))
        //    return;
        //if (GrupeOnline.grupeOnline.isDrug == true)
        //    return;
        if (collision.tag == "Player")
        {
            //可以移动
            this.GetComponent<RectTransform>().anchoredPosition = MarkPos.anchoredPosition;
           
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //可以移动
            
            GrupeOnline.grupeOnline.isDrug = false;
        }
    }
    public List<RectTransform> rectTransformsA;
    private int INDEX;
    public void JinYiWei(int index)
    {
        if (INDEX> index)
        {
            INDEX--;
            this.GetComponent<RectTransform>().DOAnchorPos(rectTransformsA[INDEX].anchoredPosition, 0.5f);
        }
    }
    public void OnDisable()
    {
        this.transform.parent.gameObject.BroadcastMessage("JinYiWei", INDEX, SendMessageOptions.DontRequireReceiver);
        GrupeOnline.grupeOnline.isDrug = false;
    }
}

