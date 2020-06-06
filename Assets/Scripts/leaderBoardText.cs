using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class leaderBoardText : MonoBehaviour
{
    public Text texto;
    private List<PlayerInfo> collectedStats;
    private bool clear=false;
    // Start is called before the first frame update
    void Start()
    {
        collectedStats = new List<PlayerInfo>();
        texto.text = LoadLeaderBoard();
    }

    // Update is called once per frame
    void Update()
    {
        //  texto.text = PlayerPrefs.GetString("LeaderBoards");
        /* if (clear)
         {
             ClearPrefs();
             clear = false;
             UpdateLeaderBoard();
         }*/
    }

    public string LoadLeaderBoard() {
        texto.text = PlayerPrefs.GetString("LeaderBoards");
        string[] stats2 = texto.text.Split(',');
        for (int i = 0; i < stats2.Length - 2; i += 2)
        {
            //Use The Collected Information To Create An Object
            PlayerInfo loadedInfo = new PlayerInfo(stats2[i], int.Parse(stats2[i + 1]));

            //Add The Object To The List
            collectedStats.Add(loadedInfo);

        }

        //Clear Current Displayed LeaderBoard
        texto.text = "";

        //Simply Loop Through The List And Add The Name And Score To The Display Text
        for (int i = 0; i <= collectedStats.Count - 1; i++)
        {
            texto.text += collectedStats[i].name + " : " + collectedStats[i].score + "\n";
        }

        return texto.text;
    }

    public void UpdateLeaderBoard() {
        //Clear Current Displayed LeaderBoard
        texto.text = PlayerPrefs.GetString("LeaderBoards");
        texto.text = "";

        //Simply Loop Through The List And Add The Name And Score To The Display Text
        for (int i = 0; i <= collectedStats.Count - 1; i++)
        {
            texto.text += collectedStats[i].name + " : " + collectedStats[i].score + "\n";
        }
    }

    public void ClearPrefs()
    {
        //Use This To Delete All Names And Scores From The LeaderBoard
        PlayerPrefs.DeleteAll();
    
        //Clear Current Displayed LeaderBoard
        texto.text = "";
    }
    public void ClearTrue() {
        this.clear = true;
    }
}
