# 2026 닷넷 개발자 데스크톱 개발

## 2. Unity 실습

### 2.1 유니티 학습

- https://learn.unity.com/ 튜토리얼대로 하기
- keijiro Takahashi Github : https://github.com/keijiro
- 이전버전 확인 다우로드 설치

#### GetStarted With Unity

- Tutorial 순서대로 따라하기
![alt text](image-62.png)

- 1번 챕터 완료후

![alt text](image-63.png)

### 2.2 Essentials PathWay

- 가장 짧은 시간에 Unity를 학습할 수 있는 튜토리얼

#### Essentials PathWay Template

![alt text](image-64.png)

- 템플릿 다운로드 우선
- 프로젝트명, 프로젝트 위치 선택, 생성

#### 화면/시점 이동

- 방향키, wasd
- Mouse Right, Wheel
- Fly Mode : Mouse Right + wasd / EQ

- Object 선택 후 F 클릭

#### Pan Tool

- 오브젝트 위치, 회전, 크기를 조절할 수 있는 아이콘 툴바

- Veiw 
![alt text](image-68.png)
![alt text](image-67.png)
![alt text](image-66.png) 

#### Kid's Room 꾸미기

- 방 오브젝트
- 침대, 카펫, 협탁, 알람시계, 침실조명 등 위치 및 회전, 크기조정
![alt text](image-65.png)

![alt text](image-69.png)
- Material 객체를 Ball 객체에 드래그\

![alt text](image-70.png)

#### RigidBody

- 물리역학 기능 제공 컴포넌트
- Ball 선택 Inspector에서 Add Component 버튼 클릭

#### Physics Material

- 물체가 충돌할 때 마찰력, 반발력을 설정하는 자산
- Bounciness : 1 완전 탄성 충돌
    - 0.1(쇠구슬), 0.7(축구공), 0.9(고무공)

#### Ramp Object 추가

- 위치, 회전 지정
- Mesh Collider 컴포넌트 추가

![alt text](image-71.png)

#### Block 객체 생성

- Cube로 생성
- Scale x,y,z를 0.1, 0.25, 0.1로 설정. Ball이 튕겨서 닿는 위치에
- Rigid Body 추가

#### 카메라 시전 변환

- Flythrough 모드로 이동후
- 카메라 오브젝트 선택
- Ctrl + Shift + f : 현 카메라 시점을 플레이 카메라 시점으로 변경

#### 프리팹 변경

- Prefabs 폴더 내에 기존 Object를 드래그하면 Prefab으로 변경
![alt text](image-72.png)

#### Block 쌓기

- Pivot을 Center로 변경 후 
- 프리팹 Block을 쌓아올림

#### 프리팹 편집모드
![alt text](image-74.png)

- 프로젝트 창의 프리팹을 더블클릭
- Inspector 수정
- RigidBody > mass를 1보다 작게 수정(0.1)
- mass가 
- Hierachy 창의 < 버튼 클릭

#### 라이트, 스카이박스 조정

- 라이트
    - y,z 축으로 낮밤 조절 가능
    - Emission > Color 조정으로 빛 색상 조절
    - Emission > Light Appearance, Filter and Temprature 선택후 
    - 빛의 온도와 느낌을 조정
![alt text](image-73.png)
- 


#### 플레이ㅏ모드 구분 짓기
- Preferences > Colors > Play mode tints 색상을 어두운색으로 변경
- Play시 UI 색상이 Edit모드와 다르게 표시
![alt text](image-75.png)

#### 피벗기능

- Object를 쌓을때 v를 누르면 Object의 기준점이 변경

![alt text](image-76.png)

### Chapter2
![alt text](image-77.png)

#### Chapter3 

- 냄비 프리팹 선택. 가스레인지 위 위치
- Audio Source 컴포넌트 추가
- Audio Generator 선택, Loop 체크
- Spatial Blend : 2D ~ 3D로 변경

![alt text](image-79.png)


#### Unity 오브젝트 복사

- Ctrl + D : 선택한 오브젝트가 바로 복사

