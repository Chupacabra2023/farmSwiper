using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SharpenerBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float sharpenDuration = 5f;
    public GameObject sparks;

    private ScrollRect scrollRect;
    private Vector3 originalPosition;
    private float timeOverScythe = 0f;
    private ScytheBehaviour currentScythe = null;

    IEnumerator Start()
    {
        yield return null;
        scrollRect = FindAnyObjectByType<ScrollRect>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
        scrollRect.enabled = false;
        timeOverScythe = 0f;
        currentScythe = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        ScytheBehaviour foundScythe = null;
        foreach (var result in results)
        {
            var scythe = result.gameObject.GetComponent<ScytheBehaviour>();
            if (scythe != null)
            {
                foundScythe = scythe;
                break;
            }
        }

        if (foundScythe != null)
        {
            if (currentScythe == foundScythe)
            {
                // pohybuj iskrami spolu s prstom
                if (sparks != null)
                {
                    sparks.transform.position = eventData.position + new Vector2(-30, 0);
                    sparks.SetActive(eventData.delta.magnitude > 3f);
                }

                timeOverScythe += Time.deltaTime;
                Debug.Log($"Sharpening... {timeOverScythe:F1}s / {sharpenDuration}s");

                if (timeOverScythe >= sharpenDuration)
                {
                    foundScythe.Sharpen();
                    timeOverScythe = 0f;
                    StopSparks();
                    Debug.Log("Scythe sharpened!");
                }
            }
            else
            {
                currentScythe = foundScythe;
                timeOverScythe = 0f;
            }
        }
        else
        {
            StopSparks();
            currentScythe = null;
            timeOverScythe = 0f;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        scrollRect.enabled = true;
        transform.position = originalPosition;
        timeOverScythe = 0f;
        currentScythe = null;
        StopSparks();
    }

    private void StopSparks()
    {
        if (sparks != null)
            sparks.SetActive(false);
    }
}
