#!/usr/bin/python3
import os
import paho.mqtt.client as mqtt
from time import sleep

host="broker.mqttdashboard.com"
topic="LUWB/TAG5"
clientID="clientId-23g36U6BJL"

def on_connect(client, userdata, flags, rc):
    print("Connected with result code "+str(rc))
    client.subscribe("topic")

def postData():
    folders = []
    #folder names opvragen, dit zijn de MAC's van de tags
    for (path, dirnames, filenames) in os.walk('/var/www/html/data'):
        folders.extend(name for name in dirnames)
    #in elke tag zijn folder de afstanden van de anchors uitlezen
    for folder in folders:
        files = []
        file_contents = []
        for (path, dirnames, filenames) in os.walk('/var/www/html/data/' + folder):
            files.extend(os.path.join(folder,name) for name in filenames)
        for file in files:
            with  open('/var/www/html/data/' + file) as f:
                s = f.read()
                distance, timestamp = s.split(",")
                f.close()
            split_path = file.split("/")
            if split_path[0] == 'TAG5':
                #Mac_Tag;Mac_Anchor;Distance;Unix_Timestamp
                payload = split_path[0] + ';' + split_path[1] + ';' + distance + ';' + timestamp
                #print(payload)
                client.publish(topic, payload, qos=0)

client=mqtt.Client(client_id=clientID)
client.on_connect = on_connect
client.connect("broker.mqttdashboard.com", 1883)
client.loop_start()
try:
    while True:
        sleep(0.2)
        postData()
except KeyboardInterrupt:
    client.loop_stop()

