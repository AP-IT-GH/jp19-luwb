from pypozyx import PozyxSerial, get_first_pozyx_serial_port, Coordinates

#Change the remote ID for positioning another device
r_id = 0x673a

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

