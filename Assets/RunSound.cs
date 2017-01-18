using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSound : MonoBehaviour {

	public Player player;
	public AudioClip run;
	public AudioClip jump;
	private bool runs = false;
	private SoundKit.SKSound SoundK;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (player.isRunning) {
			if (!runs) {
				SoundK =  SoundKit.instance.playSoundLooped (run);
				runs = true;
			}
		} else {
			runs = false;
			SoundK.fadeOutAndStop(1f);
		}
		if (player.jump) {
			SoundKit.instance.playSound (jump);
		
		} 
	}
}
