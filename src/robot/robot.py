#!/usr/bin/python3
import requests

endPos = False

apiUrl = "https://luwb-api.azurewebsites.net/api/tags"
apiTag =  "TAG1"

def driveForward():
	
	
def driveBackward():
	
	
	
def stopMoving():
	
	
	

while !endPos:
	response = requests.get(apiUrl,
    params={'mac': {apiTag}})
    obj = response.json()
    tag = obj[0]
    xPos = tag.get("xPos")
    yPos = tag.get("yPos")




