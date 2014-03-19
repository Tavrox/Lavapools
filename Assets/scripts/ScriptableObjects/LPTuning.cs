using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LPTuning : ScriptableObject {
	
	public Vector2 Player_Friction;
	public int Gem_Value = 1;

	public Color ColPlayer;
	public Color ColRank;
	public Color ColScore;
	public Color ColInput;

	[HideInInspector] public float Immovable_Speed = 0f;	
	[HideInInspector] public float CapturePoint_Score = 600f;
	[HideInInspector] public float CaptureSpeed = 1f;
	[HideInInspector] public float ScoreOverTime = 5f;

	public enum LevelNames
	{
		Vesuvio,
		Grensdalur,
		Etna
	}
}
