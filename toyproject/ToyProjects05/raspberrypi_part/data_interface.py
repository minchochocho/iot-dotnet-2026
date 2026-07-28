# data_interface.py 
# arduino, raspberry pi, windows interface python code

import json
import time
import threading
from datetime import datetime

import serial
import paho.mqtt.client as mqtt

## mqtt init
pub_id = 'iot52-rpi'
broker = '210.119.12.52'     # 본인 아이피
port = 1883

mqtt_username = 'root'
mqtt_password = 'mqtt123456'

# publish topic : arduino -> rpi -> win
data_topic = 'smartfactory/52/data'

# subcribe topic : win -> rpi -> arduino
control_topic = 'smartfactory/52/control'

## serial communication init
serial_port = '/dev/tty_acm0'
baud_rate = 19200

arduino = None
running = True
serial_lock = threading.lock()

## data transfer to arudino
def send_to_arduino(command: str):
    # from mqtt command to aruino
    if arduino is None or not arduino.is_open:
        print('arduino seral port is not open')
        return

    command = command.strip()
    if not command: return

    arduino.write(f'{command}\n'.encode('utf-8'))
    print(f'[serial tx] {command}')    

## mqtt on_method events
# connection
def on_connect(client, userdata, flags, reason_code, properties=None):
    if reason_code == 0:
        print('mqtt connected')

        client.subscribe(control_topic, qos=1)        
        print(f'mqtt subscribed: {control_topic}')
    else:
        print('mqtt connection failed')

# disconnection
def on_disconnect(client, userdata, disconnect_flags, reason_code, properties=None):
    print(f'mqtt disconnected: {reason_code}')

# message receive
def on_message(client, userdata, message):
    # todo
    print(f'[mqtt sub] topic={message}')

    # command process
    command = ''
    # 
    send_to_arduino(str(command))

# publish ardino data by mqtt
def publish_arduino_data(client, serial_data: str):
    try:
        data = json.loads(serial_data)

    except json.json_decode_error:
        # r, g, b plain text
        data = serial_data

    payload = {
        'device_id': pub_id,
        'timestamp': datetime.now(),
        'data': data
    }

    json_payload = json.dumps(
        payload,
        ensure_ascii=False
    )

    client.publish(data_topic, payload=json_payload, qos=1)
    print(f'[mqtt pub] {json_payload}')


### main function
def main():
    global arduino
    client = None

    try:
        # arduino connect
        arduino = serial.serial(
            port=serial_port,
            baudrate=baud_rate,
            timeout=1
        )

        # reboot need
        time.sleep(2)
        arduino.reset_input_buffer()
        print(f'arduino connected : {serial_port}')

        # mqtt client 
        client = mqtt.client(client_id=pub_id, protocol=mqtt.mqt_tv5, userdata=none)
        client.username_pw_set(username=mqtt_username, password=mqtt_password)

        client.on_connect = on_connect
        client.on_disconnect = on_disconnect
        client.on_message = on_message

        client.connect(broker, port, keepalive=60)
        client.loop_start()

        print(f'publish topic : {data_topic}')
        print(f'subscribe topic : {control_topic}')

        # arduino data receive
        while True:
            if arduino.in_waiting > 0:
                serial_data = arduino.readline().decode(
                    'utf-8',
                    errors='ignore'
                ).strip()

                if serial_data:
                    print(f'[serial rx] {serial_data}')
                    # mqtt publish
                    publish_arduino_data(client, serial_data)

            time.sleep(0.01)

    except KeyboardInterrupt:
        print('\n_program quit')

    except serial.serial_exception as error:
        print(f'serial error : {error}')

    except Exception as error:
        print(f'error : {error}')

    finally:
        # release mqtt
        if client is not None:
            client.loop_stop()
            client.disconnect()

        # release arduino(serial)
        if arduino is not None and arduino.is_open:
            arduino.close()

        print('program exit')

if __name__ == '__main__':
    main()