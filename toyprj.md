# 웹 통합 토이 프로젝트

## 국가교통정보센터 CCTV 정보 앱 

## 개요

국가교통정보센터에서 제공하는 OpenAPI를 통합해서 운영하는 RESTAPI서비스와 모니터링 앱 통합개발

- 국가교통정보센터 OpenAPI

### 사용기술

- C# 14(.NET 10.0)
- WPF
- OpenAPI 사용 + Wrapping RESTAPI 서비스
- ProgressBar?
- Newtonsoft.json
- ITS 국가교통정보센터 OpenAPI - [링크](https://www.its.go.kr/)
- 경찰청 도시교통정보센터 OpenAP - [링크](https://www.utic.go.kr/)
- MahApps.Metro? - 

### 개발환경 설정

#### 국가교통정보센터 사이트 회원가입

##### 로그인 후 인증키 신청

- 오픈데이터 > 오픈데이터 목록 > CCTV 화상자료
- 인증키 신청 버튼

![alt text](image-229.png)

![alt text](image-228.png)

##### 마이페이지 확인

![alt text](image-230.png)

#### Visual Studio

##### WPF 앱 프로젝트 생성

![alt text](image-231.png)

#### 동영상 플레이 라이브러리

- 실시간 스트리밍(HLS), 동영상(mp4) 모두 재생이 가능한 라이브러리 필요
- WPF MediaElement - HLS 재생 어려움, mp4 재생가능, 별도 이미지처리
- WebView2 - HLS 확인필요, mp4 재생가능, 별도 이미지처리
- FFME - HLS, mp4 가능. 이미지 별도
- `LibVLCSharp`.WPF - HLC, mp4 가능. 이미지 별도

##### VLC

VideoLAN Organization에서 제공하는 크로스 플랫폼 멀티미디어 재생툴

스트리밍, 동영상 재생, 이미지 로드 가능

[링크](https://www.videolan.org/)

![alt text](image-232.png)

##### NuGet 패키지 설치

- Newtonsoft.Json
- LibVLCSharp.WPF

### 화면 UI

#### 와이어프레임

![alt text](image-233.png)

### 기본 구현

#### 메인화면 디자인

![alt text](image-234.png)

#### 앱 구조 설계

- Common - 공통 함수나 공통 변수 네임스페이스(폴더)
- Model - OpenAPI Json 데이터 구조 모델 클래스 네임스페이스
- Services - OpenAPI 서비스 동작 클래스 네임스페이스

##### 앱 구조별 구현

- Commmon/AppCommon.cs

#### 앱 구조 설계

##### 앱 구조별 구현

- Common/AppCommon.cs - [소스](./toyproject/ToyProjects01/WpfCctvMonitorApp/Common/AppCommon.cs)
- Models/CctvInfo.cs - [소스](./toyproject/ToyProjects01/WpfCctvMonitorApp/Model/CctvInfo.cs)
- Services/ItsCctvService.cs - [소스](./toyproject/ToyProjects01/WpfCctvMonitorApp/Services/ItsCctvService.cs)

##### 화면에 VLC 라이브러리 추가

```xml
<!-- vlc 네임스페이스 추가 -->
<Window x:Class="WpfCctvMonitorApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:vlc="clr-namespace:LibVLCSharp.WPF;assembly=LibVLCSharp.WPF"
        ... >
```

##### 기본구현

- 로딩 후 스트리밍 테스트


##### 비즈니스 로직에 따라 구현

type `실시간`, 동영상, 정지영상 모두 같은 CCTV를 표현하는 방법만 다름

0. App.config에서 API key 로드
1. 고속도로/국도 선택
2. 지역 검색 - 지역별 최소/최대 위도, 최대/최소 경도 확인
     - 지역 선택으로 간결화
3. 상세필터 - 시/도로 최대/최소 위도와 경도 확인. (노선, 방향은 삭제)
4. 검색 - OpenAPI URL로 위경도 범위별 CCTV 조회
5. CCTV 목록 - 리스트
6. 리스트아이템 클릭 - CCTV 영상 플레이
7. 지도 영역 - CCTV 위치 지도위에 표시
8. CCTV 정보 - json 결과 추출 표시

##### App.config

![alt text](image-236.png)

- xml로 구성된 파일
- [소스](./toyproject/ToyProjects01/WpfCctvMonitorApp/App.config)

##### UI 변경

- CCTV목록 페이징 삭제
- 지역 검색 삭제
- 시/도 선택 -> 지역 선택 변경
- 노선, 방향 선택 삭제

##### GeoBound 클래스 생성

- 지역 선택시 최소, 최대 위도/경도를 할당해주는 클래스 - [소스](./toyproject/ToyProjects01/WpfCctvMonitorApp/Common/GeoBound.cs)
- 지역 선택 콤보박스에 로직 추가

##### 검색 버튼 작성

- BtnSearch 명명 및 로직 추가
- ItsCctvService.cs - [소스](./toyproject/ToyProjects01/WpfCctvMonitorApp/Services/ItsCctvService.cs)
- json 매핑 모델 클래스. CctvResponse.cs - [소스](./toyproject/ToyProjects01/WpfCctvMonitorApp/Model/CctvResponse.cs)

##### 중간결과 화면

![alt text](image-237.png)

- 도로구분 선택, 지역 선택 후 검색. CCTV 리스트 갯수 출력

##### CCTV 목록 사용자컨트롤

##### 리스트뷰 클릭 스트리밍

##### 지도표시

##### CCTV 상세정보

##### 상태표시줄 작업

##### 즐겨찾기 DB 추가

##### 즐겨찾기 읽어오기

##### 프로그래스바

##### 전체 검색 개수

- 6000개?

### UI 변경

- MahApps.Metro 또는 WPF UI
- Light/Dark Theme

### OpenAPI 래핑 웹서비스