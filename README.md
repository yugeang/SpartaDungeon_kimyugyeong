# Unity 1인칭 서바이벌 게임 프로토타입

플레이어가 **아이템을 습득**하고, **소모품을 사용**하며, **트랩을 피하거나 맞으면서 체력을 관리**하는 1인칭 서바이벌 게임 프로토타입입니다.  
기본적인 이동, 점프, 인벤토리, 아이템 인터랙션, 체력 UI, 데미지 처리, 바운스 패드 등이 구현되어 있습니다.

---

## 조작법
| 동작 | 키 |
|------|----|
| 이동 | WASD |
| 시점 회전 | 마우스 이동 |
| 점프 | 스페이스바 |
| 상호작용 | E |
| 인벤토리 열기/닫기 | Tab |
| 아이템 사용 | 인벤토리 내 버튼 클릭 |
| 아이템 버리기 | 인벤토리 내 버튼 클릭 |

---

## 시스템 구성

### Player & Manager
- **CharacterManager** : 전역 싱글톤으로 플레이어 객체 참조 관리
- **Player** : PlayerController, PlayerCondition, 인벤토리 데이터 연결
- **PlayerController** : 이동, 점프, 마우스 회전, 커서 잠금, 속도 버프 적용
- **PlayerCondition** : 체력 관리(Condition), 데미지 처리(IDamagable 구현), 사망 처리

### 아이템 & 인벤토리
- **ItemData** (ScriptableObject) : 이름, 설명, 아이콘, 타입(자원/소모품), 효과(체력 회복, 속도 증가)
- **ItemObject** : 월드에 배치되는 아이템, 상호작용 후 플레이어 인벤토리에 추가, 리스폰 가능
- **UIInventory / ItemSlot** : 인벤토리 UI, 아이템 스택 처리, 사용/버리기 기능
- **Interaction** : 중앙 화면 레이캐스트로 IInteractable 오브젝트 감지 및 상호작용

### 환경 오브젝트
- **BouncePad** : 플레이어나 오브젝트를 위로 튕겨 올림 (쿨다운, 속도 초기화 옵션)
- **Trap** : 닿아있는 대상에게 주기적으로 데미지 부여

### UI
- **Condition** : 체력 게이지 표시 및 수치 변경
- **UICondition** : 플레이어의 UICondition 참조 연결
- **DamageIndicator** : 데미지 시 화면 플래시 효과

---

## 폴더 구조 (요약)
Assets/
├─ Scripts/
│ ├─ Core/
│ │ ├─ CharacterManager.cs
│ │ ├─ Player.cs
│ │ ├─ PlayerController.cs
│ │ ├─ PlayerCondition.cs
│ │ ├─ Condition.cs
│ │ └─ UICondition.cs
│ ├─ Items/
│ │ ├─ ItemData.cs
│ │ ├─ ItemObject.cs
│ │ ├─ UIInventory.cs
│ │ └─ ItemSlot.cs
│ ├─ Interaction/
│ │ └─ Interaction.cs
│ ├─ Environment/
│ │ ├─ BouncePad.cs
│ │ └─ Trap.cs
│ └─ UI/
│ └─ DamageIndicator.cs
├─ Art/
├─ Scenes/
└─ Resources/

---

## 주요 기능
- 플레이어 이동/점프/시점 회전
- 상호작용 가능한 오브젝트 시스템 (IInteractable)
- 아이템 습득/사용/버리기
- 체력 회복/속도 증가 버프
- 트랩 데미지 & UI 표시
- 바운스 패드 점프 기믹
- 인벤토리 UI와 스택 처리
- 데미지 시 화면 피드백

---

## 로드맵
- [ ] 몬스터 AI 및 전투 시스템 추가
- [ ] 다양한 아이템 타입(무기)
- [ ] UI/UX 개선

