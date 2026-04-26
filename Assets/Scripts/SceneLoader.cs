using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private AssetReference _sceneReference;
    [SerializeField] private GameObject _loadingScreen;

    private void Start()
    {
        LoadScene();
    }

    public void LoadScene()
    {
        if (_loadingScreen != null)
            _loadingScreen.SetActive(true);

        Debug.Log("Начинаем загрузку сцены");

        Addressables.LoadSceneAsync(_sceneReference, LoadSceneMode.Single).Completed += OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        if (_loadingScreen != null)
            _loadingScreen.SetActive(false);

        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Сцена успешно загружена через Addressables");
        }
        else
        {
            Debug.LogError("Ошибка загрузки сцены");
        }
    }
}
