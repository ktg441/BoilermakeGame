import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Enumeration;

public class Server{

    public static void main(String[] args) {
        ServerSocket mySocket;
        Socket clientSocket;
        try {
            mySocket = new ServerSocket(1157);
            for (int i = 0; i < 2; i++) {
                clientSocket = mySocket.accept();
                System.out.println("Established a new connection with: " + clientSocket);
                DataInputStream inputStream = new DataInputStream(clientSocket.getInputStream());
                DataOutputStream outputStream = new DataOutputStream(clientSocket.getOutputStream());
                Thread t = new ClientHandler(outputStream, inputStream, clientSocket);
                t.start();
            }
        } catch (IOException e) {
            System.err.println("Was unable to accept a connection");
            e.printStackTrace();
            System.exit(1);
        }
    }
}