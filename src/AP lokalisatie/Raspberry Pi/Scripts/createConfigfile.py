#!/usr/bin/python3
from configparser import ConfigParser

config= ConfigParser()

obj = []
obj.append(input("What is the socket tcp ip? "))
obj.append(input("What is the socket tcp port? "))
obj.append(input("What is mqtt host? "))
obj.append(input("What is mqtt topic? "))
obj.append(input("What is the client id? "))
obj.append(input("What is the API url if needed?"))

config['default'] = {'tcp_ip': obj[0],'tcp_port':obj[1],'host':obj[2],'topic':obj[3],'clientid':obj[4],'api_url':obj[5]}
with open('config.ini','+w') as f:
	config.write(f)
