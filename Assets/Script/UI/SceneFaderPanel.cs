using CustomFrame;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class SceneFaderPanel : BasePanel
{
    Image Fadeimage;
    float fadeInDuration = 2.5f;
    float fadeOutDuration = 2.5f;

    static readonly string path = "Prefabs/UI/Panel/SceneFaderPanel";

    public SceneFaderPanel() : base(new UIType(path))
    {


    }

    public override void OnEnter()
    {
        base.OnEnter(); 
        Fadeimage = UITool.GetOrAddComponentInChildren<Image>("FadeImage");
        this.StartCoroutine(FadeOutIn());
    }
     
    public IEnumerator FadeOutIn()
    {
        yield return FadeOut(fadeInDuration);
        yield return FadeIn(fadeOutDuration); 
    } 

    public IEnumerator FadeOut(float time)
    {
   
        while (Fadeimage.color.a < 1)
        {
            Fadeimage.color  += new Color(Fadeimage.color.r, Fadeimage.color.g, Fadeimage.color.b, Time.deltaTime / time) ; 
            yield return null;
        }
        GameRoot.Instance.AllowChangeScene();
        
    }
    public IEnumerator FadeIn(float time)
    {
         
        while (Fadeimage.color.a != 0)
        {
            Fadeimage.color -= new Color(Fadeimage.color.r, Fadeimage.color.g, Fadeimage.color.b, Time.deltaTime / time);
            yield return null;
        } 
    }
}
