using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EggsNest : MonoBehaviour, IPointerClickHandler
{
    [Header("Sprites")]
    public Sprite emptySprite;
    public Sprite fullSprite;

    [Header("References")]
    public ChickenManager chickenManager;
    public ChickenFood chickenFood;
    public WaterBucket waterBucket;

    [Header("Settings")]
  
    private float eggIntervalSeconds;
    private int eggCount = 0;

    private Image nestImage;

    void Awake()
    {
        nestImage = GetComponent<Image>();
    }

    void Start()
    {
        eggIntervalSeconds = Random.Range(1f, 10);
        UpdateSprite();
        StartCoroutine(GenerateEggs());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eggCount == 0) return;

        Debug.Log($"Zebralas {eggCount} vajicok!");
        eggCount = 0;
        UpdateSprite();
    }

    public void AddEgg()
    {
        eggCount++;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (nestImage == null) return;
        nestImage.sprite = eggCount > 0 ? fullSprite : emptySprite;
    }

  
    private IEnumerator GenerateEggs()
    {
        while (true)
        {
            yield return new WaitForSeconds(eggIntervalSeconds);
            eggIntervalSeconds = Random.Range(1f, 10);

            bool hasFood = chickenFood != null && !chickenFood.IsEmpty();
            bool hasWater = waterBucket != null && !waterBucket.IsEmpty();
            int freeChickens = chickenManager != null ? chickenManager.GetAdultOutsideChickenCount() : 0;

            if (hasFood && hasWater && freeChickens > 0)
            {
                for (int i = 0; i < freeChickens; i++)
                    AddEgg();

                Debug.Log($"{freeChickens} kuriek znieslo vajicka!");
            }
        }
    }
}
