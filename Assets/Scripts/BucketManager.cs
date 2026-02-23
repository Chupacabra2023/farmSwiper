using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;


public class BucketManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public static event System.Action OnBucketFilled;
   public static event System.Action OnBucketEmptied;

    public static void FillAllBuckets()
    {
        OnBucketFilled?.Invoke();
    }

    public static void EmptyAllBuckets()
    {
        OnBucketEmptied?.Invoke();
    }


}






