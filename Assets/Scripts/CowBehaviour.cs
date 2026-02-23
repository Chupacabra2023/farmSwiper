using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CowBehaviour : MonoBehaviour
{
    public Sprite cowSprite;
    public Sprite dirtyCowSprite;
    private bool isDirty = false;
    private Image currentImage;

    void Start()
    {
        currentImage = GetComponent<Image>();
        currentImage.sprite = cowSprite;
        StartCoroutine(DirtyCycle());
    }

    IEnumerator DirtyCycle()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            isDirty = !isDirty;
            currentImage.sprite = isDirty ? dirtyCowSprite : cowSprite;
        }
    }

    public void Clean()
    {
        isDirty = false;
        currentImage.sprite = cowSprite;
    }
}