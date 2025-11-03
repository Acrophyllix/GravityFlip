using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class LevelMenu : MonoBehaviour
{


	public Button[] buttons;
	
	public int childCount;
	private int unlockedLevel = 0;

	private void Start(){
		
		if(!PlayerPrefs.HasKey("unlockedLevel")){
			unlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1);

		}else{
		
			unlockedLevel = PlayerPrefs.GetInt("unlockedLevel");	
		}

		for (int i = 0; i < buttons.Length; i++)
			{
				buttons[i].interactable = false;
			}

		for (int i = 0; i < unlockedLevel; i++)
		{
			buttons[i].interactable = true;
		}

		Debug.Log(unlockedLevel);
		Debug.Log("MEME");
		PlayerPrefs.Save();
	}


	


}
