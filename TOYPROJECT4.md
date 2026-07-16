# 토이 프로젝트

출처 : 자바 스프링부트 프로젝트와 파이썬 AI 프로젝트 연결하기(허진경 / 부크크)로 학습

## AI 비전검사 시스템

![alt text](image-274.png)

### Python WebAPI 서비스

- Python 웹 라이브러리/프레임워크
    - 오픈소스이기에 라이브러리 프레임워크가 엄청 많음
    - Flask - 가볍고 필요한 기능만 제공하는 소규모 프로젝트용 웹
    - `FastAPI` - REST API에 최적화 된 웹. 매우 빠름, 난이도 중
    - Django - 모든 기능을 제공하는 대형 프레임워크.
    - Pyramid - 중대형 프로젝트용 프레임워크. 난이도 상
    - Falcon - REST API 전용
    - Bottle - 초경량, 난이도 하

- 웹 서버(실행 서버)
    - `Uvicorn` - FastAPI 실행 서버

#### 파이썬 가상환경 설치

```bash
> python -m venv .venv
```

- 가상환경 실행
```bash
.\.venv\Scripts\activate.ps1
```

- .gitignore에 python 관련 설정 추가

#### 기본 패키지 설치

```bash
> pip install fastapi uvicorn
```

### Python AI 물체인식

### WebAPI

### 연동

### 비전검사

