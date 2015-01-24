﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelDownBelow : MonoBehaviour 
{
	public Text m_clue;
	private float m_clueFadeInSpeed;
	Vector4 m_clueColor;
	
	private void Awake ()
	{
		m_clueFadeInSpeed = 0.0f;
		m_clueColor = m_clue.color;
	}
	
	private void Start ()
	{
		FadeIn();
	}
	
	private void FadeIn (float p_speed = 30.0f)
	{
		m_clueFadeInSpeed = p_speed;
		StartCoroutine("IEFadeIn");
	}
	
	private IEnumerator IEFadeIn ()
	{
		while(m_clueColor.w < 1.0f)
		{
			m_clueColor.w += (Time.deltaTime * m_clueFadeInSpeed * Constants.ALPHA_RATIO);
			m_clue.color = (Color) m_clueColor;
			yield return 0;
		}
		
		StopCoroutine("IEFadeIn");
	}
}
