using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Transition : MonoBehaviour
{

    public float FadeDuration = 1f;
    private int _Scroll = Shader.PropertyToID("_Scroll");
    private int? _lastEffect;

    private Image _image;
    private Material _material;

    public void FadeIn()
    {
        StartFadeIn();
    }

    public void FadeOut()
    {
        StartFadeOut();
    }

    private void Awake()
    {
        _image = GetComponent<Image>();

        Material mat = _image.material;
        _image.material = new Material(mat);
        _material = _image.material;

        _lastEffect = _Scroll;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            FadeIn();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            FadeOut();
        }
    }

    private void StartFadeIn()
    {
        _material.SetFloat("_Scroll", 1.2f);

        StartCoroutine(HandleFade(8.2f, 1.2f));
    }

    private void StartFadeOut()
    {
        _material.SetFloat("_Scroll", 8.2f);

        StartCoroutine(HandleFade(1.2f, 8.2f));
    }
    private IEnumerator HandleFade(float targetAmount, float startAmount)
    {
        float elapsedTime = 0f;
        while (elapsedTime < FadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float lerpedAmount = Mathf.Lerp(startAmount, targetAmount, (elapsedTime / FadeDuration));
            _material.SetFloat("_Scroll", lerpedAmount);

            yield return null;
        }
    }
}
