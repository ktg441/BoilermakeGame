import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;
import java.util.*;

public class ClientHandler extends Thread {
    final DataOutputStream outputStream;
    final DataInputStream inputStream;
    final Socket socket;
    String[] colors = {"0,255,255", "255,165,0", "0,255,0", "255,0,0"};
    String[] actions = {"action0", "action1", "action2", "action3"};
    static String currentAnswer;
    static boolean VRConnected = false;
    static boolean PCConnected = false;
    static boolean debug = false;

    public ClientHandler(DataOutputStream output, DataInputStream input, Socket skt) {
        outputStream = output;
        inputStream = input;
        socket = skt;
    }

    public void setNewAnswer() {
        Random r = new Random();
        currentAnswer = colors[r.nextInt(colors.length)];
    }

    class VRHandler extends ClientHandler {

        public VRHandler(DataOutputStream o, DataInputStream i, Socket s) {
            super(o, i, s);
        }
    }

    class PCHandler extends ClientHandler {
        public PCHandler(DataOutputStream o, DataInputStream i, Socket s) {
            super(o, i, s);
        }
    }

    @Override
    public void run() {
        try {
            writeToStream(outputStream, "Who are you");
            String identifier = readFromStream(inputStream);
            System.out.println("Received from client: " + identifier);
            System.out.printf("Length of message was %d", identifier.length());

            if (identifier.equals("PC")){
                PCConnected = true;
                if (!debug && PCConnected && VRConnected) runPC();
                else{
                    writeToStream(outputStream, "You are connected. Waiting for VR Player...");
                    while (!VRConnected){yield();}
                    runPC();
                }
            }
            else if (identifier.equals("VR")){
                VRConnected = true;
                if (!debug && PCConnected && VRConnected) runVR();
                else{
                    writeToStream(outputStream, "You are connected. Waiting for PC Player...");
                    while (!PCConnected){yield();}
                    runVR();
                }
            }
            //else System.exit(1);

        } catch (IOException e) {
            e.printStackTrace();
            System.exit(1);
        }
    }

    public void runPC() {
        try {
            System.out.println("into runpc");
            while (true) {
                String [] options = generatePCAnswers();
                writeToStream(outputStream, String.format("%s %s %s %s",
                        options[0], options[1], options[2], options[3]));
                String response = readFromStream(inputStream);
                if (response.equals(currentAnswer)) {
                    //VRAnswered = true;
                    writeToStream(outputStream, "correct");
                } else {
                    writeToStream(outputStream, "incorrect");
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
            System.exit(1);
        }
    }

    public void runVR() {
        try {
            System.out.println("into runvr");
            while (true) {
                //writeToStream(outputStream, currentAnswer);
                String response = readFromStream(inputStream);
                if (currentAnswer == null){
                    System.out.println("This is the case");
                    currentThread().yield();
                }
                if (response.equals(currentAnswer)) {
                    //VRAnswered = true;
                    writeToStream(outputStream, "correct");
                } else {
                    writeToStream(outputStream, "incorrect");
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
            System.exit(1);
        }
    }

    public void writeToStream(DataOutputStream d, String s) throws IOException {
        byte[] bytes = s.getBytes();
        for (int i = 0; i < bytes.length; i++) {
            if (i == 0) d.writeByte(' ');
            d.writeByte(bytes[i]);
        }
    }

    public String readFromStream(DataInputStream d) throws IOException {
        String s = "";
        int i = 0;
        while (true) {
            byte readByte = d.readByte();
            if (readByte == '~') break;
            s += (char) readByte;
            System.out.println((char) readByte);
        }
        System.out.println(s);
        return s;
    }

    public String[] generatePCAnswers() {
        Random r = new Random();
        int i = 0;
        String attemptColor;
        String attemptAction;
        Set<String> inAnswer = new HashSet<>();
        String[] returnArray = new String[4];
        //Get 4 Answers
        while (i < 4){
            //Get color
            while (inAnswer.contains(attemptColor = colors[r.nextInt(colors.length)])){}
            //Get action
            while (inAnswer.contains(attemptAction = actions[r.nextInt(actions.length)])){}
            returnArray[i++] = attemptColor + ":" + attemptAction;
            inAnswer.add(attemptColor);
            inAnswer.add(attemptAction);
        }
        int correctIndex = r.nextInt(returnArray.length);
        currentAnswer = returnArray[correctIndex];
        System.out.printf("Correct Answer is %s%n", currentAnswer);
        return returnArray;
    }
}