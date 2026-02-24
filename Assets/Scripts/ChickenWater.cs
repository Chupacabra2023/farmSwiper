using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBucket : MonoBehaviour
{
    [Header("Chickens")]
    public GameObject chickenPrefab;
    public Transform chickenParent;
    public List<GameObject> chickens = new List<GameObject>();
    public Button addChickenButton;

    [Header("Water")]
    public float waterAmount = 100f;
    public float maxWater = 100f;
    private float drainInterval = 1f;
    private float drainPerChicken = 50f;

    [Header("Sprites")]
    public Sprite fullBucketSprite;
    public Sprite emptyBucketSprite;
    private Image bucketImage;

    void Start()
    {
        bucketImage = GetComponent<Image>();
        UpdateSprite();
        StartCoroutine(DrainWater());
    }

    // ---- CHICKEN MANAGER ----
    public void AddChicken()
    {
        if (chickens.Count >= 9) return;

        GameObject newChicken = Instantiate(chickenPrefab, chickenParent);
        chickens.Add(newChicken);

        if (chickens.Count >= 9)
            addChickenButton.interactable = false;
    }

    public int GetChickenCount() => chickens.Count;

    public int GetOutsideChickenCount()
    {
        return chickens.FindAll(c =>
            c != null &&
            !c.GetComponent<ChickenMovement>().isSitting
        ).Count;
    }

    // ---- WATER ----
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

            int outsideChickens = GetOutsideChickenCount();
            DrainWaterAmount(outsideChickens * drainPerChicken);

            Debug.Log($"Voda: {waterAmount} | Kurky vonku: {outsideChickens}");
        }
    }
}