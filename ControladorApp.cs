using UnityEngine;
using System.IO.Ports;
using TMPro;
using UnityEngine.UI;

enum TaskState
{
    INIT,
    WAIT_COMMANDS
}

public class ControladorApp : MonoBehaviour
{
    private static TaskState taskState = TaskState.INIT;
    private SerialPort _serialPort;
    public TextMeshProUGUI estadoBtn1;
    public TextMeshProUGUI estadoBtn2;
    public TextMeshProUGUI estadoBtn3;
    public Button btnLed;
    public Button btnReadBtn;
    public TMP_Dropdown nLed;
    public TMP_Dropdown eLed;

    public void btnledcont()
    {
            // leer el estado del dropdown del selector de LED
                // Enviar mensaje con el numero de lED
        if (nLed.value==0)
        {
            _serialPort.Write("led1\n");
            Debug.Log("Send led1");
        }
        if (nLed.value==1)
        {
            _serialPort.Write("led2\n");
            Debug.Log("Send led2");
        }
        if (nLed.value==2)
        {
            _serialPort.Write("led3\n");
            Debug.Log("Send led3");
        }
        
            //Leer el estado del dropdown para enviar si es ON o es OFF
              // Envia el segundo mensaje
        if (eLed.value==0)
        {
            _serialPort.Write("ON\n");
            Debug.Log("Send ON");
        }
        if (eLed.value==1)
        {
            _serialPort.Write("OFF\n");
            Debug.Log("Send OFF");
        } 
        
    }

    public void btnBtRead()
    {
        _serialPort.Write("readBUTTONS\n");
        Debug.Log("Send readBUTTONS");
    }
    
    public void btnState(){
        string response = _serialPort.ReadLine();
        if (response == "000" )
        {
            estadoBtn1.text = "1: Pressed";
            estadoBtn2.text = "2: Pressed";
            estadoBtn3.text = "3: Pressed";
        }
        if (response == "001")
        {
            estadoBtn1.text = "1: Pressed";
            estadoBtn2.text = "2: Pressed";
            estadoBtn3.text = "3: Released";
        }
        if (response == "010")
        {
            estadoBtn1.text = "1: Pressed";
            estadoBtn2.text = "2: Released";
            estadoBtn3.text = "3: Pressed";
        }
        if (response == "100")
        {
            estadoBtn1.text = "1: Released";
            estadoBtn2.text = "2: Pressed";
            estadoBtn3.text = "3: Pressed";
        }
        if (response == "110")
        {
            estadoBtn1.text = "1: Released";
            estadoBtn2.text = "2: Released";
            estadoBtn3.text = "3: Pressed";
        }
        if (response == "011")
        {
            estadoBtn1.text = "1: Pressed";
            estadoBtn2.text = "2: Released";
            estadoBtn3.text = "3: Released";
        }
        if (response == "101")
        {
            estadoBtn1.text = "1: Released";
            estadoBtn2.text = "2: Pressed";
            estadoBtn3.text = "3: Released";
        }
        if (response == "111")
        {
            estadoBtn1.text = "1: Released";
            estadoBtn2.text = "2: Released";
            estadoBtn3.text = "3: Released";
        }
        Debug.Log(response);
    }
    
    void Start()
    {
        _serialPort = new SerialPort();
        _serialPort.PortName = "/dev/ttyUSB0";
        _serialPort.BaudRate = 115200;
        _serialPort.DtrEnable = true;
        _serialPort.NewLine = "\n";
        _serialPort.Open();
        Debug.Log("Open Serial Port");
    }

    void Update()
    {
        switch (taskState)
        {
            case TaskState.INIT:
                taskState = TaskState.WAIT_COMMANDS;
                Debug.Log("WAIT COMMANDS");
                break;
            case TaskState.WAIT_COMMANDS:
                if (_serialPort.BytesToRead > 0)
                {
                    btnState();
                }
                break;
        }
    }
}