#### 단축키
- 메뉴 Edit > Shorcuts
![alt text](image-80.png)

#### 배경음악, 새소리
- 계층창에서  Audio Source 선택
- 알맞은 사운드 Audio Generator에 선택
- 시작하면서 바로 음악 플레이 하고 싶으면
    - Play on Awake 체크
- 새소리처럼 랜덤하게 플레이 하고 싶으면
    - Play on Awake 체크해제
    - PlaySoundAtRandomIntervals 스크립트 추가
    - Min/Max Seconds 랜덤시간 지정

#### Chapter 4. Programming

- 유니티 개발시 가장 핵심!

- Player 오브젝트 위치, 회전, 크기 조정
- PlayerController 스크립트 생성, Player 드래그

- 입력시스템 변경
    - Project Settings > Player > Other Settings > Input Handing, both로 바꾸기

#### 카메라 플레이어 Child 지정

- Main Camera, Player 하위로 드래그
- 카메라 위치 수정 및 드래그

- 방 아래 Cube까지 화면에 출력. 위치 조정 잘 해줘야 플레이시 카메라 진동X

#### 플레이모드 변수값 변경

- Speed : 5.0f, RotationSpeed : 120.0f
- 플레이시 이동속도가 빠름
- 플레이모드 변수값 수정하면서 알맞은 속도 확인
- 

#### 아이템 코인 오브젝트
- Prefabs 폴더에서 Collectible Coin 드래그, 위치, 사이즈 조정
- Collectable.cs 스크립트 생성

![alt text](image-81.png)

#### 아이템 획득 기능 추가

- Coin에 Box Colider > `Is Trigger` 체크
- 충돌은 발생하지 않고, 충돌감지 기능 활성화

![alt text](image-82.png)

```cs
public class Collectable : MonoBehaviour {
    [Header("회전 설정")]
    [Tooltip("프레임당 회전 속도")]
    public float rotationSpeed = 1.0f;

    [Tooltip("아이템 획득시 이펙트지정")]
    public GameObject OnCollectEffact;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);  // 매프레임마다 y축을 rotationSpeed씩 회전
    }

    // 물체끼리 충돌이 발생했을때 이벤트처리
    private void OnTriggerEnter(Collider other)
    {
        // Destroy the collectable 
        Destroy(gameObject);

        Instantiate(OnCollectEffact, transform.position, transform.rotation);
    }
}
```

#### 실행화면

- ![에센셜실행결과](./에센셜2.gif)

#### 점프기능 추가

- PlayerController.cs에 공용변수, Update() 추가

```cs
[Tooltip("점프강도")]
public float jumpForce = 3.0f;

private void Start()
{
    rb = GetComponent<Rigidbody>();
    if (rb == null) Debug.LogWarning("PlayerController needs a Rigidbody.");
}

// 입력처리, 카메라... Frame별 실행
// LateUpdate() : Update() 후에 실행되는 메서드. 카메라 추적
private void Update()
{
    if (Keyboard.current.spaceKey.wasPressedThisFrame)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }
}
```
![alt text](image-83.png)

#### 생성형 AI 활용 밤낮처리 추가

- 프롬프트
```
유니티에서 Directional Light를 조정해서 밤낮으로 바뀌는 스크립트를 작성해줘. 
20초에 한번씩 해가지고 다시 뜨도록 만들어줘. 
DayNightCycle.cs로 만들어줘
```

- Directional Light 오브젝트에 할당
- 변수 Sun Directional Light 할당

![alt text](image-84.png)

- Tutorial 스크립트

```cs
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("회전 속도 설정")]
    public float rotationSpeed = 1f;

    [Header("시간 설정")]
    [Tooltip("하루(24시간)가 지나는데 걸리는 실제 시간(초)")]
    public float dayDuration = 60f;

    private float timePassed = 0.0f;

    void Start()
    {
        rotationSpeed = Mathf.Abs(rotationSpeed);
    }

    void Update()
    {
        float angleToRotate =
            (360.0f / dayDuration) * Time.deltaTime;

        transform.Rotate(
            Vector3.right,
            angleToRotate * rotationSpeed);

        timePassed += Time.deltaTime;

        if (timePassed >= dayDuration)
        {
            timePassed = 0.0f;
        }
    }
}
```

