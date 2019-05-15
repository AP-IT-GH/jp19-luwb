# 1. Installeren van Apache2 en instellen voor gebruik index.php
## 1.1 Installeren van Apache2
1. sudo apt-get update
2. sudo apt-get install apache2  -y
3. test de webserver door naar de website http://localhost/ te gaan
## 1.2 Apache2 instellen voor PHP
1. sudo apt-get install php libapache2-mod-php -y
2. cd /var/www/html
3. Plaats hier het index.php bestand (aanmaken en kopiëren via sudo touch index.php / sudo nano index.php)
4. sudo rm index.html
5. sudo service apache2 restart
## 1.3 Aanpassen eigendomsrechten voor index.php script
1. sudo chown -R www-data:www-data /var/www/html
Indien de fout Unable to open file! tevoorschijn komt probeer volgende commando's:
- sudo chmod +w /var/www/html/data/
- sudo chmod 0744 index.php
- sudo service apache2 restart
# 2. Uitvoeren van postscriptMQTT.py
- python3 postscriptMQTT.py
Bij een result code 0 is de connectie met de ingestelde MQTT topic gelukt.