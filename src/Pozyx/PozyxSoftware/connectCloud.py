# Dit script zal verbinden over de cloud met de pozyx software

import paho.mqtt.client as mqtt
import ssl

host = "mqtt.cloud.pozyxlabs.com"
port = 443
topic = "5c76ad123d5f08cd07ada5bd"  # your mqtt topic
username = "5c76ad123d5f08cd07ada5bd"  # your mqtt username
password = "e92ef727-9bb6-4e67-8c8b-2e928c713b77"  # your generated api key

def on_connect(client, userdata, flags, rc):
    print(mqtt.connack_string(rc))

# Callback triggered by a new Pozyx data packet
def on_message(client, userdata, msg):
    print("Positioning update:", msg.payload.decode())

def on_subscribe(client, userdata, mid, granted_qos):
    print("Subscribed to topic!")

client = mqtt.Client(transport="websockets")
client.username_pw_set(username, password=password)

# sets the secure context, enabling the WSS protocol
client.tls_set_context(context=ssl.create_default_context())

# set callbacks
client.on_connect = on_connect
client.on_message = on_message
client.on_subscribe = on_subscribe
client.connect(host, port=port)
client.subscribe(topic)

# works blocking, other, non-blocking, clients are available too.
client.loop_forever()
