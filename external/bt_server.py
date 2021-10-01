import bluetooth

server_sock = bluetooth.BluetoothSocket(bluetooth.RFCOMM)
server_sock.bind(("", bluetooth.PORT_ANY))
server_sock.listen(1)

port = server_sock.getsockname()[1]
uuid = "00001801-0000-1000-8000-00805f9b34fb"
bluetooth.advertise_service(server_sock, "SampleServer", service_id=uuid,
                            service_classes=[uuid, bluetooth.SERIAL_PORT_CLASS],
                            profiles=[bluetooth.SERIAL_PORT_PROFILE],
                            )
while True:
    print("Waiting for connection on RFCOMM channel", port)
    client_sock, client_info = server_sock.accept()
    print("Accepted connection from", client_info)

    try:
        while True:
            data = client_sock.recv(1024).decode('utf-8')
            if not data:
                break
            elif "Hello" in data:
                client_sock.send("Ready\n".encode('utf-8'))
                client_sock.send("[Salida;-1.29;-10.03;0]\n".encode('utf-8'))
                # ....
                client_sock.send("[Baño 2;-0.34;-4.56;-90]\n".encode('utf-8'))
                client_sock.send("Done\n".encode('utf-8'))
            print("Received: ", data)

    except OSError:
        pass

    print("Disconnected.")
    client_sock.close()
    print("Trying to reconnect.")

server_sock.close()
