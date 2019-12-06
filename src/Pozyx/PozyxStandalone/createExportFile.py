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
obj.append(input("Want to export via API?(y/n) "))
obj.append(input("What is the API URL? "))
obj.append(input("What is the API MAC Name? "))


config['default'] = {'export_file': obj[0],'file_name':obj[1],
					 'export_mqtt':obj[2],'mqtt_host':obj[3],'mqtt_topic':obj[4],'mqtt_clientid':obj[5],
					 'export_api':obj[6],'api_url':obj[7],'api_tag':obj[8]}
with open('export.ini','+w') as f:
	config.write(f)
