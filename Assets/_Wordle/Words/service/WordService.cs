using System.Net.Sockets;
using UnityEngine;

public interface WordService 
{
    bool isAWord(string word);

    string randomWord(int wordLength);

}
