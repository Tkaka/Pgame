
public    class BytesUtils
    {
        public static byte[] IntToBytes(byte[] bytes, int val)
        {
            bytes[3] = (byte)((val & 0xFF000000) >> 24);
            bytes[2] = (byte)((val & 0x00FF0000) >> 16);
            bytes[1] = (byte)((val & 0x0000FF00) >> 8);
            bytes[0] = (byte)((val & 0x000000FF));
            return bytes;
        }


        public static int BytesToInt(byte[] bytes, int offset)
        {
            int value;
            value = (int)((bytes[offset] & 0xFF)
                    | ((bytes[offset + 1] & 0xFF) << 8)
                    | ((bytes[offset + 2] & 0xFF) << 16)
                    | ((bytes[offset + 3] & 0xFF) << 24));
            return value;  
        }
    }

