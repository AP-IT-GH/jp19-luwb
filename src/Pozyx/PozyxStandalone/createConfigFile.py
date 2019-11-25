#!/usr/bin/python3
from configparser import ConfigParser

config= ConfigParser()
cParser = ConfigParser()
cParser.read('config.ini')

class Anchor:
	def __init__(self):
		self.id = 0
		self.x = 0
		self.y = 0
		self.z = 0
	def get_id(self):
		return self.id
	def get_x(self):
		return self.x
	def get_y(self):
		return self.y
	def get_z(self):
		return self.z
	def set_id(self,id):
		self.id = id
	def set_x(self,x):
		self.x = x
	def set_y(self,y):
		self.y = y
	def set_z(self,z):
		self.z = z

settings = ["",""]
settings[0] = cParser.get('default','connected_id')
settings[1] = cParser.get('default','remote_id')

anchors = []

for anchor in range(4):
  anchors.append(Anchor())

for index, anchor in enumerate(anchors):
	anchor.set_id(cParser.get('default','anchor' + str(index+1) + '_id'))
	anchor.set_x(cParser.get('default','anchor' + str(index+1) + '_X'))
	anchor.set_y(cParser.get('default','anchor' + str(index+1) + '_Y'))
	anchor.set_z(cParser.get('default','anchor' + str(index+1) + '_Z'))

changeConnected = input("Do you want to change the connected device? Y/N ").lower()
if changeConnected == 'y':
	settings[0] = input("What is the ID of the connected device? ")
changeRemote = input("Do you want to change the device to execute the sripts on? Y/N ").lower()
if changeRemote == 'y':
	settings[1] = input("What is the ID of the device to execute the scripts on? ")

for index, anchor in enumerate(anchors):
	changeAnchor = input("Do you want to change the settings of anchor " + str(index+1) + "? Y/N ").lower()
	if changeAnchor == 'y':
		anchor.set_id = input("What is the ID of anchor " + str(index+1) + "? ")
		anchor.set_x = input("What is the X-coordinate of anchor " + str(index+1) + "? ")
		anchor.set_y = input("What is the Y-coordinate of anchor " + str(index+1) + "? ")
		anchor.set_z = input("What is the Z-coordinate of anchor " + str(index+1) + "? ")

config['default'] = {'connected_id': settings[0],'remote_id':settings[1],
'anchor1_id':anchors[0].get_id(),'anchor1_X':anchors[0].get_x(),'anchor1_Y':anchors[0].get_y(),'anchor1_Z':anchors[0].get_z(),
'anchor2_id':anchors[1].get_id(),'anchor2_X':anchors[1].get_x(),'anchor2_Y':anchors[1].get_y(),'anchor2_Z':anchors[1].get_z(),
'anchor3_id':anchors[2].get_id(),'anchor3_X':anchors[2].get_x(),'anchor3_Y':anchors[2].get_y(),'anchor3_Z':anchors[2].get_z(),
'anchor4_id':anchors[3].get_id(),'anchor4_X':anchors[3].get_x(),'anchor4_Y':anchors[3].get_y(),'anchor4_Z':anchors[3].get_z()}
with open('config.ini','+w') as f:
	config.write(f)
