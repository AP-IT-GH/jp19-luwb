from pypozyx import PozyxSerial, get_first_pozyx_serial_port, UWBSettings

serial_port = get_first_pozyx_serial_port()

if serial_port is not None:
    pozyx = PozyxSerial(serial_port)
    uwb_settings = UWBSettings()
    pozyx.getUWBSettings(uwb_settings)
    print(uwb_settings)
else:
    print("No Pozyx port was found")
