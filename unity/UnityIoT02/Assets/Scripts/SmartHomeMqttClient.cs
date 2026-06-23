using M2MqttUnity;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;

public class SmartHomeMqttClient : M2MqttUnityClient {
    [Header("")]
    public string topic = "home/sensor";

    public TMP_Text status; // 현재 상태 표시할 텍스트

    public TMP_Text MsgContent; // IoT Message 출력할 텍스트

    private readonly List<string> receivedMessages = new List<string>();
    private bool connectRequested;

    protected override void Start()
    {
        brokerAddress = "192.168.0.3";
        brokerPort = 1883;
        autoConnect = false;

        if (MsgContent == null)
        {
            Debug.LogWarning("MsgContent is not assigned. Drag a TMP_Text from Content UI.");
        }
        else
        {
            MsgContent.gameObject.SetActive(true);
            MsgContent.text = "브로커 연결 중...";
        }

        if (status != null)
        {
            status.gameObject.SetActive(false);
        }

        base.Start();
        Connect();
    }

    public override void Connect()
    {
        if (connectRequested)
        {
            return;
        }

        connectRequested = true;
        base.Connect();
    }

    protected override void OnConnected()
    {
        connectRequested = false;
        if (MsgContent != null) MsgContent.text = "연결됨. 메시지 대기 중...";
        base.OnConnected();
    }

    protected override void OnConnectionFailed(string errorMessage)
    {
        connectRequested = false;
        if (MsgContent != null) MsgContent.text = "브로커 연결 실패";
        Debug.LogWarning($"MQTT connection failed: {errorMessage}");
        base.OnConnectionFailed(errorMessage);
    }

    protected override void OnConnectionLost()
    {
        connectRequested = false;
        if (MsgContent != null) MsgContent.text = "연결이 끊겼습니다.";
        base.OnConnectionLost();
    }

    protected override void SubscribeTopics()
    {
        // base.SubscribeTopics();  // 부모클래스에 아무런 로직이 없음
        // 토픽으로 구독시작!
        client.Subscribe(
            new string[] { topic },
            new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE }
            );

        if (status != null)
        {
            status.gameObject.SetActive(true);
        }
        Debug.Log($"Subscribed to topic: {topic}");
    }

    protected override void DecodeMessage(string receivedTopic, byte[] message)
    {
        base.DecodeMessage(receivedTopic, message);
        string msg = Encoding.UTF8.GetString(message);
        Debug.Log($"MQTT payload [{receivedTopic}] => {msg}");
        receivedMessages.Add(msg);
    }

    protected override void UnsubscribeTopics()
    {
        // base.UnsubscribeTopics(); // 부모클래스 virtual 메서드에 아무것도 없음
        // 토픽 구독 종료
        client.Unsubscribe(new string[] { topic });
    }

    private void Update()
    {
        base.Update();  // 부모클래스는 MQTT 관련 처리 진행

        if (receivedMessages.Count > 0) // MQTT 메시지가 넘어왔으면
        {
            string msg = receivedMessages[0];
            receivedMessages.RemoveAt(0);

            if (!string.IsNullOrWhiteSpace(msg) && MsgContent != null)
            {
                if (!MsgContent.gameObject.activeSelf)
                {
                    MsgContent.gameObject.SetActive(true);
                }
                MsgContent.text = msg;  // Canvas 메시지 출력 텍스트에 IoT json
            }
        }
    }
}