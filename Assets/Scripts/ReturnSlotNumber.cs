using UnityEngine;
using System.Collections;

public class ReturnSlotNumber : MonoBehaviour
{
	
		Layout gui;
		int clickpos;
	
		void Start ()
		{
				gui = GameObject.FindGameObjectWithTag ("TheUI").GetComponent<Layout> ();
				int.TryParse (this.gameObject.name, out clickpos);
				
		}
	
		public void Clickpos ()
		{
				switch (gui.layoutnumber) {
				case 1: //classic
						{
								if (gui.becuri [clickpos].sprite == gui.bec) {
										gui.prevpos = clickpos;
										gui.becuri [clickpos].sprite = gui.nulla;
										gui.becuriframe [clickpos].color = new Color (54 / 255.0f, 54 / 255.0f, 54 / 255.0f, 149 / 255.0f);
										gui.scoreint += 1;
										StartCoroutine (gui.SpawnBulbs ());
								}
								if (gui.becuri [clickpos].sprite == gui.neon) {
										gui.becuri [clickpos].sprite = gui.nulla;
										gui.becuriframe [clickpos].color = new Color (54 / 255.0f, 54 / 255.0f, 54 / 255.0f, 149 / 255.0f);
										gui.time.value += 3;
										gui.scoreint += 3;
				
								}
							
								break;}
				case 2://light attack
						{
								if (gui.becuri [clickpos].sprite == gui.bec) {
										gui.becuri [clickpos].sprite = gui.nulla;
										gui.becuriframe [clickpos].color = new Color (54 / 255.0f, 54 / 255.0f, 54 / 255.0f, 149 / 255.0f);
										gui.scoreint += 1;
										gui.time.value += 1;
										gui.prevpos = clickpos;
								}
								if (gui.becuri [clickpos].sprite == gui.neon) {
										gui.becuri [clickpos].sprite = gui.nulla;
										gui.becuriframe [clickpos].color = new Color (54 / 255.0f, 54 / 255.0f, 54 / 255.0f, 149 / 255.0f);
										gui.time.value += 5;
										gui.scoreint += 5;
								}
								if (gui.becuri [clickpos].sprite == gui.bomb) {
										gui.becuri [clickpos].sprite = gui.nulla;
										gui.becuriframe [clickpos].color = new Color (54 / 255.0f, 54 / 255.0f, 54 / 255.0f, 149 / 255.0f);
										gui.time.value -= 3;
				gui.scoreint -= 3;
				if (gui.scoreint<0)
					gui.scoreint = 0 ;
								}
							
			
			
								break;
						}
				case 3:
						{
								if (gui.becuri [clickpos].sprite == gui.bec) {
										gui.becuri [clickpos].sprite = gui.nulla;
										gui.becuriframe [clickpos].color = new Color (54 / 255.0f, 54 / 255.0f, 54 / 255.0f, 149 / 255.0f);
										gui.scoreint += 1;
										gui.becurislider [clickpos].value = 1.01f;
										gui.becurislider [clickpos].gameObject.SetActive (false);
										gui.prevpos = clickpos;
								}
								
								break;
						}
				}

		}
}
