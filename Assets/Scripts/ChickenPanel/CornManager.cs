using UnityEngine;

public class CornManager : MonoBehaviour
{
    public static event System.Action OnCornFilled;
    public static event System.Action OnCornEmptied;

    public static void FillAllCorn()
    {
        OnCornFilled?.Invoke();
    }

    public static void EmptyAllCorn()
    {
        OnCornEmptied?.Invoke();
    }
}