using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Layout : MonoBehaviour
{
		//PREMIUM NOT CHANGD! TEST FREE BEFORE!	
		Transform playground;
		Text score;
		[System.NonSerialized]
		public int
				scoreint;
		public Slider time;
		public Slider life;
		Transform gamepanel;
		Transform gameover;
		Transform blockRaycast;
		Button pause;
		Text timeremaining;
		Text liferemaining;
		Text starttimetext;
		[System.NonSerialized]
		public Image[]
				becuri = new Image[14];
		public Image[] becuriframe = new Image[14];
		public Slider[] becurislider = new Slider[14];
		[System.NonSerialized]
		public int
				layoutnumber = 0;
		int i, startTime, layoutlength, livesLeft;
		Text[] bestscore = new Text[4];
		float t, c;
		bool startAnimation;
		[System.NonSerialized]
		public int
				prevpos = 0;
		public Sprite nulla;
		public Sprite bec;
		public Sprite neon;
		public Sprite bomb;
		public Sprite stone;
		[System.NonSerialized]
		public float
				spawnspeed;
		[System.NonSerialized]
		public AdMob
				admob;
	
		void Awake ()
		{
				score = transform.Find ("Game/ScorePanel/Score").GetComponent<Text> ();
				time = transform.Find ("Game/Time").GetComponent <Slider> ();
				life = transform.Find ("Game/Life").GetComponent<Slider> ();
				timeremaining = time.transform.Find ("Seconds").GetComponent<Text> ();
				liferemaining = life.transform.Find ("Lives").GetComponent<Text> ();
				starttimetext = transform.Find ("Game/AnimTime").GetComponent<Text> ();
				gamepanel = transform.Find ("Game");
				gameover = gamepanel.Find ("GameOver");
				blockRaycast = gamepanel.Find ("BlockRaycast");
				pause = gamepanel.Find ("Pause").GetComponent<Button> ();
				layoutlength = 12;
				Time.timeScale = 0;
				layoutnumber = 0;
				admob = GameObject.FindGameObjectWithTag ("AdMob").GetComponent<AdMob> ();
				bestscore [1] = transform.Find ("Score/Classic/Score").GetComponent<Text> ();
				bestscore [2] = transform.Find ("Score/LightAttack/Score").GetComponent<Text> ();
				bestscore [3] = transform.Find ("Score/Countto3/Score").GetComponent<Text> ();
		
		}
	
		void FixedUpdate ()
		{
				if (startAnimation) {
						StartCoroutine ("PreGame");
						timeremaining.text = c.ToString ();		
						pause.interactable = false;
				} else {
						StopCoroutine ("PreGame");
						if (layoutnumber != 3) {
								TimeFlows ();
								timeremaining.text = t.ToString ();	
						} else { 
								life.value = livesLeft;
								liferemaining.text = "x" + livesLeft.ToString ();
								if (life.value == 0) {
										starttimetext.text = "UP!";
										StartCoroutine ("EndGame");
										blockRaycast.gameObject.SetActive (true);
								}
						}
						score.text = scoreint.ToString ();
						pause.interactable = true;
			
				}
		}
	
		IEnumerator PreGame ()
		{
				startTime = (int)t;
				starttimetext.gameObject.SetActive (true);
				starttimetext.text = startTime.ToString ();
				yield return new WaitForSeconds (1f);
				t -= Time.deltaTime;
				if (t <= 1.6f && time.value < 60)
						time.value = Mathf.Lerp (time.value, 61, Time.deltaTime * 2);
				if (startTime == 0)
						starttimetext.text = "GO!";
				if (t <= 0.1 && time.value == 60) {
						starttimetext.gameObject.SetActive (false);
						starttimetext.text = "";
						startAnimation = false;
				}
				c = (int)time.value;
		
		}
	
		void TimeFlows ()
		{	
				time.value -= Time.deltaTime;
				t = (int)time.value;
				if (t <= 5 && !startAnimation) {
						starttimetext.gameObject.SetActive (true);
						if (t == 0) {
								starttimetext.text = "UP!";
								StartCoroutine ("EndGame");
								blockRaycast.gameObject.SetActive (true);
						} else {
								starttimetext.text = t.ToString ();
								blockRaycast.gameObject.SetActive (false);
						}
				} else if (t > 5 && !startAnimation)
						starttimetext.gameObject.SetActive (false);
		}
	
		public IEnumerator SpawnBulbs ()
		{
				bool zz = true;
				while (zz) {
						if (!startAnimation) {
								switch (layoutnumber) {
								case 1:
										{
												int b;
												do {
														b = UnityEngine.Random.Range (1, layoutlength + 1); 
												} while(b==prevpos||becuri[b].sprite!=nulla);
												prevpos = b;
												becuri [b].sprite = bec;
												becuriframe [b].color = new Color (241 / 255.0f, 255 / 255.0f, 105 / 255.0f, 149 / 255.0f);
					
												zz = false; //opreste while-u pt coroutina controlata
					
												break;
										}
					
								case 2:
										{								
												int b, c = 0;
												object[] parms = new object[2];
												
					
												do {
														b = UnityEngine.Random.Range (1, layoutlength + 1);
														c++;
														if (c == layoutlength)
																break;
												} while(becuri[b].sprite!=nulla||b==prevpos);
												if (c < layoutlength) {
														becuri [b].sprite = bec;
														becuriframe [b].color = new Color (241 / 255.0f, 255 / 255.0f, 105 / 255.0f, 149 / 255.0f);
														if (spawnspeed >= 0.4f)
																spawnspeed -= 0.004f;
					
														parms [1] = spawnspeed;
														parms [0] = b;
														StartCoroutine ("Despawn", parms);

												}
												break;						
										}
								case 3:
										{
												int b, final = 0;
					
												do {
														b = UnityEngine.Random.Range (1, layoutlength + 1);
														final++;
														if (final == layoutlength)
																break;
												} while(becuri[b].sprite!=nulla||b==prevpos);
					
												if (final < layoutlength) {
						
														becuri [b].sprite = bec;
														becuriframe [b].color = new Color (241 / 255.0f, 255 / 255.0f, 105 / 255.0f, 149 / 255.0f);
														becurislider [b].gameObject.SetActive (true);
														if (spawnspeed >= 0.27f)
																spawnspeed -= 0.004f;
														becurislider [b].value = 1;
														StartCoroutine ("Slider", b);
												}
												break;
										}
								}				
						}
						yield return new WaitForSeconds (spawnspeed);			
				}
		}
	
		IEnumerator SpawnNeon ()
		{	
				bool zz = true;
				while (zz) {
						float waittime = 0;
						if (layoutnumber == 1)
								waittime = UnityEngine.Random.Range (8, 15);
			
						if (layoutnumber == 2)
								waittime = 0.7f;
			
						yield return new WaitForSeconds (waittime);	
						if (!startAnimation) {
								int b, c = 0;
								object[] parms = new object[2];
				
				
								do {
										b = UnityEngine.Random.Range (1, layoutlength + 1);
										c++;
										if (c == layoutlength)
												break;
								} while(becuri[b].sprite!=nulla);
								if (c < layoutlength) {
										parms [0] = b;
										parms [1] = spawnspeed;
				
										if (layoutnumber == 1) {
												becuri [b].sprite = neon;
												becuriframe [b].color = new Color (135 / 255.0f, 215 / 255.0f, 255 / 255.0f, 149 / 255.0f);
					
												if (becuri [b].sprite == neon) 
														StartCoroutine ("Despawn", parms);
					
					
										}
										if (layoutnumber == 2) {
												if (UnityEngine.Random.value <= 0.98) {
														becuri [b].sprite = bomb;
														becuriframe [b].color = new Color (1f, 0f, 0f, 149 / 255.0f);
												} else {
														becuri [b].sprite = neon;
														becuriframe [b].color = new Color (135 / 255.0f, 215 / 255.0f, 255 / 255.0f, 149 / 255.0f);
												}
												if (becuri [b].sprite == neon || becuri [b].sprite == bomb) 
														StartCoroutine ("Despawn", parms);
					
										}					
								}
						}
				}
		
		}
	
		IEnumerator Despawn (object[] parms)
		{
				int b = (int)parms [0];
				float despawnTime = (float)parms [1];
				yield return new WaitForSeconds (despawnTime);
		
				becuri [b].sprite = nulla;
				becuriframe [b].color = new Color (54 / 255.0f, 54 / 255.0f, 54 / 255.0f, 149 / 255.0f);
				//	StopAllCoroutines ();		
				//StopCoroutine ("Despawn");
		}
	
		public void StartGameMode (int lnumber)
		{
				Time.timeScale = 1;
				score.text = "0";
				scoreint = 0;
				time.value = 0;
				prevpos = 0;
				t = 3;
				playground = transform.Find ("Game/Layout_" + 1.ToString ());	//nu merge, trebuie pus lnumber in loc
		
				playground.gameObject.SetActive (true);
				transform.Find ("Game").gameObject.SetActive (true);
				blockRaycast.gameObject.SetActive (false);
				startAnimation = true;
				life.gameObject.SetActive (false);
				time.gameObject.SetActive (true);
		
		
		
		
				for (i=1; i<=layoutlength; i++) {
						becuri [i] = playground.Find (i.ToString () + "/Image").GetComponent<Image> ();
						becuri [i].sprite = nulla;	
						becuriframe [i] = playground.Find (i.ToString ()).GetComponent<Image> ();
						becuriframe [i].color = new Color (54 / 255.0f, 54 / 255.0f, 54 / 255.0f, 149 / 255.0f);
						becurislider [i] = playground.Find (i.ToString () + "/Slider").GetComponent<Slider> ();	
						becurislider [i].gameObject.SetActive (false);
				}
		
		
				switch (lnumber) {
				case 1:
						{
								StartCoroutine ("SpawnNeon");
								break;
						}
				case 2:
						{
								spawnspeed = 1f;
								for (i=1; i<=8; i++) {
										StartCoroutine ("SpawnNeon");
								}			
								break;				
						}
				case 3:
						{
								spawnspeed = 0.8f;
								livesLeft = 3;
								liferemaining.text = "x" + livesLeft.ToString ();
								life.value = livesLeft;
								time.gameObject.SetActive (false);
								life.gameObject.SetActive (true);
								break;
						}
				}
		
		
		
				StartCoroutine ("SpawnBulbs");
		}
	
		IEnumerator EndGame ()
		{
				yield return new WaitForSeconds (1);
				admob.ShowInterstitial ();
				Time.timeScale = 0;
				time.value = 0;
				for (i=1; i<=layoutlength; i++) {
						becuri [i].sprite = nulla;	
						becuriframe [i].color = new Color (1f, 0f, 0f, 0.5f);
						starttimetext.gameObject.SetActive (false);
						gameover.gameObject.SetActive (true);
			
			
						Text actualscore = gameover.Find ("ActualScore").GetComponent<Text> ();
						Text bestscoretext = gameover.Find ("BestScore").GetComponent<Text> ();
						actualscore.text = scoreint.ToString ();
			
						if (scoreint > PlayerPrefs.GetInt ("Best" + layoutnumber.ToString ()))
								PlayerPrefs.SetInt ("Best" + layoutnumber.ToString (), scoreint);
			
						bestscoretext.text = PlayerPrefs.GetInt ("Best" + layoutnumber.ToString ()).ToString ();	
			
			
						StopAllCoroutines ();
				}
		}
	
		public IEnumerator Slider (int becid)
		{
		
				yield return new WaitForSeconds (0.1f);
		
				while (becurislider [becid].value!=0&&	becurislider [becid].value<=1) {
						becurislider [becid].value = Mathf.Lerp (becurislider [becid].value, -1, Time.deltaTime);
			
						yield return null;		
				}
		
				if (becurislider [becid].value == 0) {
						becuriframe [becid].color = new Color (0.5f, 0.5f, 0.5f, 149 / 255.0f);
						becuri [becid].sprite = stone;	
						livesLeft--;
				}
		}
	
		public void LayoutNumber (int clone)
		{
				layoutnumber = clone;
		}
	
		public void RestartGameMode ()
		{
				StartGameMode (layoutnumber);
		}
	
		public void PauseGame ()
		{
				Time.timeScale = 0;
		}
	
		public void ResumeGame ()
		{
				Time.timeScale = 1;
		}
	
		public void EndGameCall ()
		{
				Time.timeScale = 0;
				time.value = 0;
				starttimetext.gameObject.SetActive (false);
				StopAllCoroutines ();
		}
	
		public void BestScoree ()
		{
				bestscore [1].text = PlayerPrefs.GetInt ("Best1").ToString ();
				bestscore [2].text = PlayerPrefs.GetInt ("Best2").ToString ();
				bestscore [3].text = PlayerPrefs.GetInt ("Best3").ToString ();
		
		}
	
		public void ResetScores ()
		{
				PlayerPrefs.SetInt ("Best1", 0);
				PlayerPrefs.SetInt ("Best2", 0);
				PlayerPrefs.SetInt ("Best3", 0);
		}
	
		public void BuyPremium ()
		{
				Application.OpenURL ("https://play.google.com/store/apps/details?id=com.Tankbird.DeLight");
		}
}


