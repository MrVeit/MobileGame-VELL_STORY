using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class InternetConnection : MonoBehaviour
{
    //Первый вариант проверки сети

    string m_ReachabilityText;



    //Второй вариант проверки сети

    private void Awake()
    {
        StartCoroutine(CheckInternetConnection((isConnected) => {
            Debug.Log(isConnected);                                 //старт корутины в любом методе на проверку подключения к сети.
        }));
    }

    private void Start()
    {
        InternetChecker();
    }

    IEnumerator CheckInternetConnection(Action<bool> action)
    {
        UnityWebRequest www = new UnityWebRequest("http://google.com");
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            action(false);
        }

        else
        {
            action(true);
        }

    }

    public void InternetChecker()
    {
        //Вывод информации о доступе к сети в консоль
        print("Internet : " + m_ReachabilityText);
        //проверка доступа к сети интернет на устройстве
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //Change the Text
            m_ReachabilityText = "Выход в сеть запрещён";
        }
        //Проверка возможности выхода в интернет через мобильную сеть
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            m_ReachabilityText = "Доступ к сети через передачу данных";
        }
        //Проверка выхода в сеть через проводное подключение на устройстве
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            m_ReachabilityText = "Доступ через локальную сеть.";
        }
    }
}
