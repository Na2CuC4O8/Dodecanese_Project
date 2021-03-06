﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;



public class DodEventCentre{

	private static DodEventCentre _instance;
	public static DodEventCentre Instance{get{
		if (DodEventCentre._instance == null) {
			DodEventCentre._instance = new DodEventCentre();
		}
		return DodEventCentre._instance;
	}}

	private Dictionary<EType, UnityAction<DodEvent>> dic = new Dictionary<EType, UnityAction<DodEvent>>();

	public void on(EType type, UnityAction<DodEvent> listener){
		if (dic.ContainsKey(type)) {
			dic[type] += listener;
		} else {
			dic.Add(type, listener);
		}
	}

	public void off(EType type, UnityAction<DodEvent> listener){
		if (dic.ContainsKey(type)) {
			dic[type] -= listener;
		} else {
			//do nothing
		}
	}

	public void Invoke(DodEvent e){
		EType type = e.GetEType();
		if (dic.ContainsKey(type)) {
			dic[type].Invoke(e);
		}
	}

	public DodEventCentre(){
		//测试用
		// on(EType.NONE, (DodEvent e)=>{Debug.Log("trigger");});
		// Invoke(new DE_None());

		// UnityAction<DodEvent> act = new UnityAction<DodEvent>((DodEvent e) =>{
		// 	DE_Cyberpunk2077isReleased actual = (DE_Cyberpunk2077isReleased)e;
		// 	Debug.Log(actual.how + " much");
		// });
		// on(EType.BUT_WE_ALL_WANT_CYBERPUNK2077, act);
		// off(EType.BUT_WE_ALL_WANT_CYBERPUNK2077, act);
		// on(EType.BUT_WE_ALL_WANT_CYBERPUNK2077, (DodEvent e)=>{
		// 	var actual = (DE_Cyberpunk2077isReleased)e;
		// 	Debug.Log(actual.how + "cool");
		// });
		// on(EType.BUT_WE_ALL_WANT_CYBERPUNK2077, (DodEvent e)=>{
		// 	var actual = (DE_Cyberpunk2077isReleased)e;
		// 	Debug.Log(actual.how + " awesome");
		// });
		// on(EType.BUT_WE_ALL_WANT_CYBERPUNK2077, act);
		// Invoke(new DE_Cyberpunk2077isReleased("very"));
	}

}

public enum EType{
	NONE,
	BUT_WE_ALL_WANT_CYBERPUNK2077,
	ENTER_MAP_NODE,
	LEAVE_MAP_NODE,
	BLOCK,
	UI_ACTOR_CLICKED,	//Actor被点击
	ACTOR_LOCATION,
	OPERATOR_DEPLOYED,
	OPERATOR_WITHDRAWD,
	SHOW_UI_OPERTAOR,
	ACTORBLOCK_CLICKED,
	ADD_OPERATOR_TO_MAINUI,
	SUBUI_LEFT_BUTTON_CLICKED,
	SUBUI_RIGHT_BUTTON_CLICKED
}

public interface DodEvent{
	EType GetEType();
}

public class DE_None:DodEvent{
	public EType GetEType(){return EType.NONE;}
}

public class DE_Cyberpunk2077isReleased:DodEvent{
	public EType GetEType(){return EType.BUT_WE_ALL_WANT_CYBERPUNK2077;}
	public string how = "a bit";
	public DE_Cyberpunk2077isReleased(string how){
		this.how = how;
	}
}

public class DE_EnterMapNode : DodEvent{
	public EType GetEType(){return EType.ENTER_MAP_NODE;}
	public IntVec point;
	public GameObject publisher;
	public DE_EnterMapNode(IntVec point, GameObject publisher){
		this.point = point;
		this.publisher = publisher;
	}
}

public class DE_LeaveMapNode : DodEvent{
	public EType GetEType(){return EType.LEAVE_MAP_NODE;}
	public IntVec point;
	public GameObject publisher;
	public DE_LeaveMapNode(IntVec point, GameObject publisher){
		this.point = point;
		this.publisher = publisher;
	}
}

public class DE_Block : DodEvent{
	public EType GetEType(){return EType.BLOCK;}
	public GameObject blocker;
	public GameObject beBlocked;

	public DE_Block(GameObject blocker, GameObject beBlocked) {
		this.blocker = blocker;
		this.beBlocked = beBlocked;
	}
}

public class DE_ActorClicked : DodEvent{
	public EType GetEType(){return EType.UI_ACTOR_CLICKED;}
	public GameObject clickedActor;

	public DE_ActorClicked(GameObject clicked){
		clickedActor = clicked;
	}
}

//RM = Render module
public class RM_ActorLocation : DodEvent{
    public EType GetEType(){return EType.ACTOR_LOCATION;}
    public Vector2 location;

    public RM_ActorLocation(Vector2 position){
        location = position;
    }
}

public class RM_OperatorDeployed : DodEvent{
	public EType GetEType(){return EType.OPERATOR_DEPLOYED;}
	public string name;
	public Vector2 location;

	public RM_OperatorDeployed(string operatorName, Vector2 position){
		location = position;
		name = operatorName;
	}
}

public class RM_OperatorWithdrawd : DodEvent{
	public EType GetEType(){return EType.OPERATOR_WITHDRAWD;}
	public string name;//按照spine立绘文件名

	public RM_OperatorWithdrawd(string operatorName){
		name = operatorName;
	}
}

public class RM_ActorBlockClicked : DodEvent{
	public EType GetEType(){return EType.ACTORBLOCK_CLICKED;}
	public string name;
	public Vector2 location;
	public Vector3 worldPosition;

	public RM_ActorBlockClicked(string something, Vector2 position, Vector3 worldlocation){
		name = something;
		location = position;
		worldPosition = worldlocation;
	}
}

public class RM_AddOperatorToMainUI : DodEvent{
	public EType GetEType(){return EType.ADD_OPERATOR_TO_MAINUI;}
	public string name;

	public RM_AddOperatorToMainUI(string something){
		name = something;
	}
}


public class RM_ShowUIOperator : DodEvent{
	public EType GetEType(){return EType.SHOW_UI_OPERTAOR;}
	public string name;//按照spine立绘文件名

	public RM_ShowUIOperator(string operatorName){
		name = operatorName;
	}
}

public class RM_SubUILeftButtonClicked : DodEvent{
	public EType GetEType(){return EType.SUBUI_LEFT_BUTTON_CLICKED;}
	string operatorName;

	public RM_SubUILeftButtonClicked(string name){
		operatorName = name;
	}
}

public class RM_SubUIRightButtonClicked : DodEvent{
	public EType GetEType(){return EType.SUBUI_RIGHT_BUTTON_CLICKED;}
	string operatorName;

	public RM_SubUIRightButtonClicked(string name){
		operatorName = name;
	}
}