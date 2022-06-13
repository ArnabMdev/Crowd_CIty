using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class KilledBy : MonoBehaviour {

	public TextMeshProUGUI rank, Killedname;

	public void KilledByMethod()
	{
		Killedname.text = PlayerController.killedBy.ToString ();
		//rank = 
	}

	public void Restart()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
