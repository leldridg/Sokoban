using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator animator;
    public static GameManager Instance;
    public List<Basket> baskets;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }
    }

    void ResetLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadLevel(nextSceneIndex));
        }
    }

    IEnumerator LoadLevel(int level)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(level);
    }

    public void CheckForNextLevel()
    {
        if (baskets.TrueForAll(basket => basket.fruit.enabled))
        {
            LoadNextLevel();
        }
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(0));
    }
}
