using UnityEngine;

public class CharacterManager : MonoBehaviour
/*전역에서 접근 가능한 싱글톤 패턴 구현
게임 도중에 Player 참조를 저장하고 꺼낼 수 있음
씬이 바뀌어도 Manager 오브젝트는 계속 유지
이미 존재하는 인스턴스가 있다면 새로 생성된 건 삭제해서 중복 방지*/
{
    private static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // 씬에서 먼저 찾기
                _instance = FindObjectOfType<CharacterManager>();

                // 그래도 없으면 생성
                if (_instance == null)
                {
                    var obj = new GameObject("CharacterManager");
                    _instance = obj.AddComponent<CharacterManager>();
                }
            }
            return _instance;
        }
    }

    private Player _player;
    public Player Player
    {
        get => _player;
        set => _player = value;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}