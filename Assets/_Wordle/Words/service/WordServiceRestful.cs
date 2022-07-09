using System.Net.Sockets;
using UnityEngine;

public class WordServiceRestful :  WordService
{

    private const string IS_A_WORD_URL = "http://localhost:8080/api/v1/words/isAWord/";

    private const string RANDOM_WORD = "http://localhost:8080/api/v1/words/random/";

    private WordServiceRestful() {
    }
    private static WordServiceRestful instance = null;
    public static WordServiceRestful Instance {
        get {
            if (instance == null) {
                instance = new WordServiceRestful();
            }
            return instance;
        }
    }

    public bool isAWord(string word) {
        string result;
        try {
            result = RESTConnection.Instance.callGetUrlRequest(IS_A_WORD_URL + word);
        } catch(SocketException exception) {
            Debug.LogError(exception.Message);
            result = "false";
        }
        return result.Equals("true");
    }

    public string randomWord(int wordLength) {
        string result ;
        try {
            result = RESTConnection.Instance.callGetUrlRequest(RANDOM_WORD + wordLength);
        } catch (SocketException exception) {
            Debug.LogError(exception.Message);
            result = "error";
        }
        return result;
    }

}
