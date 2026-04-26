using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class RequesterTime : MonoBehaviour
{
    private const string YandexTimeApiUrl = "https://yandex.com/time/sync.json";

    public void GetServerTime(Action<DateTime> OnComplete)
    {
        StartCoroutine(GetTimeCoroutine(OnComplete));
    }

    private IEnumerator GetTimeCoroutine(Action<DateTime> OnComplete)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(YandexTimeApiUrl))
        {
            request.timeout = 5;
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                var timeData = JsonUtility.FromJson<YandexTimeResponse>(json);
                DateTime epochTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime serverTime = epochTime.AddMilliseconds(timeData.time).ToLocalTime();
                OnComplete?.Invoke(serverTime);
            }
            else
            {
                Debug.LogError($"Ошибка: {request.error}");
                OnComplete?.Invoke(DateTime.Now);
            }
        }
    }

    [Serializable]
    private class YandexTimeResponse
    {
        public long time;
    }
}
