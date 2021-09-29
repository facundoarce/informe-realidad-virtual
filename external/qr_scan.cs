/// Capture and scan the current frame
void Scan() {
  System.Action<byte[], int, int> callback = (bytes, width, height) => {
    if (bytes == null) {
      // No image is available.
      return;
    }
    // Decode the image using ZXing parser
    try {
      IBarcodeReader barcodeReader = new BarcodeReader();
      var result = barcodeReader.Decode(bytes, width, height, RGBLuminanceSource.BitmapFormat.Gray8);
      string resultText = result.Text;
      // Relocate player
      if (first) {
        Relocate(resultText);
        first = false;  // avoids relocating on every Update() to the same calibration point
      }
    }
    catch (NullReferenceException) {  //throws NullReferenceException if no QR was found
      first = true;                   // allows relocating next time it finds a qr code
    }
  };
  CaptureScreenAsync(callback);
}
