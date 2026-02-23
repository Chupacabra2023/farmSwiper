using UnityEngine;
using UnityEngine.UI;

public class GrassBehaviour : MonoBehaviour
{
    public Sprite tallGrassSprite;
    public Sprite cutGrassSprite;
    private Image grassImage;
    private bool isCut = false;

    void Start()
    {
        grassImage = GetComponent<Image>();
    }

    public bool IsCut => isCut;

    public void Cut()
    {
        if (!isCut)
        {
            isCut = true;
            grassImage.sprite = cutGrassSprite;
            Debug.Log("Grass cut!");
        }
    }
}