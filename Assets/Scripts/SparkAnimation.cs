using UnityEngine;
using UnityEngine.UI;

public class SparkAnimation : MonoBehaviour
{
    public Sprite[] frames;
    public float fps = 12f;
    
    private Image image;
    private int currentFrame;
    private float timer;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f / fps)
        {
            timer = 0;
            currentFrame = (currentFrame + 1) % frames.Length;
            image.sprite = frames[currentFrame];
        }
    }
}