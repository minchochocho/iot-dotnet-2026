using System;
using UnityEngine;
using UnityEngine.UI;

namespace IndustryCSE.IoT {
    public class DateTimeGenerator : MonoBehaviour {
        [SerializeField] Text _dateTimeText;
        private DateTime _nextLogTime;

        private void Start()
        {
            _nextLogTime = DateTime.Now;
        }

        // Update is called once per frame
        void Update()
        {
            // nextLogTime 보다 현재 시분초가 더 최신이면
            if (DateTime.Now >= _nextLogTime)
            {
                // 현재 날짜를 변경
                DateTime currentDateTime = DateTime.Now;

                // 원본 포맷팅
                //_dateTimeText.text = currentDateTime.ToString("HH:mm ddd dd MMM yyyy");
                _dateTimeText.text = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                // Update next log time
                // 다음 로그타임 1분으로
                _nextLogTime = DateTime.Now.AddMinutes(1);
            }
        }
    }
}


