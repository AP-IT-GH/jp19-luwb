#!/usr/bin/python3
import configparser

config= configparser.ConfigParser()

obj = []
obj.append(input("Want to export positioning data to a file?(y/n) "))
obj.append(input("What is the file name? "))
obj.append(input("Want to export via MQTT?(y/n) "))
obj.append(input("What is the MQTT host? "))
obj.append(input("What is the MQTT Topic? "))
obj.append(input("What is the MQTT ClientID? "))


config['default'] = {'export_file': obj[0],'file_name':obj[1],'export_mqtt':obj[2],'mqtt_host':obj[3],'mqtt_topic':obj[4],'mqtt_clientid':obj[5]}
with open('export.ini','+w') as f:
	config.write(f)
