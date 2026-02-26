using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBucket : MonoBehaviour
{
    [Header("Chickens")]

    public ChickenManager chickenManager;

    [Header("Water")]
    public float waterAmount = 100f;
    public float maxWater = 100f;
    private float drainInterval = 5f;
    private float drainPerChicken = 1f;

    [Header("Sprites")]
    public Sprite fullBucketSprite;
    public Sprite emptyBucketSprite;
    private Image bucketImage;

    void Start()
{
    bucketImage = GetComponent<Image>();
    Debug.Log($"bucketImage: {bucketImage}");
    Debug.Log($"waterAmount: {waterAmount}");
    UpdateSprite();
    StartCoroutine(DrainWater());
}

    public void SetWater(float amount)
    {
        waterAmount = Mathf.Clamp(amount, 0, maxWater);
        UpdateSprite();
    }

    public void AddWater(float amount)
    {
        SetWater(waterAmount + amount);
    }

    public void DrainWaterAmount(float amount)
    {
        SetWater(waterAmount - amount);
    }

    public float GetWater() => waterAmount;
    public bool IsEmpty() => waterAmount <= 0;

    private void UpdateSprite()
    {
        if (bucketImage == null) return;
        bucketImage.sprite = waterAmount <= 0 ? emptyBucketSprite : fullBucketSprite;
    }

    private IEnumerator DrainWater()
    {
        while (true)
        {
            yield return new WaitForSeconds(drainInterval);

            int outsideChickens = chickenManager.GetOutsideChickenCount();
            DrainWaterAmount(outsideChickens * drainPerChicken);

           
        }
    }
}