#### 방문열기 기능

- DoorOpener.cs 생성
- Door 루트오브젝트에 스크립트 지정
- Box Colloder 추가 후 위치, 크기 수정
- 튜토리얼에 있는 스크립트 붙여넣기
- Player 객체에 `Player` 태그를 선택

![alt text](image-85.png)

#### 코인 획득 사운드 추가
```cs

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        AudioSource.PlayClipAtPoint(pickupSound, transform.position);   // 뾰롱소리
        Destroy(gameObject);     // 코인삭제
        Instantiate(OnCollectEffact, transform.position, transform.rotation);   // 파티클 이펙트 실행
    }
}
```

- 프리팹의 코인을 선택, Script 내 pickupSound 설정

![alt text](image-86.png)

#### Chapter 6. 배포하기

- UI(Canva) 메뉴에서 선택

![alt text](image-87.png)

#### 빌드 시 사용할 신 리스트 설정

- 메뉴 File > Build profile 선택
- seenlist 추가

![alt text](image-88.png)

- 플레이어 세팅 작업
    - Company Name, Product Name, Version, Default icon
    - Resolution > windowed, Width, Height 설정
![alt text](image-89.png)

- 메뉴 클릭 신 이동, ESC키로 메뉴 리턴

![alt text](image-90.png)

![alt text](image-91.png)

- 유니티 UI Canvase > Button Inspector 속성


![alt text](image-92.png)
---



### 2.2 3D 모델 불러오기

#### 렌더링 파이프라인

- 오브젝트 생성, 카메라 확인, 빛 계산, 그림자 계산, 재질 생성/계산, 후처리 후 모니터 출력 등의 순서과정

#### Built-in / SRP

- Built-in - Unity가 렌더링 방식 고정. 수정 어려움
- SRP(Scriptable Render Pipiline) - Unity는 뼈대만 제공, 개발자가 원하는 렌더링을 추가하는 방식

#### 프로젝트 구분

- 렌더링 파이프라인 종류 3가지 구분

| 종류 | 성능 | 그래픽품질 | 모바일/VR지원 |
|---|---|---|---|
| Built-in | 보통 | 보통 | 보통 |
| URP | 높음 | 높음 | 높음 |
| HDRP | 낮음(고사양) | 매우좋음 | 제한적 |

- 기본적으로 Built-in으로 학습

![alt text](image-93.png)

- 에셋스토어에서 제공하는 에셋의 RP 종류를 확인하고 사용할 것

#### 3D 모델 활용법

- 유니티에서는 3D 모델링이 아주 제한적
- 3D 모델 활용방법
    - Blender 무료 3D 모델링 툴에서 작업한 모델 import
    - 3D Max, Rhino 상용 모델링 툴 작업 모델 import 
    - Unity Asset Store에서 제공하는 3D 모델 import 
    - 생성형 AI 모델 생성 import
![alt text](image-94.png)

#### 3D 모델 가져오기

- https://poly.pizza/
- https://sketchfab.com/
- https://www.cgtrader.com/ 에서 검색

![alt text](image-95.png)

- 호환되는 파일 포맷
    - FBX : Autodesk 3D(AutoCAD) 교환 포맷. Unity에서 가장호환
    - OBJ : 범용 정적 모델 포맷. Unity에서 사용 가능
    - STL : 3D 프린터용포맷. 비추천
    - BLEND : Blender 원본 파일. 가능(Blender 설치)
    - 3DS : 구형 Autodesk 3D Studio. 사용 가능

- 스캐치팹 사이트 > Conveyer Belt 검색 > 로그인 후 다운로드

- 압축해제, fbx, 텍스처를 프로젝트 Assets 폴더 아래 이동

![alt text](image-97.png)

- Models 폴더에 위치한 Conveyor를 scene 뷰에 드래그

![alt text](image-96.png)

#### 2.3 생산라인

#### 생산품 박스

