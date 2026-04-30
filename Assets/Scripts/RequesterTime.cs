using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class RequesterTime : MonoBehaviour
{
    [Serializable]
    private class YandexTimeResponse
    {
        public long time;
    }

    public void GetServerTime(Action<DateTime> onComplete)
    {
        StartCoroutine(GetTimeCoroutine(onComplete));
    }

    private IEnumerator GetTimeCoroutine(Action<DateTime> onComplete)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(ClockConstants.TIME_API_URL))
        {
            request.timeout = ClockConstants.REQUEST_TIMEOUT_SECONDS;
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var timeData = JsonUtility.FromJson<YandexTimeResponse>(request.downloadHandler.text);
                DateTime epochTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime serverTime = epochTime.AddMilliseconds(timeData.time).ToLocalTime();
                onComplete?.Invoke(serverTime);
                yield break;
            }

            Debug.LogError($"Ошибка запроса времени: {request.error}");
            onComplete?.Invoke(DateTime.Now);
        }
    }
}