using ScratchMe;
using UnityEngine;
using UnityEngine.UI;

public class ScrachManager : MonoBehaviour
{
    public Transform YellowGroup;

    public Transform BlueGroup;

    public Transform GreenGroup;

    public Transform HuaBan;

    public Image CurrentPaint;

    public Sprite BlueBrushNormal;

    public Sprite BlueBrushPaint;

    public Sprite YellowBrushNormal;

    public Sprite YellowBrushPaint;

    public Sprite GreenBrushNormal;

    public Sprite GreenBrushPaint;

    private Sprite NormalBrush;

    private Sprite NormalBrushPaint;
     
    public Button GreenButton;

    public Button YellowButton;

    public Button BlueButton;

    InteractionManager manger;

    public int SucCount = 0;

    public ScratchImage blue;
    public ScratchImage green;
    public ScratchImage yellow;

    private void Start()
    {
        HuaBan.SetAsLastSibling();
        manger = FindObjectOfType<InteractionManager>();
        NormalBrush = manger.normalHandTexture;
        NormalBrushPaint = manger.gripHandTexture;


        GreenButton.onClick.AddListener(OnGreenClick);
        YellowButton.onClick.AddListener(OnYellowClick);
        BlueButton.onClick.AddListener(OnBlueClick);
         
        OnBlueClick();

        blue.Brush.Size.Set(600, 800);
        green.Brush.Size.Set(600, 800);
        yellow.Brush.Size.Set(600, 800); 
    }

    private void Update()
    {
        if(SucCount == 3)
        { 
            manger.normalHandTexture = NormalBrush;
            manger.gripHandTexture = NormalBrushPaint;
            manger.releaseHandTexture = NormalBrush;
            manger.guiHandCursor.sprite = NormalBrush;

            GameRoot.Instance.Pop(null);
            GameRoot.Instance.Scene1Animation3DisAble();
        }
    }

    public void OnGreenClick()
    {
        green.CanScrach = true;
        blue.CanScrach = false;
        yellow.CanScrach = false; 

        CurrentPaint.sprite = GreenBrushNormal;
        GreenGroup.SetAsLastSibling();
        HuaBan.SetAsLastSibling();
        manger.normalHandTexture = GreenBrushNormal;
        manger.gripHandTexture = GreenBrushPaint;
        manger.releaseHandTexture = GreenBrushNormal;
        manger.guiHandCursor.sprite = GreenBrushNormal;
    }
    public void OnYellowClick()
    {
        green.CanScrach = false;
        blue.CanScrach = false;
        yellow.CanScrach = true;

        CurrentPaint.sprite = YellowBrushNormal;
        YellowGroup.SetAsLastSibling();
        HuaBan.SetAsLastSibling();
        manger.normalHandTexture = YellowBrushNormal;
        manger.gripHandTexture = YellowBrushPaint;
        manger.releaseHandTexture = YellowBrushNormal;
        manger.guiHandCursor.sprite = YellowBrushNormal;
    }
    public void OnBlueClick()
    {
        green.CanScrach = false;
        blue.CanScrach = true;
        yellow.CanScrach = false;

        CurrentPaint.sprite = BlueBrushNormal;
        BlueGroup.SetAsLastSibling();
        HuaBan.SetAsLastSibling();
        manger.normalHandTexture = BlueBrushNormal;
        manger.gripHandTexture = BlueBrushPaint;
        manger.releaseHandTexture = BlueBrushNormal;
        manger.guiHandCursor.sprite = BlueBrushNormal;
    }

    public void CompleteEvent()
    {
        Debug.Log("wancheng");
        SucCount  = SucCount+1;
    }
}
