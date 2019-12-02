#!/usr/bin/python3
from pypozyx import PozyxSerial, get_first_pozyx_serial_port, Coordinates
from configparser import ConfigParser
import paho.mqtt.client as mqtt
import requests

#read config files
cParser = ConfigParser()
cParser.read('config.ini')
eParser = ConfigParser()
eParser.read('export.ini')

#Change the remote ID for positioning from another device from config file
r_id = int(cParser.get('default','remote_id'),16)

#Set Export properties
exportFile = eParser.get('default','export_file')
fileName = eParser.get('default','file_name')
exportMQTT = eParser.get('default','export_mqtt')
host = eParser.get('default','mqtt_host')
topic = eParser.get('default','mqtt_topic')
clientID = eParser.get('default','mqtt_clientid')
exportAPI = eParser.get('default','export_api')
apiUrl = eParser.get('default','api_url')
apiTag = eParser.get('default','api_tag')

#Setup variables
positionTag = Coordinates()

#Check if connected
serial_port = get_first_pozyx_serial_port()
if serial_port is not None:
    pozyx = PozyxSerial(serial_port)
    print("Connection success!")
else:
    print("No Pozyx port was found")
    exit()

try:
    if exportFile is "y": f = open("position.txt", "a+")
    if exportMQTT is "y":
        client = mqtt.Client(client_id=clientID)
        client.connect("broker.mqttdashboard.com")
        client.loop_start()
    while True:
        pozyx.doPositioning(positionTag, remote_id=r_id)
        publishString = hex(r_id) + ": " + str(positionTag)
        print(publishString)
        if exportFile is "y": f.write(publishString + "\n")
        if exportMQTT is "y": client.publish(topic,publishString)
        if exportAPI is "y":
            pos = str(positionTag).split(" ")
            xPos = pos[1]
            yPos = pos[3]
            zPos = pos[5]
            print(xPos + yPos + zPos)
            apiData = {'mac': apiTag,'xpos': xPos, 'ypos': yPos, 'zpos': zPos}
            requests.put(apiUrl, params = apiData)
        
#Exception exit
except:
    if exportFile is "y": f.close()
    if exportMQTT is "y": client.loop_stop()
    print("")
    print("Exit program")

