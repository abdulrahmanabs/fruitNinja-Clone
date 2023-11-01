using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class PopupText : MonoBehaviour
{
    [Header("UI Settings")]
    public TMP_Text textPrefab;
    public Transform popupParent;
    public float displayDuration = 2f;
    public float moveSpeed = 1.0f;

    // Singleton instance
    public static PopupText Instance { get; private set; }

    private List<TMP_Text> activePopups = new List<TMP_Text>();
    private Queue<TMP_Text> popupPool = new Queue<TMP_Text>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowPopup(Vector3 worldPosition, string message, Color textColor)
    {
        TMP_Text popup = GetPooledPopup();
        SetupPopup(popup, worldPosition, message, textColor);
        activePopups.Add(popup);
        StartCoroutine(FadeOutAndMoveUp(popup));
    }

    private TMP_Text GetPooledPopup()
    {
        if (popupPool.Count > 0)
        {
            return popupPool.Dequeue();
        }
        else
        {
            return Instantiate(textPrefab, Vector3.zero, Quaternion.identity);
        }
    }

    private void SetupPopup(TMP_Text popup, Vector3 worldPosition, string message, Color textColor)
    {
        popup.transform.SetParent(popupParent);
        popup.transform.position =new Vector3(worldPosition.x,worldPosition.y,-1.5f) ;
        //popup.transform.position=worldPosition;
        popup.text = message;
        popup.color = textColor;

        popup.gameObject.SetActive(true); // Ensure it's active
    }

    private IEnumerator FadeOutAndMoveUp(TMP_Text popup)
    {
        float startTime = Time.time;
        float initialAlpha = popup.color.a;
        Vector3 initialPosition = popup.transform.position;

        while (Time.time - startTime < displayDuration)
        {
            float elapsedTime = Time.time - startTime;
            float newAlpha = Mathf.Lerp(initialAlpha, 0f, elapsedTime / displayDuration);
            SetPopupAlpha(popup, newAlpha);

            // Move the popup upwards
            popup.transform.position = initialPosition + Vector3.up * moveSpeed * elapsedTime;
            yield return null;
        }

        FinishPopup(popup);
    }

    private void SetPopupAlpha(TMP_Text popup, float alpha)
    {
        Color newColor = popup.color;
        newColor.a = alpha;
        popup.color = newColor;
    }

    private void FinishPopup(TMP_Text popup)
    {
        activePopups.Remove(popup);
        popupPool.Enqueue(popup);
        popup.gameObject.SetActive(false); // Deactivate the text object
    }
}
