from threading import Timer
import os

class RepeatedTimer(object):
    def __init__(self, interval, function, *args, **kwargs):
        self._timer     = None
        self.interval   = interval
        self.function   = function
        self.args       = args
        self.kwargs     = kwargs
        self.is_running = False
        self.start()

    def _run(self):
        self.is_running = False
        self.start()
        self.function(*self.args, **self.kwargs)

    def start(self):
        if not self.is_running:
            self._timer = Timer(self.interval, self._run)
            self._timer.start()
            self.is_running = True

    def stop(self):
        self._timer.cancel()
        self.is_running = False

import requests
from time import sleep

def postData():
    #print("postData wordt aangeroepen")
    #server adres
    #final_url="https://iot-ep.azurewebsites.net/api/measurements"
    # postman ip
    final_url="http://192.168.3.1:44308/api/measurements"
    #final_url="http://192.168.3.1:44308"
    folders = []

    #folder names opvragen, dit zijn den MAC's van de tags
    for (path, dirnames, filenames) in os.walk('/var/www/html/data'):
        folders.extend(name for name in dirnames)
        #print(folders)
    #in elke tag zijn folder de afstanden van de anchors uitlezen
    for folder in folders:
        files = []
        file_contents = []

        #print (folder)
        for (path, dirnames, filenames) in os.walk('/var/www/html/data/' + folder):
            files.extend(os.path.join(folder,name) for name in filenames)
        #print(files)
        for file in files:
            with open('/var/www/html/data/' + file) as f:
                 s = f.read()
                 distance, timestamp = s.split(",")
                 f.close()
            split_path = file.split('/')
            if split_path[0] == 'TAG5':
               payload = {'Mac_Tag': split_path[0], 'Mac_Anchor': split_path[1], 'Distance': distance, 'Unix_Timestamp': timestamp}
               print(split_path[1], distance)
               if split_path[1] == 'ANCHOR1':
                  pass
               response = requests.post(final_url,json=payload)
            #print(response.text) #TEXT/HTML
            #print(response.status_code, response.reason) #HTTP
            #file_contents.append(s)
        #print(file_contents)

print ("starting...")
rt = RepeatedTimer(1, postData) # it auto-starts, no need of rt.start()

