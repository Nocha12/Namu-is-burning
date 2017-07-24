using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ground : MonoBehaviour {
	enum GroundType {
		Normal,
		Stone,
		Fire,
		Trash
	}

	public int type = (int)GroundType.Normal;
	public GameObject changeGround;
	GameObject fireUpgrade;
	GameObject rockUpgrade;
	GameObject trashUpgrade;
	GameObject Upgrade;
	Slider pollutionBar;
	public bool isTouched = false;
	public int level = 0;
	public int pollution = 100;
	int cheatCount = 0;

	GameObject waveText;
	GameObject hpText;
	GameObject energyText;

	void Awake()
	{
		changeGround = GameObject.FindGameObjectWithTag ("ChangeGround");
		fireUpgrade = GameObject.FindGameObjectWithTag ("FireUpgrade");
		rockUpgrade = GameObject.FindGameObjectWithTag ("RockUpgrade");
		trashUpgrade = GameObject.FindGameObjectWithTag ("TrashUpgrade");
		waveText = GameObject.Find ("wText");
		hpText = GameObject.Find ("HpText");
		energyText = GameObject.Find ("energyText");
	}

	void TextChangeFade (bool isFadeIn)
	{
		if (isFadeIn) {
			FadeManager.instance.TextFaidIn (waveText);
			FadeManager.instance.TextFaidIn (hpText);
			FadeManager.instance.TextFaidIn (energyText);
		} else {
			FadeManager.instance.TextFaidOut (waveText);
			FadeManager.instance.TextFaidOut (hpText);
			FadeManager.instance.TextFaidOut (energyText);
		}
	}

	void OnMouseDown()
	{
		if ((changeGround.transform.GetChild (0).GetComponent<Image> () && changeGround.transform.GetChild (0).GetComponent<Image> ().color.a == 0)
		    && (fireUpgrade.transform.GetChild (0).GetComponent<Image> () && fireUpgrade.transform.GetChild (0).GetComponent<Image> ().color.a == 0)
		    && (rockUpgrade.transform.GetChild (0).GetComponent<Image> () && rockUpgrade.transform.GetChild (0).GetComponent<Image> ().color.a == 0)
		    && (trashUpgrade.transform.GetChild (0).GetComponent<Image> () && trashUpgrade.transform.GetChild (0).GetComponent<Image> ().color.a == 0)) {
			GroundClick ();
			cheatCount = 0;
		} else {
			cheatCount++;
			if (cheatCount > 10)
				GameManager.instance.energy = 999;
		}
	}

	void Update()
	{
		if (pollutionBar)
			pollutionBar.value = pollution;

		if (pollution <= 0) {
			type = (int)GroundType.Normal;
			if (!(fireUpgrade.transform.GetChild (0).GetComponent<Image> () && fireUpgrade.transform.GetChild (0).GetComponent<Image> ().color.a == 0))
			{
				level = 0;
				pollution = 100;
				FadeManager.instance.CanvasFaidOut(fireUpgrade);
				isTouched = false;
				FadeManager.instance.SpriteFaidOut(transform.Find("Fire").gameObject);
			}
			if (!(trashUpgrade.transform.GetChild (0).GetComponent<Image> () && trashUpgrade.transform.GetChild (0).GetComponent<Image> ().color.a == 0))
			{
				level = 0;
				pollution = 100;
				FadeManager.instance.CanvasFaidOut(trashUpgrade);
				isTouched = false;
				FadeManager.instance.SpriteFaidOut(transform.Find("Trash").gameObject);
			}
			if (!(rockUpgrade.transform.GetChild (0).GetComponent<Image> () && rockUpgrade.transform.GetChild (0).GetComponent<Image> ().color.a == 0))
			{
				level = 0;
				pollution = 100;
				FadeManager.instance.CanvasFaidOut(rockUpgrade);
				isTouched = false;
				FadeManager.instance.SpriteFaidOut(transform.Find("Stone").gameObject);
			}
		}
	}

	void GroundClick()
	{
		isTouched = true;
		AudioManager.instance.touchSound.Play ();
		if (type == (int)GroundType.Normal) {
			for(int i = 0; i < changeGround.transform.childCount; i++) 
			{ 
				Transform child = changeGround.transform.GetChild(i); 
				child.gameObject.SetActive(true);
			}
			FadeManager.instance.CanvasFaidIn(changeGround);
			TextChangeFade (false);
		}

		if (type == (int)GroundType.Fire) {
			for(int i = 0; i < fireUpgrade.transform.childCount; i++) 
			{ 
				Transform child = fireUpgrade.transform.GetChild(i); 
				child.gameObject.SetActive(true);
			}
			FadeManager.instance.CanvasFaidIn(fireUpgrade);
			GameObject.Find ("FireLevelText").GetComponent<Text> ().text = level.ToString();
			GameObject.Find ("FireEnergyText").GetComponent<Text> ().text = (level + 3).ToString();
			pollutionBar = GameObject.Find ("FirePollutionBar").GetComponent<Slider>();
			TextChangeFade (false);
		}
		if (type == (int)GroundType.Stone) {
			for(int i = 0; i < rockUpgrade.transform.childCount; i++) 
			{ 
				Transform child = rockUpgrade.transform.GetChild(i); 
				child.gameObject.SetActive(true);
			}
			FadeManager.instance.CanvasFaidIn(rockUpgrade);
			GameObject.Find ("RockLevelText").GetComponent<Text> ().text = level.ToString();
			GameObject.Find ("RockEnergyText").GetComponent<Text> ().text = (level + 2).ToString();
			pollutionBar = GameObject.Find ("RockPollutionBar").GetComponent<Slider>();
			TextChangeFade (false);
		}

		if (type == (int)GroundType.Trash) {
			for(int i = 0; i < trashUpgrade.transform.childCount; i++) 
			{ 
				Transform child = trashUpgrade.transform.GetChild(i); 
				child.gameObject.SetActive(true);
			}
			FadeManager.instance.CanvasFaidIn(trashUpgrade);
			GameObject.Find ("TrashLevelText").GetComponent<Text> ().text = level.ToString();
			GameObject.Find ("TrashEnergyText").GetComponent<Text> ().text = (level + 2).ToString();
			pollutionBar = GameObject.Find ("TrashPollutionBar").GetComponent<Slider>();
			TextChangeFade (false);
		}
	}

	public void FireInstall()
	{
		if (!isTouched) return;
		AudioManager.instance.touchSound.Play ();
		if (GameManager.instance.energy < 4)
			return;
		else
			GameManager.instance.energy -= 4;
		level++;
		pollution = 100;
		type = (int)GroundType.Fire;
		FadeManager.instance.CanvasFaidOut(changeGround);
		isTouched = false;
		FadeManager.instance.SpriteFaidIn(transform.Find("Fire").gameObject);
		TextChangeFade (true);
	}

	public void StoneInstall()
	{
		if (!isTouched) return;
		AudioManager.instance.touchSound.Play ();
		if (GameManager.instance.energy < 3)
			return;
		else
			GameManager.instance.energy -= 3;
		level++;
		pollution = 100;
		type = (int)GroundType.Stone;
		FadeManager.instance.CanvasFaidOut(changeGround);
		isTouched = false;
		FadeManager.instance.SpriteFaidIn(transform.Find("Stone").gameObject);
		TextChangeFade (true);
	}

	public void TrashInstall()
	{
		if (!isTouched) return;
		AudioManager.instance.touchSound.Play ();
		if (GameManager.instance.energy < 3)
			return;
		else
			GameManager.instance.energy -= 3;
		level++;
		pollution = 100;
		type = (int)GroundType.Trash;
		FadeManager.instance.CanvasFaidOut(changeGround);
		isTouched = false;
		FadeManager.instance.SpriteFaidIn(transform.Find("Trash").gameObject);
		TextChangeFade (true);
	}

	public void InstallBackBtn()
	{
		if (!isTouched) return;
		AudioManager.instance.touchSound.Play ();
		FadeManager.instance.CanvasFaidOut(changeGround);
		isTouched = false;
		TextChangeFade (true);
	}

	public void FireUpgrade()
	{
		if (!isTouched) return;
		AudioManager.instance.touchSound.Play ();
		if (GameManager.instance.energy < level + 3)
			return;
		else
			GameManager.instance.energy -= level + 3;
		level++;
		pollution = 100;
		FadeManager.instance.CanvasFaidOut(fireUpgrade);
		isTouched = false;
		TextChangeFade (true);
	}

	public void FireBackBtn()
	{
		if (!isTouched) return;
		AudioManager.instance.touchSound.Play ();
		FadeManager.instance.CanvasFaidOut(fireUpgrade);
		isTouched = false;
		pollutionBar = null;
		TextChangeFade (true);
	}

	public void RockUpgrade()
	{
		if (!isTouched) return;
		AudioManager.instance.touchSound.Play ();
		if (GameManager.instance.energy < level + 2)
			return;
		else
			GameManager.instance.energy -= level + 2;
		level++;
		pollution = 100;
		FadeManager.instance.CanvasFaidOut(rockUpgrade);
		isTouched = false;
		TextChangeFade (true);
	}

	public void RockBackBtn()
	{
		if (!isTouched) return;
		AudioManager.instance.touchSound.Play ();
		FadeManager.instance.CanvasFaidOut(rockUpgrade);
		isTouched = false;
		pollutionBar = null;
		TextChangeFade (true);
	}

	public void TrashUpgrade()
	{
		if (!isTouched) return;
		AudioManager.instance.touchSound.Play ();
		if (GameManager.instance.energy < level + 2)
			return;
		else
			GameManager.instance.energy -= level + 2;
		level++;
		pollution = 100;
		FadeManager.instance.CanvasFaidOut(trashUpgrade);
		isTouched = false;
		TextChangeFade (true);
	}

	public void TrashBackBtn()
	{
		if (!isTouched) return;
		AudioManager.instance.touchSound.Play ();
		FadeManager.instance.CanvasFaidOut(trashUpgrade);
		isTouched = false;
		pollutionBar = null;
		TextChangeFade (true);
	}
}
