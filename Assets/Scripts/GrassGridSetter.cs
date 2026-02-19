using System.Collections.Generic;
using UnityEngine;

public class GrassGridSetter : MonoBehaviour
{
    public GameObject grassPrefab;
    private List<GameObject> grassTiles = new List<GameObject>();
   void Start()
{
    int total = 0;
    while(total < 15)
    {
        if(total == 7)
        {
            var empty = new GameObject("WellSlot", typeof(RectTransform));
    empty.transform.SetParent(this.transform, false);
        }
        else
        {
            grassTiles.Add(Instantiate(grassPrefab, this.transform));
        }
        total++;
    }
}
}
