from pypozyx import PozyxSerial, get_first_pozyx_serial_port, Coordinates
from configparser import ConfigParser

#read setup file
parser = ConfigParser()
parser.read('config.ini')

#Change the remote ID for positioning from another device from config file
r_id = parser.get('default','remote_id')

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
    while True:
        pozyx.doPositioning(positionTag, remote_id=r_id)
        print(positionTag)

#Exception exit
except:
    print("")
    print("Exit program")