- Cube 오브젝트로 생성
- 구글에서 `Plastic Normal Map` 검색
- 텍스처 이미지 저장 > Assets > Textures 아래 위치
- Material 생성, Base Map 앞 사각형에 텍스쳐를 드래그

![alt text](image-98.png)

#### 컨베이어 벨트 물리 컴포넌트 적용

- Belt에만 Collider 추가
    - Mesh Colider : 3D 모델 폴리곤 메시 개수만큼 충돌영역 지정. 리소스 부하
    - `Box Colider` : 큐브 형태로 충돌영역을 지정. 리소스 부하 적음

#### 컨베이어 벨트 스크립트

- ConveyorBelt.cs 스크립트 생성
- 충돌이 감지되는 동안 물체이동

```cs
using UnityEngine;  // 유니티 엔진 클래스

// MonoBehaviour C# 스크립트가 기본적으로 상속받는 핵심 클래스
// 개발자코드가 유니티 엔진과 인터렉티브하게 소통할 수 있도록
// 오브젝트에 컴포넌트로 연결, 동작을 제어
public class ConveyorBelt : MonoBehaviour {
    [Header("물체이동 방향")]
    public Vector3 moveDirection = Vector3.right;

    [Header("물체이동 속도")]
    public float speed = 2.0f;
    // 매 프레임 두 충돌영역이 접촉하고 있는 동안 발생
    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.rigidbody; // 충돌감지된 오브젝트의 리지드바디를 가져옴
        if (rb != null)
        {
            rb.linearVelocity = moveDirection.normalized * speed;   // 이동방향으로 속도만큼 이동(값을줌)
        }
    }
}

```
![alt text](image-99.png)

- 컨베이어 오브젝트 중 Collider 컴포넌트를 적용한 벨트에 스크립트 할당

#### Box Spawner 생성

- 박스를 일정시간마다 하나씩 생성하도록 하는 기능
- Product 박스와 컨베이어 모두 프리팹으로 이동

    ![alt text](image-100.png)

- EmptyObject 생성, 위치를 이전 Projuct 위치로 지정
- BoxSpawner.cs 스크립트

```cs
public class BoxSpawner : MonoBehaviour {
    [Header("프리팹 지정")]
    public GameObject prdPrefab;

    [Header("생성 간격")]
    public float interval = 3.0f;

    float timer;

    void Update(){
        timer = Time.deltaTime; // HW 성능별 FPS 고정

        if (timer > interval){
            timer = 0;

            // instant 예제, 샘플
            // Quaternion.identity 회전값이 없는 상태
            Instantiate(prdPrefab,
                        transform.position,
                        Quaternion.identity);
        }
    }
}
```

- Spawner 빈오브젝트에 스크립트 할당

#### 실행결과
- ![컨베이어실행결과](./컨베이어.gif)

#### 컨베이어 벨트 여러개 구성

- 프리팹 드래그 추가

#### 컨베이어 벨트 멈추기 기능

- BoxSpawner.cs 열기
- 로직 변경

```cs
public class ConveyorBelt : MonoBehaviour {
    ...
    [Header("벨트 동작여부")]
    public bool isRunning = true;
    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;

        if (rb == null) return;

        if (!isRunning){
            rb.linearVelocity = Vector3.zero; // 0으로 초기화
            return;
        }
        rb.linearVelocity = moveDirection.normalized * speed;   // 이동방향으로 속도만큼 이동(값을줌)
    }

    public void StopBelt(){
        isRunning = false;
    }

    public void StartBelt(){
        isRunning = true;
    }
}
```

- 벨트 동작여부 체크 확인

![alt text](image-101.png)

- 컨베이어 끝에 센서가 있다고 가정. Collider 트리거가 발생하면 멈춤
- 빈 오브젝트 생성 > Sensor 명명
- Sensor 오프젝트 > Box Collider 컴포넌트 추가. `Is Trigger` 체크
- Edit Collider 아이콘 클릭 위치, 크기 조정

![alt text](image-102.png)

- SensorTrigger.cs 스크립트 생성
- Sensor 객체에 스크립트 추가

