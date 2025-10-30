using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public int nextLevelInt;
    public int thisLevelInt;
    private string lvl = "Level ";
    private string nextLevel = "";
    private string thisLevel = "";

    void Start(){

        nextLevel = lvl + nextLevelInt;
        thisLevel = lvl + thisLevelInt;
    }
    public void Restart(){

        if(thisLevelInt > 5 || thisLevelInt < 0){
            SceneManager.LoadScene("Tutorial2");    
            Time.timeScale = 1f;
        }else{
        SceneManager.LoadScene(thisLevel);
        Time.timeScale = 1f;
        }
    }
    
    public void NextLevel(){
        SceneManager.LoadScene(nextLevel);
        Time.timeScale = 1f;
    }

    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause(){
        Time.timeScale = 0f;
    }
}

