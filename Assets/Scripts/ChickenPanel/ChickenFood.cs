using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChickenFood : MonoBehaviour
{
    [Header("Chickens")]
    public ChickenManager chickenManager;

    [Header("Food")]
    public float foodAmount = 100f;
    public float maxFood = 100f;
    private float drainInterval = 5f;
    private float drainPerChicken = 1f; // nastav podľa seba

    [Header("Sprites")]
    public Sprite fullFoodSprite;
    public Sprite emptyFoodSprite;
    private Image foodImage;

    void Start()
    {
        foodImage = GetComponent<Image>();
        UpdateSprite();
        StartCoroutine(DrainFood());
    }

    public void SetFood(float amount)
    {
        foodAmount = Mathf.Clamp(amount, 0, maxFood);
        UpdateSprite();
    }

    public void AddFood(float amount) => SetFood(foodAmount + amount);
    public void DrainFoodAmount(float amount) => SetFood(foodAmount - amount);

    public float GetFood() => foodAmount;
    public bool IsEmpty() => foodAmount <= 0;

    private void UpdateSprite()
    {
        if (foodImage == null) return;
        foodImage.sprite = foodAmount <= 0 ? emptyFoodSprite : fullFoodSprite;
    }

    private IEnumerator DrainFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(drainInterval);

            int outsideChickens = chickenManager.GetOutsideChickenCount();
            DrainFoodAmount(outsideChickens * drainPerChicken);

           
        }
    }
}