```cs
public class SensorTrigger : MonoBehaviour
{
    // 다른 Collider가 들어와서 Trigger 발생하면?
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("제품 감지!");
    }
}
```

- 콘솔로 변경, 시작

```cs
public class SensorTrigger : MonoBehaviour
{
    [Header("컨베이어 1")]
    public ConveyorBelt conveyor1;

    [Header("컨베이어 2")]
    public ConveyorBelt conveyor2;

    private bool isProcessing = false;

    // 다른 Collider가 들어와서 Trigger 발생하면?
    private void OnTriggerEnter(Collider other)
    {
        if (isProcessing) return;

        if (other.CompareTag("Product"))
        {
            // 시간이 걸리는 작업을 여러 프레임에 나눠서 실행하는 기능
            StartCoroutine(Process());
        }
    }

    private IEnumerator Process()
    {
        isProcessing = true;

        Debug.Log("제품 감지!");

        conveyor1.Stop();  // isRunning = false;
        conveyor2.Stop();

        yield return new WaitForSeconds(3.0f);  // 3초동안 대기한 뒤 다음로직으로 

        conveyor1.StartBelt();
        conveyor2.StartBelt();

        yield return new WaitForSeconds(1.0f); 

        isProcessing = false;
    }
}

```

- ConveyorBelt 컨베이어 1번 변수에 Collider 지정된 벨트 객체 할당
- ConveyorBelt 컨베이어 2번 변수에 Collider 지정된 벨트 객체 할당


- Product 프리팹에 `Product` 태그 생성 지정


#### 벨트 동작화면
- ![벨트 동작화면](./KakaoTalk_20260618_150725179.gif)

#### 컨베이어, 스폰 기능 동기화

- 센서가 감지되면 Conveyor와 Spawner를 같이 중지
- BoxSpawner.cs 수정

```cs
void Update()
{
    if (!isRunning) return; // isRunning이 false면 아래 로직 실행
    timer += Time.deltaTime; // HW 성능별 FPS 고정
    if (timer > interval)
    {
        timer = 0;
        Instantiate(prdPrefab,
                    transform.position,
                    Quaternion.identity);
    }
}
public void StopSpawner()
{
    isRunning = false;
}
public void StartSpawner()
{
    isRunning = true;
}
```


- SensorTrigger.cs 수정, 


```cs
[Header("박스 생성기")]
public BoxSpawner spawner;

private bool isProcessing = false;

// 다른 Collider가 들어와서 Trigger 발생하면?
private void OnTriggerEnter(Collider other)
{
    if (isProcessing) return;

    if (other.CompareTag("Product"))
    {
        // 시간이 걸리는 작업을 여러 프레임에 나눠서 실행하는 기능
        StartCoroutine(Process());
    }
}

private IEnumerator Process()
{
    isProcessing = true;

    Debug.Log("제품 감지 - 컨베이어/스폰 중지!");

    conveyor1.StopBelt();  // isRunning = false;
    conveyor2.StopBelt();
    spawner.StopSpawner();

    yield return new WaitForSeconds(3.0f);  // 3초동안 대기한 뒤 다음로직으로 

    conveyor1.StartBelt();
    conveyor2.StartBelt();
    spawner.StartSpawner();

    yield return new WaitForSeconds(1.0f);

    isProcessing = false;
}
```
![alt text](image-127.png)

#### 최종 실행결과

- ![컨베이어2](/컨베이어2.gif)

---

### 2.4 ProBuilder

#### 개요

- Unity에서 건물을 손쉽게 만들 수 있도록 도와주는 패키지

#### 설치

- Windows > Package Manager > Unity Registry에서 ProBuilder 검색 후 설치

![alt text](image-103.png)

#### 사용법

- 메뉴 Tools > ProBuilder > Create Shape > 오브젝트 선택

![alt text](image-104.png)

- Heirarchy 창 > 마우스 오른쪽 > ProBuilder > 오브젝트 선택
- 프로빌더로 생성한 오브젝트 선택 후
- Scene 뷰 > ProBuilder 선택
![alt text](image-113.png)

