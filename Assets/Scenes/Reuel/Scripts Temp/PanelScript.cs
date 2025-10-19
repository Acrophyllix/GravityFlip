using UnityEngine;
using UnityEngine.Audio;
using TMPro;
public class WinLose : MonoBehaviour
{

    //Text
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _totalTimeField;
    [SerializeField] private TextMeshProUGUI _livesLeftField;
    [SerializeField] private TextMeshProUGUI _fragmentsField;
    [SerializeField] private TextMeshProUGUI _scoreField;

    //Scoring
    [SerializeField] private int _totalTime;    
    [SerializeField] private int _livesLeftScore;
    [SerializeField] private int _fragmentsScore;
    
    //Timer
    [SerializeField] private float _timer;
    [SerializeField]private bool timerEnabled = true;

    //UI
    public GameObject loseLaser;
    public GameObject loseWire;
    public GameObject loseSpike;
    public GameObject loseTime;
    public GameObject WIN;
    public GameObject PanelBox;

    public int score;
    public int previousScoreLevel;
    public static int  nextScoreLevel;
    
    //PlayerScript
    [SerializeField] private PCReuel _playerScript;
    public DoorController doorScript;
    public bool isPlayerOnDoor;
    public string lastHitHazard;

    //SFX 
    [SerializeField] private AudioMixer mixer;

    public void Start()
    {   
        EnableSFX();
        Time.timeScale = 1f;
        previousScoreLevel = nextScoreLevel;
        loseLaser.SetActive(false);
        loseWire.SetActive(false);
        loseSpike.SetActive(false);
        loseTime.SetActive(false);
        WIN.SetActive(false);
       _timer = 120;
    }

    // Update is called once per frame
    void Update()
    {   
        isPlayerOnDoor = doorScript.playerIsOnDoor;
        lastHitHazard = _playerScript.lastHitHazard;

        if(_playerScript._playerLives<= 0){
            GameOver();
        }
        
        if(timerEnabled){
            if(_timer <= 0){
                lastHitHazard = "NoTime";
                GameOver();
            }else{

                _timer -= Time.deltaTime;
                _timerText.text = $"{(int)_timer}";
            }
        }

        if(isPlayerOnDoor){
            ExitLevel();
        }
    }
    public void GameOver()
    {
        
        if(lastHitHazard == "Spikers"){
            loseSpike.SetActive(true);
        }else if(lastHitHazard == "LiveWires"){
            loseWire.SetActive(true);
        }else if(lastHitHazard == "Laser"){
            loseLaser.SetActive(true);
        }else if(lastHitHazard == "NoTime"){
            loseTime.SetActive(true);
        }

        PanelBox.SetActive(true);


        //Accumulate the score
        _totalTime = (int)_playerScript.totalTime;
        int totalTimeScore = 120 - _totalTime / 5;
        _livesLeftScore = _playerScript.lives * 10;
        _fragmentsScore = _playerScript.fragmentsCollected * 2;
        score = totalTimeScore + _livesLeftScore + _fragmentsScore + previousScoreLevel;

        _totalTimeField.text = _totalTime.ToString();
        _livesLeftField.text = _playerScript.lives.ToString();
        _fragmentsField.text = _playerScript.fragmentsCollected.ToString() + " /15";
        _scoreField.text = score.ToString();
        Time.timeScale = 0f;
        DisableSFX();
    }




    public void ExitLevel()
    {
        WIN.SetActive(true);    
        //Move next level
        //Accumulate the score


        PanelBox.SetActive(true);


        _totalTime = (int)_playerScript.totalTime;
        int totalTimeScore = 120 - _totalTime / 5;
        _livesLeftScore = _playerScript.lives * 10;
        _fragmentsScore = _playerScript.fragmentsCollected * 2;
        score = totalTimeScore + _livesLeftScore + _fragmentsScore + previousScoreLevel;
        nextScoreLevel = score;

        _totalTimeField.text = _totalTime.ToString();
        _livesLeftField.text = _playerScript.lives.ToString();
        _fragmentsField.text = _playerScript.fragmentsCollected.ToString() + " /15";
        _scoreField.text = score.ToString();
        Time.timeScale = 0f;
        DisableSFX();
    }

    public void LoseLaser(){
        loseLaser.SetActive(true);
    }

    public void LoseWire(){
        loseWire.SetActive(true);
    }

    public void LoseSpike(){
        loseSpike.SetActive(true);
    }

    public void LoseTime(){
        loseTime.SetActive(true);
    }
    
    void DisableSFX(){
        mixer.SetFloat("SoundEffects", -80f);
    }
    void EnableSFX(){
        mixer.SetFloat("SoundEffects", 0f);
    }

    
}
