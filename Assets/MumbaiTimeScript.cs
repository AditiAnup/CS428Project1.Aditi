using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Networking;

public class MumbaiTimeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject timeTextObject1;
    string url = "https://worldtimeapi.org/api/timezone/Asia/Kolkata";

    void Start()
    {
    InvokeRepeating("UpdateTime", 0f, 1f);   
    }

   
    void UpdateTime()
    {
        StartCoroutine(getRequest(url));
    }

    IEnumerator getRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                // print out the weather data to make sure it makes sense
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                int dateTime = webRequest.downloadHandler.text.IndexOf("datetime",0);
                int startTime = webRequest.downloadHandler.text.IndexOf("T",dateTime);
                int endTime = startTime + 8;
                string time = webRequest.downloadHandler.text.Substring(startTime+1,8);
                string suffix = "AM";
                if ((Int32.Parse(time.Substring(0,2)) > 12))
                {
                    int new_hour = Int32.Parse(time.Substring(0,2))-12;
                    time = new_hour.ToString()+time.Substring(2);
                    suffix = "PM";
                }
                Debug.Log("Time: "+time);
                timeTextObject1.GetComponent<TextMeshPro>().text = "" + time +" "+ suffix;
            }
        }
    }
}
