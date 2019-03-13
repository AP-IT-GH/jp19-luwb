package davycools.ap.be.bluetoothconnectandroid;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.MotionEvent;
import android.view.View;
import android.view.animation.AlphaAnimation;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.Toast;


import app.akexorcist.bluetotohspp.library.BluetoothSPP;
import app.akexorcist.bluetotohspp.library.BluetoothState;
import app.akexorcist.bluetotohspp.library.DeviceList;

public class MainActivity extends AppCompatActivity {

    BluetoothSPP bluetooth;

    Button connect;
    ImageButton forward,backwards,left,right;
    boolean goingForward,goingBackwards;

    @SuppressLint("ClickableViewAccessibility")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);


        //Animations
        final AlphaAnimation ButtonDown,ButtonUp;
        ButtonDown = new AlphaAnimation(1.0f, 0.5f);
        ButtonUp = new AlphaAnimation(0.5f, 1.0f);
        ButtonDown.setDuration(100);
        ButtonUp.setDuration(100);
        ButtonDown.setFillAfter(true);
        ButtonUp.setFillAfter(true);



        bluetooth = new BluetoothSPP(this);

        connect = findViewById(R.id.connect);
        forward = findViewById(R.id.forward);
        backwards = findViewById(R.id.backwards);
        left = findViewById(R.id.left);
        right = findViewById(R.id.right);

        if (!bluetooth.isBluetoothAvailable()) {
            Toast.makeText(getApplicationContext(), "Bluetooth is not available", Toast.LENGTH_SHORT).show();
            finish();
        }

        bluetooth.setBluetoothConnectionListener(new BluetoothSPP.BluetoothConnectionListener() {
            public void onDeviceConnected(String name, String address) {
                connect.setText("Connected to " + name);
            }

            public void onDeviceDisconnected() {
                connect.setText("Connection lost");
            }

            public void onDeviceConnectionFailed() {
                connect.setText("Unable to connect");
            }
        });

        connect.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (bluetooth.getServiceState() == BluetoothState.STATE_CONNECTED) {
                    bluetooth.disconnect();
                } else {
                    Intent intent = new Intent(getApplicationContext(), DeviceList.class);
                    startActivityForResult(intent, BluetoothState.REQUEST_CONNECT_DEVICE);
                }
            }
        });
        forward.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                if (event.getAction() == MotionEvent.ACTION_DOWN) {
                    // Pressed
                    forward.startAnimation(ButtonDown);
                    bluetooth.send("f", true);
                    goingForward = true;
                } else if (event.getAction() == MotionEvent.ACTION_UP) {
                    // Released
                    forward.startAnimation(ButtonUp);
                    bluetooth.send("s", true);
                    goingForward = false;
                }
                return true;
            }
        });

        backwards.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                if (event.getAction() == MotionEvent.ACTION_DOWN) {
                    // Pressed
                    backwards.startAnimation(ButtonDown);
                    bluetooth.send("b", true);
                    goingBackwards = true;
                } else if (event.getAction() == MotionEvent.ACTION_UP) {
                    // Released
                    backwards.startAnimation(ButtonUp);
                    bluetooth.send("s", true);
                    goingBackwards = false;
                }
                return true;
            }
        });

        left.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                if (event.getAction() == MotionEvent.ACTION_DOWN) {
                    // Pressed
                    left.startAnimation(ButtonDown);
                    bluetooth.send("l", true);

                } else if (event.getAction() == MotionEvent.ACTION_UP) {
                    // Released
                    left.startAnimation(ButtonUp);
                    CheckButtons();
                    //bluetooth.send("s", true);
                }
                return true;
            }
        });

        right.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                if (event.getAction() == MotionEvent.ACTION_DOWN) {
                    // Pressed
                    right.startAnimation(ButtonDown);
                    bluetooth.send("r", true);

                } else if (event.getAction() == MotionEvent.ACTION_UP) {
                    // Released
                    right.startAnimation(ButtonUp);
                    CheckButtons();
                    //bluetooth.send("s", true);
                }
                return true;
            }
        });
    }

    public void CheckButtons(){
        if(goingForward){
            bluetooth.send("f", true);
        }
        else if (goingBackwards){
            bluetooth.send("b", true);
        }
        else{
            bluetooth.send("s", true);
        }
    }
    public void onStart() {
        super.onStart();
        if (!bluetooth.isBluetoothEnabled()) {
            bluetooth.enable();
        } else {
            if (!bluetooth.isServiceAvailable()) {
                bluetooth.setupService();
                bluetooth.startService(BluetoothState.DEVICE_OTHER);
            }
        }
    }

    public void onDestroy() {
        super.onDestroy();
        bluetooth.stopService();
    }

    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (requestCode == BluetoothState.REQUEST_CONNECT_DEVICE) {
            if (resultCode == Activity.RESULT_OK)
                bluetooth.connect(data);
        } else if (requestCode == BluetoothState.REQUEST_ENABLE_BT) {
            if (resultCode == Activity.RESULT_OK) {
                bluetooth.setupService();
            } else {
                Toast.makeText(getApplicationContext()
                        , "Bluetooth was not enabled."
                        , Toast.LENGTH_SHORT).show();
                finish();
            }
        }
    }

}