- 상단 툴바에 프로빌더 아이콘 버튼 추가

![alt text](image-105.png)

- Cube 상태에서...
- Vertex Selection(점 선택), Edge Selection(선 선택), Face Selection(면 선택)
- Move, Rotate, Scale 기능으로 오브젝트 Shape를 변형

![alt text](image-106.png)

- 3D 모델링툴 Blender와 유사한 기능

#### Tip

- 바닥 오브젝트, 기타 오브젝트를 공간없이 
    - Cube에서 V키 누른 상태에서 위치이동

#### 오브젝트 변형법

- Probuilder 큐브 생성
- 변형툴바 Probuilder 선택
- Face Selection 클릭, 앞쪽세로면 선택 Move 기능으로 확장

![alt text](image-107.png)

- Edge Selection 클릭, 왼쪽상단 선 선택 Move 기능으로 축소

![alt text](image-108.png)

- Face.. 클릭, 반대편, 면 클릭, Context Menu, 

![alt text](image-109.png)
![alt text](image-114.png)
- Move, Rotate, Scale 사용 - 모양을 변형
- Face.. 클릭, Cube 상단 클릭
- Shift 누른 상태에서 Scale 조정
![alt text](image-115.png)Probuilder 큐브 생성
변형툴바 Probuilder 선택
Face Selection 클릭, 앞쪽세로면 선택 Move 기능으로 확장


- Context Menu > Extrude Faces 클릭

![alt text](image-111.png)

- Edge.. 클릭 최상후면 선 클릭 > Bezel Edge 선택

![alt text](image-110.png)

- Edge를 여러개 선택 > Bezel Edge 선택

![alt text](image-112.png)
![alt text](image-116.png)

- 바닥에서 1번 마우스 드래그드롭으로 x, z 넓이 생성, 2번 드래그드롭으로 y 1

![alt text](image-117.png)

### 큐브형태 벽 생성

- 기존 큐브 > Face 선택 > Scale 조정
- Shift 누른 상태에서 크기 조정

![alt text](image-118.png)

- 원본 면 크기보다 작게 조정가능
- Move 선택
- Shift 누른 상태에서 위치이동

![alt text](image-119.png)

- 반복잡업, 벽 생성


#### 문, 창문 만들기

- 직교기준 Edge 선택 > Context Menu > Insert Edge Loop 클릭

![alt text](image-120.png)

- 창문, 문 위치 Face 선택 > Move 선택, 창문/문 내려는 방향으로
- Shift 누른 상태에서 이동

![alt text](image-122.png)

- Context Menu > Delete Face 선택
- 반대편 면에서도 Delete Face

![alt text](image-121.png)
![alt text](image-123.png)

#### 재질 적용

- Asset Store Web > Material 검색 > 에셋 추가
- Unity Editor Import 

- Tools > Probuilder > Editors > Material Editor 클릭

![alt text](image-124.png)

#### Material 렌더링 문제

![alt text](image-125.png)

- Window > Rendering > Render Pipline Convert 선택
- Scan

![alt text](image-126.png)

- Convert Assets 버튼 클릭



### Unity Factory  

- Unity Japan에서 제공하는 무료 HDRP 공장 시뮬레이션 에셋
- 공장건물부터 컨베이어라인, 로봇팔, 작업자, 조명 ...
- https://assetstore.unity.com/ko-KR 에서 `Unity Factory` 검색

#### 프로젝트 생성
- HighDefition 3D(HERP) 프로젝트 생성
- My Assets에서 Unity Factory 검색 후 Import

- Import 후 오류 발생
    - SplineContainer 에러
    - Package Manager > Unity Registry, Splines 검색 후 설치
- Input System 오류
    - 키보드, 마우스 입력 시스템이 Unity 6부터 변경
    - 예전 방식 입력시스템 사용
    - Project Settings > Player > Other Settings > Input Handing, both로 바꾸기

- Global Volume을 오브젝트, 사용체크 비활성화

![alt text](image-78.png)

- 기존 Scene을 다른이름으로 재저장
- 계층창 오브젝트를 확인하면서 삭제

