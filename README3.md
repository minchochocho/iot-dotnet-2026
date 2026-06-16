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

---
### 2.3 Unity Factory 

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