using UnityEngine;
using System.Collections;

public class ProceduralSteps : ScriptableObject {


	public int priority;
	public enum conditionEnum
	{
		Score,
		Trigger,
		Other
	};
	public conditionEnum condition;
	public float ScoreCondition;
	public enum ProcSteps
	{
		Step1,
		Step2,
		Step3,
		Step4,
		Step5,
		Step6
	};
	public ProcSteps stepID;

	public ProcSteps transformID(int _id)
	{
		ProcSteps res = ProcSteps.Step1;
		switch (_id)
		{
		case (1):
		{
			res = ProcSteps.Step1;
			break;
		}
		case (2):
		{
			res = ProcSteps.Step2;
			break;
		}
		case (3):
		{
			res = ProcSteps.Step3;
			break;
		}
		case (4):
		{
			res = ProcSteps.Step4;
			break;
		}
		case (5):
		{
			res = ProcSteps.Step5;
			break;
		}
		case (6):
		{
			res = ProcSteps.Step6;
			break;
		}
		}
		return res;
	}

}
