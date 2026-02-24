using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GrassBehaviour : MonoBehaviour
{
    public Sprite cutGrassSprite;
    public Sprite haySprite;
    public Sprite smallGrassSprite;
    public Sprite mediumGrassSprite;
    public Sprite tallGrassSprite;

    private Image grassImage;
    private bool isCut = false;
    private bool hayCreated = false;

    void Start()
    {
        grassImage = GetComponent<Image>();
    }

    public bool IsCut => isCut;
    public bool HasHay => hayCreated;

    public void Cut()
    {
        if (!isCut)
        {
            StopAllCoroutines();
            isCut = true;
            hayCreated = false;
            grassImage.sprite = cutGrassSprite;
            StartCoroutine(TransformToHay());
            Debug.Log("Grass cut!");
        }
    }

    private IEnumerator TransformToHay()
    {
        yield return new WaitForSeconds(5f);
        hayCreated = true;
        grassImage.sprite = haySprite;
        Debug.Log("Grass transformed to hay!");
    }

    public void CollectHay()
    {
        if (hayCreated)
        {
            StopAllCoroutines();
            hayCreated = false;
            StartCoroutine(GrassGrowProcess());
            Debug.Log("Hay collected!");
        }
    }

    private IEnumerator GrassGrowProcess()
    {
        grassImage.sprite = smallGrassSprite;
        yield return new WaitForSeconds(5f);

        if (isCut && !hayCreated) grassImage.sprite = mediumGrassSprite;
        else yield break;
        yield return new WaitForSeconds(5f);

        if (isCut && !hayCreated) grassImage.sprite = tallGrassSprite;
        else yield break;

        isCut = false;
        Debug.Log("Grass regrown!");
    }
}