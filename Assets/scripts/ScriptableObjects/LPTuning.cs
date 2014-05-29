using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LPTuning : ScriptableObject {
	
	public Vector2 Player_Friction;
	public Vector3 PlayerSteps;
	public float PlayerSpeedReset;
	public int Gem_Value = 1;
	public int Gatepart_Value = 1;
	public float finishFirstStep = 5f;
	public float finishSecondStep = 15f;
	public float finishThirdStep = 25f;
	public float fadeAfterDelay = 4f;
	public float percentageLootStack;

	public Color ColPlayer;
	public Color ColRank;
	public Color ColScore;
	public Color ColInput;

	[HideInInspector] public float Immovable_Speed = 0f;	
	[HideInInspector] public float CapturePoint_Score = 600f;
	[HideInInspector] public float CaptureSpeed = 1f;
	[HideInInspector] public float ScoreOverTime = 5f;
}
