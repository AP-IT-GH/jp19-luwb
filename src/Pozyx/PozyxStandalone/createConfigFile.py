#!/usr/bin/python3
import configparser

config= configparser.ConfigParser()

obj = []
obj.append(input("What is the ID of the connected device? "))
obj.append(input("What is the ID of the device to execute the scripts on? "))
obj.append(input("What is the ID of anchor 1? "))
obj.append(input("What is the X-coordinate of anchor 1? "))
obj.append(input("What is the Y-coordinate of anchor 1? "))
obj.append(input("What is the Z-coordinate of anchor 1? "))
obj.append(input("What is the ID of anchor 2? "))
obj.append(input("What is the X-coordinate of anchor 1? "))
obj.append(input("What is the Y-coordinate of anchor 1? "))
obj.append(input("What is the Z-coordinate of anchor 1? "))
obj.append(input("What is the ID of anchor 3? "))
obj.append(input("What is the X-coordinate of anchor 1? "))
obj.append(input("What is the Y-coordinate of anchor 1? "))
obj.append(input("What is the Z-coordinate of anchor 1? "))
obj.append(input("What is the ID of anchor 4? "))
obj.append(input("What is the X-coordinate of anchor 1? "))
obj.append(input("What is the Y-coordinate of anchor 1? "))
obj.append(input("What is the Z-coordinate of anchor 1? "))

config['default'] = {'connected_id': obj[0],'remote_id':obj[1],
'anchor1_id':obj[2],'anchor1_X':obj[3],'anchor1_Y':obj[4],'anchor1_Z':obj[5],
'anchor2_id':obj[6],'anchor2_X':obj[7],'anchor2_Y':obj[8],'anchor2_Z':obj[9],
'anchor3_id':obj[10],'anchor3_X':obj[11],'anchor3_Y':obj[12],'anchor3_Z':obj[13],
'anchor4_id':obj[14],'anchor4_X':obj[15],'anchor4_Y':obj[16],'anchor4_Z':obj[17],}
with open('config.ini','+w') as f:
	config.write(f)