![alt text](image-132.png)

#### Splines 애니메이션 기능

- 컨베이어 위 생산품 움직임, 작업자 이동 기능
- 설치한 Splines 기능 사용
- Hierarchy 창 > Create > Spline > 하위 메뉴 선택


![alt text](image-133.png)

![alt text](image-134.png)

- 움직일 오브젝트 선택 > Add component > Spline Animate 추가
- Spline 속성 > 적용한 Spline 지정
- Movement Method Time Duration 변경

#### Product Spline 애니메이션

- TODO : 
- 컨베이어 위 생산품 동작 기능
- 컨베이어 생산라인 매핑되는 Spline 생성
- 이동 후 로봇팔 챔버에 동작하면 동작 멈춤
- 일정시간 로봇팔 애니메이션 발생
- 생산품 Spline 위로 이동
- CustomSplineAnimate.cs 확인 필요


### 2.4 Unity Factory 컨버전

- TODO : 나중에 다시

- Unity Factory HDRP버전, URP 지원안함
- URP 프로젝트 생성, Unity Factory Assets

![alt text](image-128.png)

- Package Manager > `Universal Render Pipline` 검색 후 설치

![alt text](image-129.png)

- URP Assest 생성
- Create > Rendering > URP Asset (with...) 선택

- Edit > Project Settings > Graphics > Default Render Pipeline 값을 HDRP 종류에서 위에서 생성한 URP 에셋으로 변경

![alt text](image-130.png)

- Edit > Project Settings > Quality > Render Pipeline Asset을 URP로 변경

![alt text](image-131.png)

- 머터리얼 변환

- Window > Rendering > Render Pipeline Converter 선택

### 2.4 IoT Simple Project

- Iot Sample Project 에셋
![alt text](image-135.png)


#### 전체 분석

- HVAC - 난방(Heating), 환기(Ventilation), 공기조화(Air Conditioning)의 영문 약자로
    - 밀폐된 공간의 온도, 습도, 공기 질을 종합적으로 제어하는 공조 시스템 및 관련 산업

- Iot Sample Scene 오픈
    - Hierachy 창 맨 위 오브젝트부터 분석 시작

- 프로젝트창
    - IotConnector 폴더 - IndustryCSE.IoT 네임스페이스 사용

- 게임오브젝트 활성화 구분
    - activeSelf - 자기자신이 활성상태 여부
    - activeHierarchy - 부모오브젝트 포함해서 활성상태 여부

1. Canvas UI 클릭
    - Top Menu Bar 아래 구성 확인
    - btn - Cloud 불필요, 사용체크 해제/비활성화
    - Date and Time 오브젝트
        - Date Time Generator 스크립트 더블클릭
        - yyyy-MM-dd HH:mm:ss 포매팅으로 변경

![alt text](image-136.png)

2. ControlSystem - Canvas UI 왼쪽 버튼집합
    - Button - 캔버스 사용시 제일 중요

    ![alt text](image-137.png)
    - On Click() 이벤트 확인
    - AppStates.ToggleOccupancy()
        - AppLogic 빈 그룹오브젝트 연결
        - AppStates.cs가 컴포넌트로 연계

    ![alt text](image-138.png)

    - AppStates.cs 스크립트 분석/수정

    - 한글 폰트 Assets에 복사
    - Window > TextMeshPro > Font Asset Creator 선택
    - 폰트 선택
    - Character Set > Unicode Range(HEX)
    - 한글 입력범위를 `AC00-D7A3` 까지 지정
    - Generate Font Atlas 클릭

    - 완료후 Save As... 리소스 폴더에 저장

    ![alt text](image-141.png)

    - OverlayMode TextMeshPro 컴포넌트에 폰트 지정

    ![alt text](image-140.png)

    - 실행

     ![alt text](image-139.png)
    - AppState에 적용된 폰트 변경 처리

3. Camera
    - CameraController.cs - 물체를 기준으로 화면을 회전, 줌인아웃 기능

