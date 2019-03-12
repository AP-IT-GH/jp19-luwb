package davycools.ap.be.bluetoothconnectandroid;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.bluetooth.BluetoothAdapter;
import android.content.Intent;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.RippleDrawable;
import android.support.annotation.DrawableRes;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.MotionEvent;
import android.view.View;
import android.view.animation.AlphaAnimation;
import android.view.animation.ScaleAnimation;
import android.widget.Button;
import android.widget.Toast;

import java.util.Set;

import app.akexorcist.bluetotohspp.library.BluetoothSPP;
import app.akexorcist.bluetotohspp.library.BluetoothState;
import app.akexorcist.bluetotohspp.library.DeviceList;

public class MainActivity extends AppCompatActivity {

    final String ON = "1";
    final String OFF = "0";

    BluetoothSPP bluetooth;

    Button connect;
    Button on;
    Button off;

    @SuppressLint("ClickableViewAccessibility")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        bluetooth = new BluetoothSPP(this);

        connect = findViewById(R.id.connect);
        on = findViewById(R.id.on);
        off = findViewById(R.id.off);
        if (!bluetooth.isBluetoothAvailable()) {
            Toast.makeText(getApplicationContext(), "Bluetooth is not available", Toast.LENGTH_SHORT).show();
            finish();
        }

        //Animation for buttons
        final AlphaAnimation alphaDown,alphaUp,alphaDownOffButton,alphaUpOffButton;
        alphaDown = new AlphaAnimation(1.0f, 0.5f);
        alphaUp = new AlphaAnimation(0.5f, 1.0f);
        alphaDown.setDuration(100);
        alphaUp.setDuration(100);
        alphaDown.setFillAfter(true);
        alphaUp.setFillAfter(true);

        alphaDownOffButton = new AlphaAnimation(1.0f, 0.5f);
        alphaUpOffButton = new AlphaAnimation(0.5f, 1.0f);
        alphaDownOffButton.setDuration(100);
        alphaUpOffButton.setDuration(100);
        alphaDownOffButton.setFillAfter(true);
        alphaUpOffButton.setFillAfter(true);



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
        on.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                if (event.getAction() == MotionEvent.ACTION_DOWN) {
                    // Pressed
                    on.startAnimation(alphaDown);
                    bluetooth.send("f", true);
                } else if (event.getAction() == MotionEvent.ACTION_UP) {
                    //
                    on.startAnimation(alphaUp);
                    //bluetooth.send(OFF, true);
                    bluetooth.send("0", true);
                }
                return true;
            }
        });

        off.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                if (event.getAction() == MotionEvent.ACTION_DOWN) {
                    // Pressed
                    off.startAnimation(alphaDownOffButton);
                    bluetooth.send("t", true);

                } else if (event.getAction() == MotionEvent.ACTION_UP) {
                    //
                    off.startAnimation(alphaUpOffButton);
                    bluetooth.send("5", true);
                }
                return true;
            }
        });
        /*on.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                bluetooth.send(ON, true);
            }
        });

        off.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                bluetooth.send(OFF, true);
            }
        });*/

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

