#include <EtherCard.h>
#include <SoftwareSerial.h>
uint8_t macAnchor = 1; 
uint8_t macTag = 0;


static byte mymac[] = { 0x74, 0x69, 0x69, 0x2D, 0x30, 0x10 + macAnchor };
static byte myip[] = { 192, 168, 3, 10 + macAnchor }; //IP
static byte gwip[] = { 192, 168, 3, 3 }; //Gateway
static byte hisip[] = { 192, 168, 3, 3 }; //Destination
int hisport = 2000;
const char website[] PROGMEM = "192.168.3.3"; //Destination
byte Ethernet::buffer[500];
Stash stash;
static long timer;
static byte session;

int distance = 0;
int ethernetInterval = 500;


static boolean receiving = false;
static byte ndx = 0;
char startCharacter = '<';
char endCharacter = '>';
char readChar;
const byte charCount = 32;
char receivedChars[charCount];
char tempChars[charCount];
boolean newData = false;

SoftwareSerial softSerial(A3, A2); // RX, TX

void checkSerial() {

  while (softSerial.available() > 0 && newData == false) {
    readChar = softSerial.read();
    

    if (receiving == true) {
      if (readChar != endCharacter) {
        receivedChars[ndx] = readChar;
        ndx++;
        if (ndx >= charCount) {
          ndx = charCount - 1;
        }
      }
      else {
        receivedChars[ndx] = '\0'; // terminate the string
        receiving = false;
        ndx = 0;
        newData = true;
      }
    }

    else if (readChar == startCharacter) {
      receiving = true;
    }
  }
  if (newData == true) {
    strcpy(tempChars, receivedChars);
    splitSerialData();
    newData = false;
  }

}

void splitSerialData() {
  char * strtokIndex;
  strtokIndex = strtok(tempChars, ",");
  macTag = atoi(strtokIndex);

  strtokIndex = strtok(NULL, ",");
  distance = atoi(strtokIndex);

  Serial.print(macTag);
  Serial.print(",");
  Serial.println(distance);
}

void sendPacket() {



  byte sd = stash.create();
  stash.print("{");

  stash.print("\"MAC_TAG\":");
  stash.print("\"");
  stash.print("TAG");
  stash.print(macTag);
  stash.print("\"");
  stash.print(",");
  stash.print("\"MAC_ANCHOR\":");
  stash.print("\"");
  stash.print("ANCHOR");
  stash.print(macAnchor);
  stash.print("\"");
  stash.print(",");
  stash.print("\"DISTANCE\":");
  stash.print(distance);

  stash.print("}");
  stash.save();
  Stash::prepare(PSTR("POST http://$F HTTP/1.1" "\r\n"
                      "Host: $F" "\r\n"
                      "Content-Length: $D" "\r\n"
                      "Content-Type: application/json" "\r\n"
                      "\r\n"
                      "$H"),
                 website, website, stash.size(), sd);
  ether.tcpSend();


  Serial.print("Packet sent, distance: ");
  Serial.println(distance);
}
void setup () {
  Serial.begin(9600);
  Serial.println("\n[getStaticIP]");
  if (ether.begin(sizeof Ethernet::buffer, mymac) == 0)
    Serial.println( "Failed to access Ethernet controller");
  ether.staticSetup(myip, gwip);
  ether.copyIp(ether.hisip, hisip);
  ether.hisport = hisport;
  ether.printIp("Server: ", ether.hisip);
  softSerial.begin(9600);
}

void loop () {

  checkSerial();
  ether.packetLoop(ether.packetReceive());

  if (millis() > timer + ethernetInterval) {
    timer = millis();
    if (distance != 0) {
      sendPacket();
    }
  }


}
