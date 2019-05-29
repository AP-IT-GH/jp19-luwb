#!/usr/bin/python3
from configparser import ConfigParser

config= ConfigParser()

obj = []
obj.append(input("What is the socket tcp ip? "))
obj.append(input("What is the socket tcp port? "))
obj.append(input("What is mqtt host? "))
obj.append(input("What is mqtt topic? "))

config['default'] = {'tcp_ip': obj[0],'tcp_port':obj[1],'host':obj[2],'topic':obj[3]}
with open('config.ini','+w') as f:
	config.write(f)
