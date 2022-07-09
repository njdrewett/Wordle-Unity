using System.Net;
using System;
using System.IO;

public class RESTConnection {

    private static RESTConnection _instance;

    public static RESTConnection Instance {
        get {
            if (_instance == null) {
                _instance = new RESTConnection();
            }
            return _instance;
        }
        set {
            _instance = value;
        }
    }


    public string callGetUrlRequest(string url) {

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        return reader.ReadToEnd();
    }

 }
