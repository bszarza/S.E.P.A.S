Imports System.IO
Imports System.Security.Cryptography

Namespace EncryptQueryString
    Public NotInheritable Class help

        'Public Shared Function EncryptString(Message As String, Passphrase As String) As String
        '    Dim Results As Byte()
        '    Dim UTF8 As New System.Text.UTF8Encoding()

        '    ' Step 1. We hash the passphrase using MD5
        '    ' We use the MD5 hash generator as the result is a 128 bit byte array
        '    ' which is a valid length for the TripleDES encoder we use below

        '    Dim HashProvider As New MD5CryptoServiceProvider()
        '    Dim TDESKey As Byte() = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase))

        '    ' Step 2. Create a new TripleDESCryptoServiceProvider object
        '    Dim TDESAlgorithm As New TripleDESCryptoServiceProvider()

        '    ' Step 3. Setup the encoder
        '    TDESAlgorithm.Key = TDESKey
        '    TDESAlgorithm.Mode = CipherMode.ECB
        '    TDESAlgorithm.Padding = PaddingMode.PKCS7

        '    ' Step 4. Convert the input string to a byte[]
        '    Dim DataToEncrypt As Byte() = UTF8.GetBytes(Message)

        '    ' Step 5. Attempt to encrypt the string
        '    Try
        '        Dim Encryptor As ICryptoTransform = TDESAlgorithm.CreateEncryptor()
        '        Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length)
        '    Finally
        '        ' Clear the TripleDes and Hashprovider services of any sensitive information
        '        TDESAlgorithm.Clear()
        '        HashProvider.Clear()
        '    End Try

        '    ' Step 6. Return the encrypted string as a base64 encoded string
        '    Return Convert.ToBase64String(Results)
        'End Function

        'Public Shared Function DecryptString(Message As String, Passphrase As String) As String
        '    Dim Results As Byte()
        '    Dim UTF8 As New System.Text.UTF8Encoding()

        '    ' Step 1. We hash the passphrase using MD5
        '    ' We use the MD5 hash generator as the result is a 128 bit byte array
        '    ' which is a valid length for the TripleDES encoder we use below

        '    Dim HashProvider As New MD5CryptoServiceProvider()
        '    Dim TDESKey As Byte() = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase))

        '    ' Step 2. Create a new TripleDESCryptoServiceProvider object
        '    Dim TDESAlgorithm As New TripleDESCryptoServiceProvider()

        '    ' Step 3. Setup the decoder
        '    TDESAlgorithm.Key = TDESKey
        '    TDESAlgorithm.Mode = CipherMode.ECB
        '    TDESAlgorithm.Padding = PaddingMode.PKCS7

        '    ' Step 4. Convert the input string to a byte[]
        '    Dim DataToDecrypt As Byte() = Convert.FromBase64String(Message)

        '    ' Step 5. Attempt to decrypt the string
        '    Try
        '        Dim Decryptor As ICryptoTransform = TDESAlgorithm.CreateDecryptor()
        '        Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length)
        '    Finally
        '        ' Clear the TripleDes and Hashprovider services of any sensitive information
        '        TDESAlgorithm.Clear()
        '        HashProvider.Clear()
        '    End Try

        '    ' Step 6. Return the decrypted string in UTF8 format
        '    Return UTF8.GetString(Results)
        'End Function
        Public Shared Function Encode(ByVal value As String,
                       ByVal key As String) As String
            Dim mac3des As New System.Security.Cryptography.MACTripleDES()
            Dim md5 As New System.Security.Cryptography.MD5CryptoServiceProvider()
            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key))
            Return Convert.ToBase64String(
              System.Text.Encoding.UTF8.GetBytes(value)) & "-"c &
              Convert.ToBase64String(mac3des.ComputeHash(
              System.Text.Encoding.UTF8.GetBytes(value)))
        End Function

        'Function to decode the string
        'Throws an exception if the data is corrupt
        Public Shared Function Decode(ByVal value As String,
                  ByVal key As String) As String
            Dim dataValue As String = ""
            Dim calcHash As String = ""
            Dim storedHash As String = ""

            Dim mac3des As New System.Security.Cryptography.MACTripleDES()
            Dim md5 As New System.Security.Cryptography.MD5CryptoServiceProvider()
            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key))

            Try
                dataValue = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(value.Split("-"c)(0)))
                storedHash = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(value.Split("-"c)(1)))
                calcHash = System.Text.Encoding.UTF8.GetString(
              mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dataValue)))

                If storedHash <> calcHash Then
                    'Data was corrupted

                    Throw New ArgumentException("Hash value does not match")
                    'This error is immediately caught below
                End If
            Catch ex As Exception
                Throw New ArgumentException("parametro entre formularios incorrecto")
            End Try

            Return dataValue

        End Function
    End Class
End Namespace