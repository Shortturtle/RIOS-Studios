using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseFracture : MonoBehaviour
{
    [Header("UI References")]
    public GameObject PauseScreen;       // Translucent black pause overlay
    public Transform shardParent;        // Empty UI object for shards

    [Header("Shard Settings")]
    public GameObject shardPrefab;       // UI Image prefab
    public int rows = 2;
    public int columns = 3;
    public float moveDistance = 120f;
    public float moveDuration = 0.4f;

    [Header("Rotation Settings")]
    public float maxRotation = 30f;
    public float randomRotationOffset = 15f;

    private bool isPaused = false;

    private void Awake()
    {
        // Ensure shards render above PauseScreen using a Canvas override
        Canvas shardCanvas = shardParent.GetComponent<Canvas>();
        if (shardCanvas == null)
        {
            shardCanvas = shardParent.gameObject.AddComponent<Canvas>();
        }
        shardCanvas.overrideSorting = true;
        shardCanvas.sortingOrder = 2; // Always on top
    }

    public void TogglePause()
    {
        if (!isPaused)
            StartCoroutine(CaptureAndFracture());
        else
            UnpauseGame();
    }

    IEnumerator CaptureAndFracture()
    {
        // Make sure pause screen is hidden for screenshot
        PauseScreen.SetActive(false);

        // Wait until end of frame for UI to render
        yield return new WaitForEndOfFrame();

        // Take screenshot of current screen
        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenTexture.Apply();

        // Pause the game
        Time.timeScale = 0f;
        isPaused = true;

        // Create shards from screenshot
        CreateShards(screenTexture);

        // Now show the pause screen behind shards
        PauseScreen.SetActive(true);
    }

    void CreateShards(Texture2D texture)
    {
        float shardWidth = texture.width / columns;
        float shardHeight = texture.height / rows;

        float centerX = (columns - 1) / 2f;
        float centerY = (rows - 1) / 2f;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                // Instantiate shard prefab
                GameObject shard = Instantiate(shardPrefab, shardParent);

                RectTransform rt = shard.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(shardWidth, shardHeight);
                rt.anchoredPosition = new Vector2(
                    (-Screen.width / 2f) + shardWidth * x + shardWidth / 2f,
                    (-Screen.height / 2f) + shardHeight * y + shardHeight / 2f
                );

                // Assign screenshot to the child image
                Image screenshotImg = shard.transform.GetChild(0).GetComponent<Image>();
                screenshotImg.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                                                     new Vector2(0.5f, 0.5f));
                screenshotImg.rectTransform.sizeDelta = new Vector2(texture.width, texture.height);

                // **Offset the screenshot inside the shard so only the correct portion shows**
                float offsetX = -rt.anchoredPosition.x;
                float offsetY = -rt.anchoredPosition.y;
                screenshotImg.rectTransform.anchoredPosition = new Vector2(offsetX, offsetY);

                // Calculate direction from center
                Vector2 dir = new Vector2(x - centerX, y - centerY).normalized;

                // Randomized rotation per shard
                float baseRotation = dir.x * maxRotation;
                float randomOffset = Random.Range(-randomRotationOffset, randomRotationOffset);
                float shardRotation = baseRotation + randomOffset;

                // Animate shard
                StartCoroutine(AnimateShard(rt, dir, shardRotation));
            }
        }
    }

    IEnumerator AnimateShard(RectTransform shard, Vector2 direction, float targetRotation)
    {
        Vector2 startPos = shard.anchoredPosition;
        Vector2 targetPos = startPos + direction * moveDistance;

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / moveDuration;

            float ease = 1f - Mathf.Pow(1f - t, 3f); // ease out cubic

            shard.anchoredPosition = Vector2.Lerp(startPos, targetPos, ease);
            shard.localRotation = Quaternion.Euler(0, 0, targetRotation * ease);

            yield return null;
        }
    }

    public void UnpauseGame()
    {
        // Destroy all shards
        foreach (Transform child in shardParent)
        {
            Destroy(child.gameObject);
        }

        PauseScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}