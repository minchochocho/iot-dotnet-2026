## 동영상 물체 인식
## 기본 OS운영
from io import BytesIO
import base64
import json

## 이미지처리
from PIL import Image, ImageDraw, ImageFont

## 영상처리, 물체인식 패키지
import numpy as np
import cv2
from ultralytics import YOLO
from ultralytics.engine.results import Results

## MQTT
import paho.mqtt.client as mqtt

### 함수 선언
def detectObjects(image: np.array):
    objects = model(image)
    classNames = model.names

    for obj in objects:
        boxes = obj.boxes.xyxy
        confidences = obj.boxes.conf
        class_ids = obj.boxes.cls

        for (box, conf, class_id) in zip(boxes, confidences, class_ids):
            x1, y1, x2, y2 = map(int, box)
            label = classNames[int(class_id)]

            cv2.rectangle(image, (x1,y1), (x2,y2), (0,0,255), thickness=2)
            cv2.putText(image, f'{label} {conf:.2f}', (x1+7, y2-7),
                        cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0,0,255),2 )



model = YOLO('yolov8m.pt')

## CV2 초기화
w, h = 540, 360
api = cv2.CAP_DSHOW #window DirectX Show

# VideoCapture 시작 - 0:웹캠 또는 동영상 경로
# 일반 동영상은 DirectShow가 불가, 웹캠만 가능
cap = cv2.VideoCapture('./traffic_test.mp4')
# cap = cv2.VideoCapture(0,api)
cap.set(cv2.CAP_PROP_FRAME_WIDTH, w) # 웹캡 width 640으로 고정
cap.set(cv2.CAP_PROP_FRAME_HEIGHT, h) # 웹캠 height를 360으로 고정

while cap.isOpened():
    ret, frame = cap.read()
    # frame = cv2.resize(frame, (w,h))    # 동영상의 크기 조절

    # if not ret:break    # 동영상이 열리지 않으면 종료
    if not ret: # 무한반복
        cap.set(cv2.CAP_PROP_POS_FRAMES, 0)
        continue

    resultImg = detectObjects(frame)

    cv2.imshow('Result',frame)

    if cv2.waitKey(3) & 0xFF == ord('q'): break

# 리소스 해제
cap.release()
cv2.destroyWindow()
