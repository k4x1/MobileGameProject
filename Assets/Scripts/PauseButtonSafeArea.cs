using UnityEngine;

public class SafeAreaAdjuster : MonoBehaviour
{
    [SerializeField] private RectTransform pauseButton;
    [SerializeField] private float offsetX = 20f;
    [SerializeField] private float offsetY = 20f;

    private void Start()
    {
        AdjustToSafeArea();
    }

    private void AdjustToSafeArea()
    {
        Rect safeArea = Screen.safeArea;

        pauseButton.anchorMin = new Vector2(0, 1);

        pauseButton.anchorMax = new Vector2(0, 1);

        pauseButton.pivot = new Vector2(0, 1);

        float xPosition = safeArea.xMin + offsetX;

        float yPosition = -(Screen.height - safeArea.yMax) - offsetY;

        pauseButton.anchoredPosition = new Vector2(xPosition, yPosition);

        pauseButton.sizeDelta = new Vector2(pauseButton.sizeDelta.x, pauseButton.sizeDelta.y);
    }
}
