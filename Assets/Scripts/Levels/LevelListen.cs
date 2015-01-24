﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelListen : MonoBehaviour 
{
	private GameObject m_player;
	private Image m_fadeScreen;
	private Vector4 m_screenColor;

	private void Awake ()
	{
		m_player = GameObject.FindGameObjectWithTag("Player");
		m_fadeScreen = GameObject.Find("FadeScreen").transform.FindChild("Image").GetComponent<Image>();
		m_screenColor = m_fadeScreen.color;
	}
	
	private void OnTriggerEnter (Collider p_collider)
	{
		if(p_collider.CompareTag("Player"))
		{
			float randPan = Random.Range(0.8f, 1.0f);
			if(Random.Range(0.0f,1.0f) < 0.5f) randPan = -randPan;
			audio.pan = randPan;
			audio.volume = 0.0f;
			audio.Play();
			StartCoroutine("IEFadeIn");
		}
	}
	
	private void OnTriggerExit (Collider p_collider)
	{
		if(p_collider.CompareTag("Player"))
		{
			if(Mathf.Abs(audio.pan) > 0.4f)
			{
				LevelMngr.Instance.UpdateQoute(1);
				LevelMngr.Instance.LoadNextLevel(Constants.LVL_DOWNBELOW);
				StartCoroutine("ResetGame");
			}
			//else hoorah!
		
			StartCoroutine("IEFadeOut");
		}
	}
	
	private IEnumerator IEFadeOut ()
	{
		while (audio.volume > 0)
		{
			audio.volume -= Time.deltaTime * 0.3f;
			yield return 0;
		}
		
		audio.Stop();
	}
	
	private IEnumerator IEFadeIn ()
	{
		while (audio.volume < 1)
		{
			audio.volume += Time.deltaTime * 0.3f;
			yield return 0;
		}
	}
	
	private IEnumerator ResetGame ()
	{
		// fade to black
		while (m_screenColor.w < 1.0f)
		{
			m_screenColor.w += Time.deltaTime * 300.0f * Constants.ALPHA_RATIO;
			m_fadeScreen.color = m_screenColor;
			yield return 0;
		}
		
		// reset player
		m_player.transform.position = Constants.PLAYER_INITPOS;
		m_player.transform.localScale = Vector3.one;
		m_player.transform.eulerAngles = Vector3.zero;
		yield return new WaitForSeconds(0.3f);
		
		// fade to black
		while (m_screenColor.w > 0.0f)
		{
			m_screenColor.w -= Time.deltaTime * 300.0f * Constants.ALPHA_RATIO;
			m_fadeScreen.color = m_screenColor;
			yield return 0;
		}
	}
}