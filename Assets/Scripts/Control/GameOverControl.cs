using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverControl : MonoBehaviour
{
    private AsyncOperation operation;

    private IEnumerator loadSceneAndData()
    {
        // 加载场景
        operation = SceneManager.LoadSceneAsync("Game");
        yield return operation;
    }

    private void Start()
    {
        AudioManager.Play("Boss死了");
    }

    public void Restart()
    {
        UserStorage.Restart();
        StartCoroutine(loadSceneAndData());
    }
}