4. BrightonOffice 오브젝트
    - 3D Max, Blender 같은 3D 모델링 툴에서 작업한 3D 사무실 모델
    - Brighton_Floor_4 - 분석 의미 없음

5. BrightonOffice.Plane
    - NavMesh Surface

    ![alt text](image-143.png)
    ![alt text](image-144.png)
    
    - NPC나 로봇이 길을 찾기 위해 사용하는 이동가능한 바닥정보
    - 로봇청소기의 경우 프리팹에 길이 셋팅되어 있음. Prefabs > AI BOTS 클릭

    ![alt text](image-145.png)

    - NavMesh - 벽이나 장애물 피하고, 이동가능한 바닥만 따라다니게 미리 계산해놓은 객체
    - agent Type - 어떤 캐릭터가 사용할지 결정
    - Default Area - Walkable(이동가능), Not Walkable(이동불가), Jmp(점프필요)

    - Bake(굽기) 버튼 - 바닥분석 후, 이동가능한 영역 계산 후 새로운 NavMesh 생성
    - 중간에 바닥을 막는 오브젝트가 존재하면 NavMesh가 분리됨

    ![alt text](image-146.png)

    - 문 등의 오브젝트를 제거하고 Clear 후 재 Bake

    ![alt text](image-147.png)


6. IoTDevices - 오피스에 위치하는 IoT 센서 장비들에 대한 설정

7. DeviceSimulator - IoT 시뮬레이션을 위한 더미데이터 생성용 클래스

8. AppLogic - Humanoid, Robot청소기 동작 처리용 오브젝트

    ![alt text](image-148.png)

    - AIAgent - Humanoid 오브젝트 PathWalker 스크립트 컴포넌트에 WayPoit 7개 이미지정
    - WayPoint 위치 변경하면, NabMash Surface를 자동으로 이동

9. MQTT Connector - IoThub.MqttMEssageProvider.cs(MQTT 브로커 연결용), IoTHub.MqttMEssageReader(MQTT 메시지 읽기용) 스크립트로 구성

#### 분석 및 수정 결과화면

- 동영상

#### 2.5 WPF Dummy IoT Data 연동 프로젝트

- IoT Samle Project는 기존 M2Mqttt 라이브러리로 동작 
- MQTTnet으로 진행

#### Unity Project 생성
- URP로 프로젝트 생성
- Asset Store에서 `Low Poly`로 검색 후 Free 클릭
- Pandazole - 로우폴리 에셋 번들

#### 방 구성

- Pandazole Home Interior 에셋으로 구성

#### 한글폰트 설정

<!-- 문서내 링크 -->
[전체분석내 폰트설정](#전체-분석)

#### MQTT 패키지 불러오기

- MQTTNet 은 DLL을 직접 가져와서 구성. MQTTnet 버전 충돌
- MQTT for Unity(M2Mqtt)를 사용

#### M2MqttUnity 설치

- M2Mqtt를 유니티 스크립트로 재정의해서 Unity에서 사용할 수 있게 만든 버전
- https://github.com/gpvigano/M2MqttUnity Code zip으로 다운로드
- 압축해제 후
- Project 창 Assets에 M2MqttUnity 압축해제한 Assets 폴더 복사
- Unity에서 컴파일 진행

- 테스트
![alt text](image-151.png)

- MQTT Publish 메시지 확인

![alt text](image-150.png)

![alt text](image-152.png)

#### Unity MQTT Subscribe 메시지 수신


- SmartHomeMqttClient.cs 작성
- 빈 객체 생성 > MqttClient 명명
- 위 스크립트 컴포넌트 지정
- Inspector에서 필요 데이터 입력, Broker Address, User Name, Password
    - Topic, Status(TextMesh Pro), JSON 데이터 출력 TextMesh Pro 지정

![alt text](image-153.png)

- IoT Sample Project 에셋에서 CameraController.cs 가져오기

- Essentials Pathway 에셋에서 SkyBox 머터리얼 가져와서 적용

#### 전체 실행결과

![전체 실행결과](/KakaoTalk_20260623_120951992.gif)

### 2.9 Unity Factory 컨버전

[웹개발 학습](./README4.md)
