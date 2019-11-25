from pypozyx import PozyxSerial, get_first_pozyx_serial_port, Coordinates

serial_port = get_first_pozyx_serial_port()

if serial_port is not None:
    pozyx = PozyxSerial(serial_port)
    print("Connection success!")
else:
    print("No Pozyx port was found")

position = Coordinates()
pozyx.doPositioning(position)
print(position)
with open('/home/pi/pozyx/position.txt','a') as f:
        f.write(str(position) + "\n")

