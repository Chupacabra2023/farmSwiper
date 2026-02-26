using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenManager : MonoBehaviour
{
    public GameObject chickenPrefab;
    public Transform chickenParent;
    public List<GameObject> chickens = new List<GameObject>();
    public Button addChickenButton;

    public void AddChicken()
{
    if (chickens.Count >= 9) return;

    GameObject newChicken = Instantiate(chickenPrefab, chickenParent);
    chickens.Add(newChicken);

    if (chickens.Count >= 9)
        addChickenButton.interactable = false;
}

    public int GetChickenCount()
    {
        return chickens.Count;
    }

    public int GetOutsideChickenCount()
    {
        return chickens.FindAll(c =>
            c != null &&
            !c.GetComponent<ChickenMovement>().isSitting
        ).Count;
    }

    public int GetAdultOutsideChickenCount()
    {
        return chickens.FindAll(c =>
            c != null &&
            c.GetComponent<ChickenMovement>() is ChickenMovement m &&
            !m.isSitting &&
            m.IsAdult
        ).Count;
    }
}