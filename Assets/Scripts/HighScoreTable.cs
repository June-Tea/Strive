/*
This script manages the score board and keeps track of which players did the best by ranking them 
based on their progress and time. This script also stores the data from the scoreboard so it persists
between playsessions 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScoreTable : MonoBehaviour
{
    //reference UI components
    private Transform container;
    private Transform template;
    private List<Transform> transformList = new List<Transform>();
    [SerializeField] private Prompt prompt;
    public bool done = false; 

    //
    private void Awake() {
        //get our UI references
        container = transform.Find("ScoreContainer");
        template = container.Find("ScoreTemplate");
        //creates an empty Highscore object so it can be saved later
        Highscores hs = new Highscores {hEList = new List<HighscoreEntry>()};
        //makes the template inactive so it doesn't mess with the board
        template.gameObject.SetActive(false);

        //checks if there is already a saved table and makes one if there isn't
        if (PlayerPrefs.GetString("highscoreTable") == "")
        {
            string json = JsonUtility.ToJson(hs);
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();
        }
        //calls the AddEntry method after a bit to make sure it doesn't come into contact with the prompt's own awake function
        Invoke("AddEntry",0.1f);
    }
    //checks if the AddEntry is done and if the player presses the space, return or enter key then loads to first scene
    private void Update() {
        if (done && (Input.GetKeyDown(KeyCode.Return) | Input.GetKeyDown(KeyCode.KeypadEnter) | Input.GetKeyDown(KeyCode.Space)))
        {
            SceneManager.LoadScene("StartScreen");
        }
    }
    //creates an entry for each score in the list and calls CreateHighscoreEntry to visualize them
    private void printScores(){
        transformList.Clear();
        transformList = new List<Transform>();
        string jString = PlayerPrefs.GetString("highscoreTable");
        Highscores hs = JsonUtility.FromJson<Highscores>(jString);
        foreach (HighscoreEntry hE in hs.hEList)
        {
            CreateHighscoreEntry(hE, container,transformList);
        }
    }
    // this method creates a visual representation of the players' scores and displays them by referencing a UI template of transforms
    private void CreateHighscoreEntry(HighscoreEntry hE, Transform container, List<Transform> transformList) {
        float height = 24f; 
        Transform entryT = Instantiate(template,container);
        RectTransform entryRect = entryT.GetComponent<RectTransform>();
        entryRect.anchoredPosition = new Vector2(0, -height * transformList.Count);
        entryT.gameObject.SetActive(true);

        //orders the score by rank, as in 1st, 2nd, 3rd, 4th, etc.
        int rank = transformList.Count + 1;
        string rankS;

        switch (rank)
        {
            default: rankS = rank + "th"; break;
            case 1: rankS = "1st"; break;
            case 2: rankS = "2nd"; break;
            case 3: rankS = "3rd"; break;
        
        }
        //checks if player completed the game
        float distance = hE.distance;
        string d;
        if (distance == 7777777)
        {
            d = "Finished!";
        }
        else
        {
            d = string.Format("{0:N}{1:##}",distance,"ft");
        }
        string t = formatTime(hE.time);
        //writes the variables down in their appropriate text fields
        entryT.Find("pos").GetComponent<TMP_Text>().text = rankS+".";
        entryT.Find("player").GetComponent<TMP_Text>().text = hE.name;
        entryT.Find("distance").GetComponent<TMP_Text>().text = d;
        entryT.Find("time").GetComponent<TMP_Text>().text = t;
        //adds the entry to a list of transforms thereby adding it to the scene
        transformList.Add(entryT);

    }
    //formats the score to better resemble common perceptions of what time looks like
    public string formatTime(float time)
    {
        int mins = (int) time/60000;
        int secs = (int) time/ 1000-60 * mins;
        return string.Format("{0:00}:{1:00}",mins,secs);
    }
    //checks if a player's scores were good enough for the scoreboard and adds them if so
    private void AddEntry() {
        //get the distance and time values from the static ScoreHolder class
        float d = ScoreHolder.playerDistance;
        float t = ScoreHolder.playerTime;
        HighscoreEntry he;
        //Load saved scores
        string jString = PlayerPrefs.GetString("highscoreTable");
        Highscores hs = JsonUtility.FromJson<Highscores>(jString);
        bool valid = true;
        //if there's more than one entry in the list, check if the player's score is better than the minimum
        if (hs.hEList.Count > 0)
        {
            float pScore = d/t;
            float minScore = hs.hEList[hs.hEList.Count - 1].distance/hs.hEList[hs.hEList.Count - 1].time;
            valid = pScore > minScore;
        }
        //If the player's score was better or if there's still space in the list, add the entry
        if ( valid || hs.hEList.Count < 20)
        {
            //turns on prompt window to get a name string from the player
            prompt.Show("Name?", "...",(string input) =>{
                //Create entry 
                he = new HighscoreEntry{distance = d, time = t, name = input};
    
                //Add new score
                hs.hEList.Add(he);
        
                if (hs.hEList.Count > 2)
                {
                    //Sort list by distance and time
                    for (int i = 0; i < hs.hEList.Count; i++)
                    {
                        for (int j = i +1; j < hs.hEList.Count; j++)
                        {
                            float jscore = (hs.hEList[j].distance) / hs.hEList[j].time;
                            float iscore = (hs.hEList[i].distance) / hs.hEList[i].time;
                            if (jscore > iscore)
                            {
                                //swap
                                HighscoreEntry temp = hs.hEList[i];
                                hs.hEList[i] = hs.hEList[j];
                                hs.hEList[j] = temp;
                            }
                        }
                    }
                }
                //prune list
                while (hs.hEList.Count > 20)
                {
                    hs.hEList.RemoveAt(hs.hEList.Count-1);
                }
        
                //Save updated scores
                string json = JsonUtility.ToJson(hs);
                PlayerPrefs.SetString("highscoreTable", json);
                PlayerPrefs.Save();
                //print scores
                printScores();
                done = true;
            });
        }
        else{
            //if the score wasn't good enought simply print the current board and allow the user to navigate back to the start scene
            printScores();
            done = true;
        }
    }
    //An object to save our scores in a JSON file
    private class Highscores {
        public List<HighscoreEntry> hEList;
    }
    //A single entry on the board
    [System.Serializable]
    private class HighscoreEntry {
        public float distance;
        public float time;
        public string name;
    }
}
