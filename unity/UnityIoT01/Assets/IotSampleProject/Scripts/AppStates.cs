using IndustryCSE.IoT;
using TMPro;
using UnityEngine;

public class AppStates : MonoBehaviour {
    // Serialized fields for UI elements and objects
    [SerializeField] private TMP_Text labelOverlay;
    [SerializeField] private TMP_Text labelApplicationMode;

    [SerializeField] private GameObject messageContainer;

    [SerializeField] private TMP_Text messageDisplay;

    // 앱 상태 토글하는 불리언 함수
    private bool _showOccupancy;            // 좌석점유상태 표시
    private bool _showHVAC;                 // HVAC(공조기) 표시
    private bool _showRoomOccupancy;        //
    private bool _showRoomTemperature;
    private bool _showLocator;

    private string _msgInfoString;

    // 성능을 위해 캐싱된 컴포넌트 배열
    private BaseDevice[] _baseDevices;      // 화면내 모든 IoT디바이스 컴포넌트
    private BaseMessageProvider[] _messageProviders;    // 메시지 공급자 컴포넌트

    [System.Obsolete]
    private void Start()
    {
        Debug.Log("AppStates checking for devices");
        // 성능 최적화용 디바이스, 메시지 캐싱
        // 파라미터 2개짜리 메서드는 Obsolete
        _baseDevices = Object.FindObjectsByType<BaseDevice>(FindObjectsInactive.Include);
        // FindObjectsSortMode.None 파라미터 받는 메서드는 Obsolete
        // _messageProviders = Object.FindObjectsByType<BaseMessageProvider>(FindObjectsSortMode.None);
        _messageProviders = Object.FindObjectsByType<BaseMessageProvider>();
        SubscribeMessageBus();  // 메시지 구독 시작
    }

    private void Update()
    {
        // 메시지 컨테이너가 활성화상태일때 메시지 업데이트
        if (messageContainer.activeSelf)
        {
            messageDisplay.text = _msgInfoString;
        }
    }

    #region Toggle Methods
    public void ToggleOccupancy()
    {
        _showOccupancy = !_showOccupancy;
        SetState("좌석 사용여부", _showOccupancy, IndustryCSE.IoT.DeviceType.Type.Occupancy);
    }

    public void ToggleHVAC()
    {
        _showHVAC = !_showHVAC;
        SetState("HVAC(공조기)", _showHVAC, IndustryCSE.IoT.DeviceType.Type.Thermostat);
    }

    public void ToggleRoomOccupancy()
    {
        _showRoomOccupancy = !_showRoomOccupancy;
        SetState("방 사용여부", _showRoomOccupancy, IndustryCSE.IoT.DeviceType.Type.OccupancyDeviceGroup);
    }

    public void ToggleRoomTemperature()
    {
        _showRoomTemperature = !_showRoomTemperature;
        SetState("방 온도/기기 현황", _showRoomTemperature, IndustryCSE.IoT.DeviceType.Type.ThermostatDeviceGroup);
    }

    // 로케이트 버튼을 클릭하면 토클
    public void ToggleLocate()
    {
        _showLocator = !_showLocator;
        // 메시지컨테이너 활성화 토클(_showLocator가 true면 활성화, false면 비활성화)
        messageContainer.SetActive(_showLocator);
    }

    #endregion

    #region State Management
    private void SetState(string label, bool isActive, IndustryCSE.IoT.DeviceType.Type deviceType)
    {
        HideVisuals();
        SetDeviceVisibility(isActive, deviceType);
        if (isActive)
        {
            labelOverlay.text = label;
        }
        else
        {
            labelOverlay.text = string.Empty;
        }
    }

    private void SetDeviceVisibility(bool isActive, IndustryCSE.IoT.DeviceType.Type deviceType)
    {
        foreach (BaseDevice component in _baseDevices)
        {
            if (component.DeviceType == deviceType)
            {
                component.SetVisibility(isActive);
            }
        }
    }

    private void HideVisuals()
    {
        SetDeviceVisibility(false, IndustryCSE.IoT.DeviceType.Type.Occupancy);
        SetDeviceVisibility(false, IndustryCSE.IoT.DeviceType.Type.Thermostat);
        SetDeviceVisibility(false, IndustryCSE.IoT.DeviceType.Type.OccupancyDeviceGroup);
        SetDeviceVisibility(false, IndustryCSE.IoT.DeviceType.Type.ThermostatDeviceGroup);
    }
    #endregion

    #region Application Mode
    /// <summary>
    /// Switch to simulated data mode.
    /// </summary>
    public void SimulateData()
    {
        labelApplicationMode.text = "Simulated";
        SetMessageProviderMode(true);
    }

    /// <summary>
    /// Switch to IoT data stream mode.
    /// </summary>
    public void ActivateIotBackend()
    {
        labelApplicationMode.text = "Cloud";
        SetMessageProviderMode(false);
    }

    private void SetMessageProviderMode(bool simulate)
    {
        foreach (BaseMessageProvider provider in _messageProviders)
        {
            provider.SetModeAsync(simulate);
        }
    }
    #endregion

    private void SubscribeMessageBus()
    {
        if (IotDeviceMessageReader.Instance != null)
            IotDeviceMessageReader.Instance.MessageBus.Subscribe<DeviceMessage>(OnDeviceMessageReceived);
    }

    private void OnDeviceMessageReceived(IndustryCSE.IoT.Messenger.IMessagePublisher publisher, DeviceMessage msg)
    {
        _msgInfoString = msg.ValueAsString();
    }

}
