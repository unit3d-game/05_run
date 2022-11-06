using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.Presets;

public class InitGame : MonoBehaviour
{
    private AsyncOperation operation;

    private RectTransform rectTransform;

    private RectTransform rectTransformByBG;

    private const float WidthOfPrecent = 98;

    private float widthOfPrecessing;

    private float precent = 10;

    private void Start()
    {
        rectTransform = UnityUtils.RequiredGetComponent<RectTransform>(transform, "Processing");
        rectTransformByBG = UnityUtils.RequiredGetComponent<RectTransform>(transform, "ProcessingBg");
        Debug.Log($"[{rectTransform.rect.width},{rectTransform.rect.height}][{rectTransformByBG.rect.width},{rectTransformByBG.rect.height}]");
        StartCoroutine(loadSceneAndData());
        widthOfPrecessing = WidthOfPrecent * rectTransformByBG.rect.width / 100;
        Vector3 pos = rectTransform.transform.localPosition;
        pos.x = -widthOfPrecessing / 2;
        rectTransform.transform.localPosition = pos;
        rectTransform.sizeDelta = new Vector2(0, rectTransform.rect.height);
    }


    private IEnumerator loadSceneAndData()
    {
        // 加载场景
        operation = SceneManager.LoadSceneAsync("Game");
        yield return operation;
    }



    private void Update()
    {
        precent = operation == null ? 10 : operation.progress * 100;
        renderPrecent();
    }


    private void renderPrecent()
    {
        precent = precent >= 100 ? 100 : precent;
        float width = precent * widthOfPrecessing / 100;
        rectTransform.sizeDelta = new Vector2(width, rectTransform.rect.height);
        Vector3 pos = rectTransform.transform.localPosition;
        pos.x = -(widthOfPrecessing - width) / 2;
        rectTransform.transform.localPosition = pos;
    }
}

