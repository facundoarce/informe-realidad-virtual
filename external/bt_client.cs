/// Send a message to bluetooth device
void send( string msg ) {
  if ( device != null && !string.IsNullOrEmpty( msg ) ) {
    device.send( System.Text.Encoding.ASCII.GetBytes( msg ) );
  }
}

// Read message from bluetooth device
IEnumerator ManageConnection( BluetoothDevice device ) {
  while ( device.IsReading ) {
    byte[] msg = device.read();
    if ( msg != null ) {
      // Convert byte array to string.
      string content = System.Text.Encoding.UTF8.GetString( msg );
      // Split the string into lines. '\n','\r' are charachter used to represent new line.
      string[] lines = content.Split( new char[] { '\n', '\r' } );
      // Add those lines to the processText
      _processText.text += content;
    }
    yield return null;
  }
}
