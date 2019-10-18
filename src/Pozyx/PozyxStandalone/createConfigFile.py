#!/usr/bin/python3
from configparser import ConfigParser

config= ConfigParser()

obj = []
obj.append(input("What is the ID of the connected device? "))
obj.append(input("What is the ID of the device to execute the scripts on? "))
obj.append(input("What is the ID of anchor 1? "))
obj.append(input("What are the coordinates of anchor 1 in X,Y,Z format? "))
obj.append(input("What is the ID of anchor 2? "))
obj.append(input("What are the coordinates of anchor 2 in X,Y,Z format? "))
obj.append(input("What is the ID of anchor 3? "))
obj.append(input("What are the coordinates of anchor 3 in X,Y,Z format? "))
obj.append(input("What is the ID of anchor 4? "))
obj.append(input("What are the coordinates of anchor 4 in X,Y,Z format? "))

config['default'] = {'connected_id': obj[0],'remote_id':obj[1],'anchor1_id':obj[2],'anchor1_coordinates':obj[3],'anchor2_id':obj[4],'anchor2_coordinates':obj[5],'anchor3_id':obj[6],'anchor3_coordinates':obj[7],'anchor4_id':obj[8],'anchor4_coordinates':obj[9]}
with open('config.ini','+w') as f:
	config.write(f)
