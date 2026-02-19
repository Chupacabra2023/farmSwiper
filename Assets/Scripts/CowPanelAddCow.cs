using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CowPanelAddCow : MonoBehaviour
{
    
    public GameObject cowPrefab;
    public Button addCowButton;
    public List<GameObject> cows = new List<GameObject>();
    
    private Button addButton;

    void Start()
    {
        AddCowToGrid();
        SpawnAddButton();
    }

    void AddCowToGrid()
    {
        cows.Add(Instantiate(cowPrefab, this.transform));
    }

    void SpawnAddButton()
    {
        if (addButton == null)
        {
            addButton = Instantiate(addCowButton, this.transform) as Button;
            addButton.onClick.AddListener(OnAddCowClicked);
        }
        else
        {
            // presunúť tlačidlo na koniec
            addButton.transform.SetAsLastSibling();
        }
    }

    void OnAddCowClicked()
    {
        if (cows.Count < 6)
        {
            AddCowToGrid();
            SpawnAddButton(); // presunie button na koniec
        }

        if (cows.Count >= 6)
        {
            addButton.gameObject.SetActive(false); // skryť keď je max
        }
    }
}