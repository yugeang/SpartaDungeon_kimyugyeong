using System;
using UnityEngine;

public class Player : MonoBehaviour
/*게임 시작 > Player 오브젝트 생성
 싱글톤 CharacterManager에 자기 자신을 등록
 자신의 PlayerController를 찾아서 controller 변수에 저장
 다른 스크립트에서 전역적으로 플레이어의 컨트롤러 기능 호출 가능*/
{
    public PlayerController controller;
    public PlayerCondition condition;

    public ItemData itemData;
    public Action addItem;

    private void Awake()
    {
        // 싱글톤매니저에 Player를 참조할 수 있게 데이터를 넘김
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}
