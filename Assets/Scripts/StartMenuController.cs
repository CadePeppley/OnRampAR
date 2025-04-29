using UnityEngine;
using System.Collections;

public class SideMenuController : MonoBehaviour
{
    [Header("Drag your SideMenu Panel here")]
    public RectTransform menuPanel;
    [Header("How long the slide animation takes (in seconds)")]
    public float animationDuration = 0.25f;

    private Vector2 closedPos;
    private Vector2 openPos;
    private bool isOpen = false;

    void Start()
    {
        // Record the start (closed) position
        closedPos = menuPanel.anchoredPosition;
        // Compute the open position (x=0)
        openPos = new Vector2(0, closedPos.y);
    }

    // Hook this up to your Gear button’s OnClick
    public void ToggleMenu()
    {
        StopAllCoroutines();
        StartCoroutine(Slide(menuPanel.anchoredPosition, isOpen ? closedPos : openPos));
        isOpen = !isOpen;
    }

    private IEnumerator Slide(Vector2 from, Vector2 to)
    {
        float elapsed = 0f;
        while (elapsed < animationDuration)
        {
            menuPanel.anchoredPosition = Vector2.Lerp(from, to, elapsed / animationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        menuPanel.anchoredPosition = to;
    }
}
