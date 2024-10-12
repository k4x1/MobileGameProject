using UnityEngine;

public class SafeAreaAdjuster : MonoBehaviour
{
    public enum Corner { TopLeft, TopRight }

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float offsetX = 20f;
    [SerializeField] private float offsetY = 20f;
    [SerializeField] private Corner corner = Corner.TopLeft;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        AdjustToSafeArea(corner);
    }

    private void AdjustToSafeArea(Corner corner)
    {
        Rect safeArea = Screen.safeArea;

        switch (corner)
        {
            case Corner.TopLeft:
                SetPositionTopLeft(safeArea);
                break;
            case Corner.TopRight:
                SetPositionTopRight(safeArea);
                break;
        }
    }
    // I kinda hate this but it works
    private void SetPositionTopLeft(Rect safeArea)
    {
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1);

        float xPosition = safeArea.xMin + offsetX;
        float yPosition = -(Screen.height - safeArea.yMax) - offsetY;

        rectTransform.anchoredPosition = new Vector2(xPosition, yPosition);
    }

    private void SetPositionTopRight(Rect safeArea)
    {
        rectTransform.anchorMin = new Vector2(1, 1);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.pivot = new Vector2(1, 1);

        float xPosition = -(Screen.width - safeArea.xMax) - offsetX;
        float yPosition = -(Screen.height - safeArea.yMax) - offsetY;

        rectTransform.anchoredPosition = new Vector2(xPosition, yPosition);
    }
}
