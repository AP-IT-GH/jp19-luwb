#!/usr/bin/python3
import os
import paho.mqtt.client as mqtt
import socket
import datetime
import re
from configparser import ConfigParser

#host="broker.mqttdashboard.com"
#topic="LUWB/TAG5"
clientID="clientId-23g36U6BJL"

#TCP_IP = '192.168.3.3'
#TCP_PORT = 2000
BUFFER_SIZE = 1024

parser = ConfigParser()
parser.read('config.ini')
TCP_IP = parser.get('default','tcp_ip')
TCP_PORT = int(parser.get('default','tcp_port'))
host = parser.get('default','host')
topic = parser.get('default','topic')

def on_connect(client, userdata, flags, rc):
    print("Connected with result code "+str(rc))
    client.subscribe("topic")

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((TCP_IP, TCP_PORT))
s.listen(1)

def postDataOnReceive():
    conn, addr = s.accept()
    data = str(conn.recv(BUFFER_SIZE))
    if data:
        data=data.replace('\\n','') #Deze lijn is enkel nodig bij het testen met postman
        data=data.replace(' ','') #Deze lijn is enkel nodig bij het testen met postman
        data=data.replace('\\x0','').replace('\\x00','') #Deze lijn is nodig voor de anchors
        objIndex = data.find('"MAC_TAG')
        data=data[objIndex:-1]
        data=data.split(',')
        obj = ["","",""]
        if data[0] and data[1] and data [2]:
            obj[0]=data[0].strip()[len('"MAC_TAG":"'):-1]
            obj[1]=data[1].strip()[len('"MAC_ANCHOR":"'):-1]
            obj[2]=data[2].strip()[len('"DISTANCE":'):]
            getal = [int(d) for d in re.findall(r'-?\d+', obj[2])]
            obj[2]= str(getal[0])
            #if str(obj.get('MAC_TAG')) == "TAG5": ###Indien je enkel 1 bepaalde TAG wilt doorsturen
            time = int(datetime.datetime.utcnow().timestamp())
            #Notatie: Mac_Tag;Mac_Anchor;Distance;Unix_Timestamp
            obj = obj[0] + ';' + obj[1] + ';' + obj[2] + ';' + str(time)
            print("Object send: " + obj)
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
    s.close()
