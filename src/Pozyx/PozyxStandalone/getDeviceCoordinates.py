from pypozyx import PozyxSerial, get_first_pozyx_serial_port, Coordinates

#Change the remote ID's for getting coordinates of another device
device1 = 0x6642
device2 = 0x671f
device3 = 0x6734
device4 = None

#Setup variables
positionDevice1 = Coordinates()
positionDevice2 = Coordinates()
positionDevice3 = Coordinates()
positionDevice4 = Coordinates()

#Check if connected
serial_port = get_first_pozyx_serial_port()
if serial_port is not None:
    pozyx = PozyxSerial(serial_port)
    print("Connection success!")
else:
    print("No Pozyx port was found")
    exit()

#Obtain the device Coordinates
pozyx.getCoordinates(positionDevice1, device1)
print(positionDevice1)
pozyx.getCoordinates(positionDevice2, device2)
print(positionDevice2)
pozyx.getCoordinates(positionDevice3, device3)
print(positionDevice3)
pozyx.getCoordinates(positionDevice4, device4)
print(positionDevice4)


#Exception exit
except:
    print("")
    print("Exit program")
