#!/usr/bin/python3
import os
import paho.mqtt.client as mqtt
import socket
import json
import datetime

host="broker.mqttdashboard.com"
topic="LUWB/TAG5"
clientID="clientId-23g36U6BJL"

def on_connect(client, userdata, flags, rc):
    print("Connected with result code "+str(rc))
    client.subscribe("topic")

TCP_IP = '192.168.3.3'
TCP_PORT = 2000
BUFFER_SIZE = 1024

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((TCP_IP, TCP_PORT))
s.listen(1)

def postDataOnReceive():
    conn, addr = s.accept()
    data = conn.recv(BUFFER_SIZE)
    if data:
        obj = data.decode('utf-8').replace("'",'"')
        obj = obj.split("keep-alive",1)[1]
        obj = json.loads(obj)
        if str(obj.get('MAC_TAG')) == "TAG5":
            time = int(datetime.datetime.utcnow().timestamp())
            #Mac_Tag;Mac_Anchor;Distance;Unix_Timestamp
            obj = obj.get('MAC_TAG') +';' +  obj.get('MAC_ANCHOR') + ';' + str(obj.get('DISTANCE')) + ';' + str(time)
            client.publish(topic, obj, qos=0)

client=mqtt.Client(client_id=clientID)
client.on_connect = on_connect
client.connect("broker.mqttdashboard.com", 1883)
client.loop_start()
try:
    while True:
        postDataOnReceive()
except KeyboardInterrupt:
    client.loop_stop()

