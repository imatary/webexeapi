Imports System
Imports Microsoft.SmallBasic.Library
Imports Microsoft.SmallBasic
Imports Microsoft.SmallBasic.Library.Primitive
Imports System.Net.Mail
Imports System.Net
Imports System.Management
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Configuration
Imports System.Resources
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Drawing.Printing
Imports System.Net.Sockets
Imports System.Math
Imports System.Speech
Imports json.net
Imports json.net.generic
Imports json.net.minecraft
Imports json.net.player
Imports json.net.server
Imports MSWinsockLib
Imports Microsoft.VisualBasic.Devices.Keyboard


'''<summary>
''' Used to manipulate e-mails functions
''' </summary>

<SmallBasicType()> Public Module SPMail
    ''' <summary>
    ''' Used to send e-mails
    ''' </summary>
    ''' <param name="subject">
    ''' The subject of the e-mail, for example : "Hello !"
    ''' </param>
    ''' <param name="body">
    ''' The body of the e-mail, for example : "Hello, i had great holidays this year !"
    ''' </param>
    ''' <param name="username">
    ''' Your username, for example : "example.xxx@xxx.com"
    ''' </param>
    ''' <param name="password">
    ''' Your e-mail password, so it can connect to send the e-mail.
    ''' </param>
    ''' <param name="sendto">
    ''' The e-mail to send it to
    ''' </param>
    ''' <param name="SMTPCLIENT">
    ''' The Smtp Client of the e-mail, aka server to use...
    ''' 
    ''' Servers :
    ''' smtp.live.com
    ''' smtp.gmail.com
    ''' </param>
    ''' 
    '''<returns>
    ''' "SUCCESS" if the e-mail was sent...
    ''' "FAILED" if the e-mail failed to send...
    ''' "ERROR" if there is something wrong written in the Primitives...
    ''' </returns>
    Public Function SendMail(ByVal subject As Primitive, ByVal body As Primitive, ByVal sendto As Primitive, ByVal username As Primitive, ByVal password As Primitive, ByVal SMTPCLIENT As Primitive) As Primitive
        Try
            Dim Mail As New MailMessage
            Mail.Subject = subject
            Mail.To.Add(sendto)
            Mail.From = New MailAddress(username)
            Mail.Body = body

            Dim SMTP As New SmtpClient(SMTPCLIENT)
            SMTP.EnableSsl = True
            SMTP.Credentials = New System.Net.NetworkCredential(username, password)
            SMTP.Port = "587"
            Try
                SMTP.Send(Mail)
                Return "SUCCESS"
            Catch ex As Exception
                Return "FAILED"
                LastError = ex.ToString
            End Try
        Catch ex As Exception
            Return "ERROR"
            LastError = ex.ToString
        End Try
    End Function
End Module
'''<summary>
''' Used to manipulate sound.
''' </summary>

<SmallBasicType()> Public Module SPSound
    ''' <summary>
    ''' Used to play a sound
    ''' </summary>
    ''' <param name="soundtoplay">
    ''' The full path of the sound to play
    ''' For example : "C:\Folder1\folder2\sound.wav"
    ''' </param>
    ''' 
    '''<returns>
    ''' The path of the sound if neccesary...
    ''' </returns>
    Public Function PlaySound(ByVal soundtoplay As Primitive) As Primitive
        My.Computer.Audio.Play(soundtoplay, AudioPlayMode.Background)
        Return soundtoplay
    End Function
    ''' <summary>
    ''' Plays a looped sound until it is stopped.
    ''' </summary>
    ''' <param name="soundtoplay">
    ''' The full path of the sound to play
    ''' For example : "C:\Folder1\folder2\sound.wav"
    ''' </param>
    ''' <returns>The path of the sound if neccesary...</returns>
    Public Function PlayLoopedSound(ByVal soundtoplay As Primitive) As Primitive
        My.Computer.Audio.Play(soundtoplay, AudioPlayMode.BackgroundLoop)
        Return soundtoplay
    End Function
    ''' <summary>
    ''' Used to stop a sound
    ''' </summary>
    Public Function StopSound() As Primitive
        My.Computer.Audio.Stop()

    End Function
End Module
''' <summary>
''' Used to manipulate sound a lot more.
''' </summary>
<SmallBasicType()> Public Module SPAdvancedSound

    Private Declare Function mciSendString Lib "Winmm.dll" Alias "mciSendStringA" (ByVal lpStrCommand As String, ByVal LpstrReturnString As String, ByVal uReturnLength As Integer, ByVal hwndCall As Integer) As Integer
    ''' <summary>
    ''' Start recording from microsoft's default device.
    ''' </summary>
    ''' <param name="name">
    ''' The name of the record.
    ''' </param>
    ''' <returns>
    ''' The name of the record if neccesary...
    ''' </returns>
    Public Function StartRecording(ByVal name As Primitive) As Primitive
        mciSendString("Open New Type Waveaudio Alias " + name, "", 0, 0)
        mciSendString("Record " + name, "", 0, 0)
        Return name
    End Function
    ''' <summary>
    ''' Stop recording from microsoft's default device.
    ''' </summary>
    ''' <param name="name">
    ''' The name of the record.
    ''' </param>
    ''' <returns>
    ''' The name of the record if neccesary...
    ''' </returns>
    Public Function StopRecording(ByVal name As Primitive) As Primitive
        mciSendString("Stop " + name, "", 0, 0)
        Return name
    End Function
    ''' <summary>
    ''' Pause recording from microsoft's default device.
    ''' </summary>
    ''' <param name="name">
    ''' The name of the record.
    ''' </param>
    ''' <returns>
    ''' The name of the record if neccesary...
    ''' </returns>
    Public Function PauseRecording(ByVal name As Primitive) As Primitive
        mciSendString("Pause " + name, "", 0, 0)
        Return name
    End Function
    ''' <summary>
    ''' Resume recording from microsoft's default device.
    ''' </summary>
    ''' <param name="name">
    ''' The name of the record.
    ''' </param>
    ''' <returns>
    ''' The name of the record if neccesary...
    ''' </returns>
    Public Function ResumeRecording(ByVal name As Primitive) As Primitive
        mciSendString("Resume " + name, "", 0, 0)
        Return name
    End Function
    ''' <summary>
    ''' Save the record.
    ''' </summary>
    ''' <param name="name">
    ''' The name of the record.
    ''' </param>
    ''' <param name="filepath">
    ''' The full path to save this at.
    ''' Note : It will save as a .wav file.
    ''' </param>
    ''' <returns>
    ''' The file path...
    ''' </returns>
    Public Function SaveRecording(ByVal name As Primitive, ByVal filepath As Primitive) As Primitive
        mciSendString("Save " + name + " " + filepath, "", 0, 0)
        Return filepath
    End Function
    ''' <summary>
    ''' Remove recording declaration.
    ''' </summary>
    ''' <param name="name">
    ''' The name of the record.
    ''' </param>
    ''' <returns>
    ''' The name of the record if neccesary...
    ''' </returns>
    Public Function RemoveRecording(ByVal name As Primitive) As Primitive
        mciSendString("Close " + name, "", 0, 0)
        Return name
    End Function
End Module
''' <summary>
''' General functions for SayPlus !
''' </summary>
<SmallBasicType()> Public Module SPGeneral
    Dim version As String = "1.3.2"
    Private Declare Function mciSendString Lib "Winmm.dll" Alias "mciSendStringA" (ByVal lpStrCommand As String, ByVal LpstrReturnString As String, ByVal uReturnLength As Integer, ByVal hwndCall As Integer) As Integer
    ''' <summary>
    ''' RESERVED FOR ADVANCED USERS
    ''' 
    ''' Send a string that will be reconized by Windows
    ''' Mostly used for file manipulating and medias.
    ''' </summary>
    ''' <param name="stringtosend">
    ''' The string to send...
    ''' </param>
    ''' <returns>
    ''' The sended string, if neccesary...
    ''' </returns>
    Public Function SimpleSendString(ByVal stringtosend As Primitive) As Primitive
        mciSendString(stringtosend, "", 0, 0)
        Return stringtosend
    End Function
    ''' <summary>
    ''' RESERVED FOR ADVANCED USERS
    ''' 
    ''' Send a string that will be reconized by Windows
    ''' Mostly used for file manipulating and medias
    ''' </summary>
    ''' <param name="stringtosend">The string to send...</param>
    ''' <param name="returnstring">The returning string...</param>
    ''' <param name="returnlength">The returning length of the string...</param>
    ''' <param name="hwndCall">Windows call...</param>
    ''' <returns>The sended string, if neccesary...</returns>
    Public Function SendString(ByVal stringtosend As Primitive, ByVal returnstring As Primitive, ByVal returnlength As Primitive, ByVal hwndCall As Primitive) As Primitive
        mciSendString(stringtosend, returnstring, returnlength, hwndCall)
        Return stringtosend
    End Function
    ''' <summary>
    ''' Will show the about box of this extension
    ''' </summary>
    Public Function About() As Primitive
        MsgBox("Made by Ashkore Dracson, current version is : " & version)
    End Function
    ''' <summary>
    ''' Will shutdown the computer.
    ''' </summary>
    Public Function Shutdown() As Primitive
        Shell("shutdown -s")
    End Function
    ''' <summary>
    ''' Logouts from the current session.
    ''' </summary>
    Public Function LogOut() As Primitive
        Shell("shutdown -l")
    End Function
    ''' <summary>
    ''' Puts the computer in real deep sleep mode.
    ''' </summary>
    ''' <remarks>
    ''' Takes a little while for the computer to enter sleep mode, you will notice that the screen shutdowns, but after like 30 seconds, it is in deep sleep mode...
    ''' You need to press the power button to activate your computer again.
    ''' </remarks>
    Public Function DeepSleep() As Primitive
        Shell("shutdown -h")
    End Function
    ''' <summary>
    ''' Aborts the shutdown proccess...
    ''' </summary>
    Public Function AbortShutdown() As Primitive
        Shell("shutdown -a")
    End Function
    ''' <summary>
    ''' Shows the shutdown dialog, mostly used to shutdown other people's computer when you are the administrator of a network.
    ''' </summary>
    Public Function ShowShutdownDialog() As Primitive
        Shell("shutdown -i")
    End Function
    ''' <summary>
    ''' Shutdowns the computer with a custom message, and a timeout before shutdown proccess.
    ''' </summary>
    ''' <param name="timeout">The timeout in millseconds</param>
    ''' <param name="message">The message you want to display.</param>
    Public Function AdvancedShutdown(ByVal timeout As Primitive, ByVal message As Primitive) As Primitive
        Shell("shutdown -s -t " + timeout + " -c " + message)
    End Function
    ''' <summary>
    ''' If the mouse wheel exists or not.
    ''' </summary>
    ''' <returns>
    ''' TRUE if it exists.
    ''' FALSE if it dosen't exists.
    ''' </returns>
    Public Function MouseWheelExists() As Primitive
        If My.Computer.Mouse.WheelExists = True Then
            Return "TRUE"
        Else
            Return "FALSE"
        End If
    End Function
    ''' <summary>
    ''' The scroll lines of the wheel
    ''' </summary>
    ''' <returns>The number of scroll lines.</returns>
    Public Function GetMouseScrollLines() As Primitive
        Return My.Computer.Mouse.WheelScrollLines
    End Function
    ''' <summary>
    ''' Gets the color of a pixel in the whole screen, not only to the graphics window...
    ''' </summary>
    ''' <param name="x">The X Value</param>
    ''' <param name="y">The Y Value</param>
    ''' <returns>The hex color or FAILED if fails.</returns>
    Public Function GetPixel(ByVal x As Primitive, ByVal y As Primitive) As Primitive
        Try
            Dim BMP As New Drawing.Bitmap(1, 1)
            Dim GFX As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(BMP)
            GFX.CopyFromScreen(New Drawing.Point(x, y), New Drawing.Point(0, 0), BMP.Size)
            Dim Pixel As Drawing.Color = BMP.GetPixel(0, 0)
            Return "#" + Microsoft.SmallBasic.Library.Text.GetSubTextToEnd(Microsoft.VisualBasic.Conversion.Hex(Pixel.ToArgb()), 3)
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Gets the color of a pixel in the image.
    ''' </summary>
    ''' <param name="x">The X Value</param>
    ''' <param name="y">The Y Value</param>
    ''' <param name="filepath">The filepath of the image.</param>
    ''' <returns>The hex color or FAILED if fails.</returns>
    Public Function GetPixelFromImage(ByVal x As Primitive, ByVal y As Primitive, ByVal filepath As Primitive) As Primitive
        Try
            Dim image As Image
            image = image.FromFile(filepath)
            Dim BMP As New Drawing.Bitmap(image.Width, image.Height)
            Dim GFX As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(BMP)
            GFX.DrawImage(image, New Point(0, 0))
            Dim Pixel As Drawing.Color = BMP.GetPixel(x, y)
            Return "#" + Microsoft.SmallBasic.Library.Text.GetSubTextToEnd(Microsoft.VisualBasic.Conversion.Hex(Pixel.ToArgb()), 3)
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
End Module
''' <summary>
''' Extra functions of this extension.
''' </summary>
<SmallBasicType()> Public Module SPExtra
    ''' <summary>
    ''' Will compile any Small Basic based text
    ''' </summary>
    ''' <param name="data">
    ''' The small basic text to compile.
    ''' For example : TextWindow.WriteLine("stuff")
    ''' For i = 0 To 10
    ''' 'Do this
    ''' EndFor
    ''' </param>
    ''' <returns>
    ''' SUCCESS if compiling worked, it should open the program.
    ''' FAILED if compiling didn't worked.
    ''' </returns>
    Public Function Compile(ByVal data As Primitive) As Primitive
        Return "Sorry, this function is currently in HUGE FIX, so we suspended it for a moment, will be most likely fixed in the next version."
    End Function
    ''' <summary>
    ''' Sends the specified word, phrase, number or character to the keyboard, to simulate a keyboard touch press.
    ''' </summary>
    ''' <param name="keytosend"></param>
    ''' <returns>The sended keys, if neccesary...or FAILED</returns>
    Public Function SendKeys(ByVal keytosend As Primitive) As Primitive
        Try
            My.Computer.Keyboard.SendKeys(keytosend, True)
            Return keytosend
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Converts a text into binary code.
    ''' </summary>
    ''' <param name="text">The text to convert into binary code.</param>
    ''' <returns>The binary code of the given text or FAILED if fails.</returns>
    Public Function ConvertTextToBinary(ByVal text As Primitive) As Primitive
        Try
            Dim Val As String = Nothing
            Dim Result As New System.Text.StringBuilder
            For Each Character As Byte In System.Text.ASCIIEncoding.ASCII.GetBytes(text)
                Result.Append(Convert.ToString(Character, 2).PadLeft(8, "0"))
                Result.Append(" ")
            Next
            Val = Result.ToString.Substring(0, Result.ToString.Length - 1)
            Return Val
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Converts a binary code into a text string.
    ''' </summary>
    ''' <param name="binary">The binary code to convert into text.</param>
    ''' <returns>The text of the given binary code or FAILED if fails.</returns>
    Public Function ConvertBinaryToText(ByVal binary As Primitive) As Primitive
        Try
            Dim Val As String = Nothing
            Dim Characters As String = System.Text.RegularExpressions.Regex.Replace(binary, "[^01]", "")
            Dim ByteArray((Characters.Length / 8) - 1) As Byte
            For Index As Integer = 0 To ByteArray.Length - 1
                ByteArray(Index) = Convert.ToByte(Characters.Substring(Index * 8, 8), 2)
            Next
            Val = System.Text.ASCIIEncoding.ASCII.GetString(ByteArray)
            Return Val
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
End Module
''' <summary>
''' Some math functions.
''' </summary>
<SmallBasicType()> Public Module SPMath
    ''' <summary>
    ''' Calculates the opposite of the number, for example 2 will return 0.5, or 0.25 will return 4.
    ''' </summary>
    ''' <param name="number">The number to find the opposite to.</param>
    ''' <returns>The opposite number of the number.</returns>
    Public Function Opposite(ByVal number As Primitive) As Primitive
        Return 1 / number
    End Function
    ''' <summary>
    ''' Calculates the square root of the given number
    ''' </summary>
    ''' <param name="number">The number to calculate.</param>
    ''' <returns>The square root of the given number.</returns>
    ''' <remarks></remarks>
    Public Function SquareRoot(ByVal number As Primitive) As Primitive
        Return number * number
    End Function
    ''' <summary>
    ''' Gives a random number between 2 given numbers.
    ''' </summary>
    ''' <param name="min">The minimum value of the random number.</param>
    ''' <param name="max">The maximum value of the random number.</param>
    ''' <returns>A random number between the min and max values or FAILED if fails.</returns>
    Public Function RandomNumber(ByVal min As Primitive, ByVal max As Primitive) As Primitive
        Try
            Dim numbertoreturn As Integer
            Dim random As New Random
            numbertoreturn = random.Next(min, max)
            Return numbertoreturn
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Add two numbers together.
    ''' </summary>
    ''' <param name="number1">The first number</param>
    ''' <param name="number2">The second number</param>
    ''' <returns>The result or FAILED if fails.</returns>
    Public Function Addition(ByVal number1 As Primitive, ByVal number2 As Primitive) As Primitive
        Try
            Return number1 + number2
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Substract two numbers together.
    ''' </summary>
    ''' <param name="number1">The first number</param>
    ''' <param name="number2">The second number</param>
    ''' <returns>The result or FAILED if fails.</returns>
    Public Function Substract(ByVal number1 As Primitive, ByVal number2 As Primitive) As Primitive
        Try
            Return number1 - number2
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Divides two numbers together.
    ''' </summary>
    ''' <param name="number1">The first number</param>
    ''' <param name="number2">The second number</param>
    ''' <returns>The result or FAILED if fails.</returns>
    Public Function Divide(ByVal number1 As Primitive, ByVal number2 As Primitive) As Primitive
        Try
            Return number1 / number2
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Multiplies two numbers together.
    ''' </summary>
    ''' <param name="number1">The first number</param>
    ''' <param name="number2">The second number</param>
    ''' <returns>The result or FAILED if fails.</returns>
    Public Function Multiply(ByVal number1 As Primitive, ByVal number2 As Primitive) As Primitive
        Try
            Return number1 * number2
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Checks if the pythagor method is true or not with the given numbers
    ''' </summary>
    ''' <param name="longestline">The longest line of the triangle.</param>
    ''' <param name="line1">The other line length</param>
    ''' <param name="line2">And again, the other line length.</param>
    ''' <returns>
    ''' TRUE if it is equal.
    ''' FALSE if it is not equal.
    ''' </returns>
    ''' <remarks></remarks>
    Public Function CheckPythagor(ByVal longestline As Primitive, ByVal line1 As Primitive, ByVal line2 As Primitive) As Primitive
        If longestline * longestline = line1 * line1 + line2 * line2 Then
            Return "TRUE"
        Else
            Return "FALSE"
        End If
    End Function
    ''' <summary>
    ''' Checks if the given numbers are opposite or not.
    ''' </summary>
    ''' <param name="num1">The first number.</param>
    ''' <param name="num2">The second number.</param>
    ''' <returns>
    ''' TRUE if the given numbers are opposite.
    ''' FALSE if the given numbers aren't opposite.
    ''' </returns>
    ''' <remarks>
    ''' The actual operation is simple
    ''' If x * y = 1 then they are opposites.
    ''' </remarks>
    Public Function CheckOpposite(ByVal num1 As Primitive, ByVal num2 As Primitive) As Primitive
        If num1 * num2 = 1 Then
            Return "TRUE"
        Else
            Return "FALSE"
        End If
    End Function
    ''' <summary>
    ''' Gets the average number with the two given numbers.
    ''' </summary>
    ''' <param name="num1">The first number</param>
    ''' <param name="num2">The second number</param>
    ''' <returns>
    ''' The average of the two numbers or FAILED.
    ''' </returns>
    Public Function Average(ByVal num1 As Primitive, ByVal num2 As Primitive) As Primitive
        Try
            Return (num1 + num2) / 2
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Gets the inverse of the square root.
    ''' Ex : 4 would return 2 instead of 2 returns 4 for the square root.
    ''' </summary>
    ''' <param name="num">The number.</param>
    ''' <returns>The Inverse Square Root of the given number.</returns>
    ''' <remarks></remarks>
    Public Function InvSquareRoot(ByVal num As Primitive) As Primitive
        Return Sqrt(num)
    End Function
    ''' <summary>
    ''' Returns the excact value of PI by calculating 4*Atan(1)
    ''' </summary>
    Public Property Pi As Primitive = 4 * Atan(1)
    ''' <summary>
    ''' Not the exact value, but very precise value...
    ''' </summary>
    Public Property ExactPi As Primitive = "3.1415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170679821480865132823066470938446095505822317253594081284811174502841027019385211055596446229489549303819644288109756659334461284756482337867831652712019091456485669234603486104543266482133936072602491412737245870066063155881748815209209628292540917153643678925903600113305305488204665213841469519415116094330572703657595919530921861173819326117931051185480744623799627495673518857527248912279381830119491298336733624406566430860213949463952247371907021798609437027705392171762931767523846748184676694051320005681271452635608277857713427577896091736371787214684409012249534301465495853710507922796892589235420199561121290219608640344181598136297747713099605187072113499999983729780499510597317328160963185950244594553469083026425223082533446850352619311881710100031378387528865875332083814206171776691473035982534904287554687311595628638823537875937519577818577805321712268066130019278766111959092164201989"
    ''' <summary>
    ''' Calculates the power of a number.
    ''' </summary>
    ''' <param name="number">The number</param>
    ''' <param name="pwr">
    ''' The power of the number, pretty much like the squareroot 5² = 25, 5
    ''' But this time it's for you to choose, for example : 5* which * is the power.
    ''' </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Power(ByVal number As Primitive, ByVal pwr As Primitive) As Primitive
        Return Microsoft.SmallBasic.Library.Math.Power(number, pwr)
    End Function
    ''' <summary>
    ''' Calculates the remainder of two numbers.
    ''' </summary>
    ''' <param name="num1">The first number.</param>
    ''' <param name="num2">The second number.</param>
    ''' <returns>The remainder after dividing both numbers.</returns>
    Public Function Remainder(ByVal num1 As Primitive, ByVal num2 As Primitive) As Primitive
        If num1 <= num2 Then
            Return 0
        Else
            Dim c As Integer = num1 / num2
            Dim result As Integer = Nothing
            c = c - Floor(c)
            result = c * num2
            Return result
        End If
    End Function
    ''' <summary>
    ''' Calculates the Greatest Common Divisor of two numbers.
    ''' </summary>
    ''' <param name="num1">The first number.</param>
    ''' <param name="num2">The second number.</param>
    ''' <returns>The greatest common divisor of the two given numbers.</returns>
    Public Function GCD(ByVal num1 As Primitive, ByVal num2 As Primitive) As Primitive
        Dim a, b As Integer
        a = num1
        b = num2
        Do
            If a > b Then
                a = a - b
            Else
                b = b - a
            End If
        Loop Until a = b
        Return a
    End Function
End Module
''' <summary>
''' A simple clipboard.
''' </summary>
<SmallBasicType()> Public Module SPClipboard
    ''' <summary>
    ''' Gets the text of the clipboard.
    ''' </summary>
    ''' <returns>The text in the clip board or "FAILED" if fails.</returns>
    ''' <remarks></remarks>
    Public Function GetText() As Primitive
        Try
            Return Clipboard.GetText.ToString
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Sets a text into the clipboard.
    ''' </summary>
    ''' <param name="text">The text to set into the clipboard.</param>
    ''' <returns>"FAILED" if fails.</returns>
    Public Function SetText(ByVal text As Primitive) As Primitive
        Try
            My.Computer.Clipboard.SetText(text)
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Clears the clipboard.
    ''' </summary>
    ''' <returns>"FAILED" if fails.</returns>
    Public Function Clear() As Primitive
        Try
            My.Computer.Clipboard.Clear()
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Gets the audio in the clipboard, and plays it.
    ''' </summary>
    ''' <returns>"FAILED" if fails.</returns>
    ''' <remarks>This can be quite buggy, especially if you don't have a good PC, because it is still an unoptimized code.</remarks>
    Public Function GetAudioAndPlay() As Primitive
        Try
            Dim audio = My.Computer.Clipboard.GetAudioStream()
            My.Computer.Audio.Play(audio.ReadByte(), AudioPlayMode.Background)
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
End Module
''' <summary>
''' Object to manipulate registry values and keys.
''' </summary>
''' <remarks></remarks>
<SmallBasicType()> Public Module SPRegistry
    ''' <summary>
    ''' Get the value of the key and value name
    ''' </summary>
    ''' <param name="keyname">The key name of the value to get.</param>
    ''' <param name="valuename">The value name of the value to get.</param>
    ''' <param name="defaultvalue">The default value fo the value to get.</param>
    ''' <returns>The value of the given key name, value name, and default value.</returns>
    Public Function GetValue(ByVal keyname As Primitive, ByVal valuename As Primitive, ByVal defaultvalue As Primitive) As Primitive
        Try
            Dim valuetoget = My.Computer.Registry.GetValue(keyname, valuename, defaultvalue)
            Return valuetoget
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Sets a value in the registry.
    ''' ONLY FOR ADVANCED USERS, MODIFYING THE RESGISTRY INCORRECTLY CAN HARM YOUR COMPUTER.
    ''' </summary>
    ''' <param name="keyname">The key name where you want to set the value at.</param>
    ''' <param name="valuename">The value name where you want to set the value at.</param>
    ''' <param name="value">The new value.</param>
    Public Function SetValue(ByVal keyname As Primitive, ByVal valuename As Primitive, ByVal value As Primitive) As Primitive
        Try
            My.Computer.Registry.SetValue(keyname, valuename, value)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
End Module
''' <summary>
''' Object used to manipulate network functions.
''' </summary>
<SmallBasicType()> Public Module SPNetwork
    ''' <summary>
    ''' Determines if a network is avaliable or not.
    ''' </summary>
    ''' <returns>TRUE or FALSE or FAILED if fails.</returns>
    Public Function IsAvaliable() As Primitive
        Try
            Dim objUrl As New System.Uri("http://www.google.com")
            Dim objWebReq As System.Net.WebRequest
            objWebReq = System.Net.WebRequest.Create(objUrl)
            Dim objresp As System.Net.WebResponse
            Try
                objresp = objWebReq.GetResponse
                objresp.Close()
                objresp = Nothing
                Return True

            Catch ex As Exception
                objresp = Nothing
                objWebReq = Nothing
                Return False
            End Try
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Pings a target with his IP to see if there's any connection with this IP.
    ''' </summary>
    ''' <param name="IP">The IP of the target.</param>
    ''' <param name="timeout">The timeout, in milliseconds or -1 for infinite.</param>
    ''' <returns>True or False, or FAILED if fails.</returns>
    Public Function Request(ByVal IP As Primitive, ByVal timeout As Primitive) As Primitive
        Try
            If timeout = -1 Then
                Dim curping = My.Computer.Network.Ping(IP, 999999)
                Return curping
            Else
                Dim curping = My.Computer.Network.Ping(IP, timeout)
                Return curping
            End If
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Pings a server to see the response time.
    ''' </summary>
    ''' <param name="IP">The IP of the target.</param>
    ''' <param name="timeout">The timeout, in milliseconds or -1 for infinite.</param>
    ''' <returns>The ping in milliseconds or FAILED.</returns>
    Public Function Ping(ByVal IP As Primitive, ByVal timeout As Primitive) As Primitive
        Try
            If timeout = -1 Then
                Dim s As New Stopwatch
                s.Start()
                My.Computer.Network.Ping(IP, 999999)
                s.Stop()
                Return s.ElapsedMilliseconds
            Else
                Dim s As New Stopwatch
                s.Start()
                My.Computer.Network.Ping(IP, timeout)
                s.Stop()
                Return s.ElapsedMilliseconds
            End If
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Downloads a file async from the internet.
    ''' </summary>
    ''' <param name="filetodownload">The URL to download.</param>
    ''' <param name="targetpath">The target file path to save this at.</param>
    ''' <returns>SUCCESS or FAILED if fails.</returns>
    Public Function DownloadFile(ByVal filetodownload As Primitive, ByVal targetpath As Primitive) As Primitive
        Try
            My.Computer.Network.DownloadFile(filetodownload, targetpath)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Uploads a file async into the internet.
    ''' </summary>
    ''' <param name="filetoupload">The file path for the upload.</param>
    ''' <param name="targetURL">The target URL in the internet.</param>
    ''' <returns>SUCCESS or FAILED if fails.</returns>
    ''' <remarks>Only works if your IP is allowed to upload files on that site, or computer, ect...
    ''' Which in major cases, won't be true, works great for FTP.
    ''' </remarks>
    Public Function UploadFile(ByVal filetoupload As Primitive, ByVal targetURL As Primitive) As Primitive
        Try
            My.Computer.Network.UploadFile(filetoupload, targetURL)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Transforms a given text into a google search link with the specified search text.
    ''' </summary>
    ''' <param name="text">The text.</param>
    ''' <returns>
    ''' The full link of the search.
    ''' Or FAILED if fails.
    ''' </returns>
    Public Function SearchGoogle(ByVal text As Primitive) As Primitive
        Try
            Return "http://www.google.com/search?hl=en&q=" + text
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Transforms a given text into a youtube search link with the specified search text.
    ''' </summary>
    ''' <param name="text">The text.</param>
    ''' <returns>
    ''' The full link of the search.
    ''' Or FAILED if fails.
    ''' </returns>
    Public Function SearchYoutube(ByVal text As Primitive) As Primitive
        Try
            Return "http://www.youtube.com/results?search_type=&search_query=" + text
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Gets the ip of the specified network adapter.
    ''' </summary>
    ''' <param name="networkID">The network ID, just go on and experience with values, it can vary very much.</param>
    ''' <returns>The IP of the specified network adapater by his ID or FAILED if it dosen't exists.</returns>
    Public Function GetIP(ByVal networkID As Primitive) As Primitive
        Try
            Dim myhost As String = System.Net.Dns.GetHostName()
            Dim myip As String = System.Net.Dns.GetHostAddresses(myhost).GetValue(networkID).ToString
            Return myip.ToString
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Gets the internet IP of this computer.
    ''' </summary>
    Public ReadOnly Property InternetIP As Primitive
        Get
            Try
                Dim getip As String = New WebClient().DownloadString("http://automation.whatismyip.com/n09230945.asp")
                Return getip
            Catch ex As Exception
                Return "FAILED"
            End Try
        End Get
    End Property
    ''' <summary>
    ''' Gets the value of a attribute specified by it's name and it's parent (tag) in a web page.
    ''' </summary>
    ''' <param name="page">The web page.</param>
    ''' <param name="tag">The tag of the attribute</param>
    ''' <param name="attribute">The attribute name</param>
    ''' <returns>The value of the attribute</returns>
    Public Function GetAttributeValue(ByVal page As Primitive, ByVal tag As Primitive, ByVal attribute As Primitive) As Primitive
        Try
            Dim page1 As New WebBrowser
            Dim toreturn As Primitive
            With page1
                .ScriptErrorsSuppressed = True
                .IsWebBrowserContextMenuEnabled = False
                .ScrollBarsEnabled = False
                .WebBrowserShortcutsEnabled = False
                .Navigate(page)
            End With
            Dim pageelements As HtmlElementCollection = page1.Document.GetElementsByTagName(tag)
            For Each curelement As HtmlElement In pageelements
                toreturn = curelement.GetAttribute(attribute)
            Next
            Return toreturn
            page1.Navigate("about:blank")
            page1.Dispose()
            page1.Hide()
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
End Module
''' <summary>
''' Used to manipulate procceses.
''' </summary>
''' <remarks></remarks>
<SmallBasicType()> Public Module SPProcess
    ''' <summary>
    ''' Starts a program.
    ''' </summary>
    ''' <param name="filepath">The filepath where the program is.</param>
    Public Function Start(ByVal filepath As Primitive) As Primitive
        Try
            Process.Start(filepath)
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Starts a program with the given arguments.
    ''' </summary>
    ''' <param name="filepath">The filepath where the program is.</param>
    ''' <param name="arguments">The arguments.</param>
    Public Function StartWithArguments(ByVal filepath As Primitive, ByVal arguments As Primitive) As Primitive
        Try
            Process.Start(filepath, arguments)
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Gets all the processes.
    ''' </summary>
    ''' <returns>The procceses in an array.</returns>
    Public Function GetProcesses() As Primitive
        Try
            Dim primitive As Primitive
            Dim count As Integer = 0
            Dim procs As Process()
            procs = Process.GetProcesses
            For Each proc In procs
                primitive.Item(count) = proc.ProcessName
                count = count + 1
            Next
            Return primitive
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    Public Function StopByID(ByVal ID As Primitive) As Primitive
        Process.GetProcessById(ID).Kill()
    End Function
    Public Function StopByName(ByVal name As Primitive) As Primitive
        Dim proctostop As Process() = Process.GetProcessesByName(name)
        For Each proc In proctostop
            proc.Kill()
        Next
    End Function
End Module
''' <summary>
''' Used to manipulate memory procceses functions.
''' Mostly used to modify values on programs.
''' USED FOR CHEATING AND HACKING.
''' I'm not reponsible if it dosen't work, or anything bad happens while hacking adresses.
''' That's up to you if you modify them.
''' </summary>
<SmallBasicType()> Public Module SPMemory
    Private TargetProcessHandle As Integer
    Private pfnStartAddr As Integer
    Private pszLibFileRemote As String
    Private TargetBufferSize As Integer

    Public Const PROCESS_VM_READ = &H10
    Public Const TH32CS_SNAPPROCESS = &H2
    Public Const MEM_COMMIT = 4096
    Public Const PAGE_READWRITE = 4
    Public Const PROCESS_CREATE_THREAD = (&H2)
    Public Const PROCESS_VM_OPERATION = (&H8)
    Public Const PROCESS_VM_WRITE = (&H20)
    Dim DLLFileName As String
    Public Declare Function ReadProcessMemory Lib "kernel32" ( _
    ByVal hProcess As Integer, _
    ByVal lpBaseAddress As Integer, _
    ByVal lpBuffer As String, _
    ByVal nSize As Integer, _
    ByRef lpNumberOfBytesWritten As Integer) As Integer

    Public Declare Function LoadLibrary Lib "kernel32" Alias "LoadLibraryA" ( _
    ByVal lpLibFileName As String) As Integer

    Public Declare Function VirtualAllocEx Lib "kernel32" ( _
    ByVal hProcess As Integer, _
    ByVal lpAddress As Integer, _
    ByVal dwSize As Integer, _
    ByVal flAllocationType As Integer, _
    ByVal flProtect As Integer) As Integer

    Public Declare Function WriteProcessMemory Lib "kernel32" ( _
    ByVal hProcess As Integer, _
    ByVal lpBaseAddress As Integer, _
    ByVal lpBuffer As String, _
    ByVal nSize As Integer, _
    ByRef lpNumberOfBytesWritten As Integer) As Integer

    Public Declare Function GetProcAddress Lib "kernel32" ( _
    ByVal hModule As Integer, ByVal lpProcName As String) As Integer

    Private Declare Function GetModuleHandle Lib "Kernel32" Alias "GetModuleHandleA" ( _
    ByVal lpModuleName As String) As Integer

    Public Declare Function CreateRemoteThread Lib "kernel32" ( _
    ByVal hProcess As Integer, _
    ByVal lpThreadAttributes As Integer, _
    ByVal dwStackSize As Integer, _
    ByVal lpStartAddress As Integer, _
    ByVal lpParameter As Integer, _
    ByVal dwCreationFlags As Integer, _
    ByRef lpThreadId As Integer) As Integer

    Public Declare Function OpenProcess Lib "kernel32" ( _
    ByVal dwDesiredAccess As Integer, _
    ByVal bInheritHandle As Integer, _
    ByVal dwProcessId As Integer) As Integer

    Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" ( _
    ByVal lpClassName As String, _
    ByVal lpWindowName As String) As Integer

    Private Declare Function CloseHandle Lib "kernel32" Alias "CloseHandleA" ( _
    ByVal hObject As Integer) As Integer
    ''' <summary>
    ''' Modifies an Integer 1,2,3...
    ''' </summary>
    ''' <param name="processname">The process name to modify the address, ex : skype,iw5mp...</param>
    ''' <param name="address">The adress to modify, ex : *asxterix*H12345678</param>
    ''' <param name="newvalue">The new value.</param>
    ''' <param name="size">
    ''' The size of the value
    ''' 1 (Byte), 2 (2 Bytes), 4 (4 Bytes (Mostly Used))
    ''' </param>
    Public Function ModifyInteger(ByVal processname As Primitive, ByVal address As Primitive, ByVal newvalue As Primitive, ByVal size As Primitive) As Primitive
        Try
            WriteInteger(processname, address, newvalue, size)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Modifies a Float 100.0F
    ''' </summary>
    ''' <param name="processname">The process name to modify the address, ex : skype,iw5mp...</param>
    ''' <param name="address">The adress to modify, ex : *asxterix*H12345678</param>
    ''' <param name="newvalue">The new value.</param>
    ''' <param name="size">
    ''' The size of the value
    ''' 1 (Byte), 2 (2 Bytes), 4 (4 Bytes (Mostly Used))
    ''' </param>
    Public Function ModifyFloat(ByVal processname As Primitive, ByVal address As Primitive, ByVal newvalue As Primitive, ByVal size As Primitive) As Primitive
        Try
            WriteFloat(processname, address, newvalue, size)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Modifies a Long 50.55
    ''' </summary>
    ''' <param name="processname">The process name to modify the address, ex : skype,iw5mp...</param>
    ''' <param name="address">The adress to modify, ex : *asxterix*H12345678</param>
    ''' <param name="newvalue">The new value.</param>
    ''' <param name="size">
    ''' The size of the value
    ''' 1 (Byte), 2 (2 Bytes), 4 (4 Bytes (Mostly Used))
    ''' </param>
    Public Function ModifyLong(ByVal processname As Primitive, ByVal address As Primitive, ByVal newvalue As Primitive, ByVal size As Primitive) As Primitive
        Try
            WriteLong(processname, address, newvalue, size)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Injects a given DLL into a process.
    ''' </summary>
    ''' <param name="processname">The process name, ex : skype,iw5mp</param>
    ''' <param name="dlltoinject">The file path of the DLL to inject.</param>
    ''' <returns>
    ''' SUCCESS if it succesfully injected.
    ''' FAILED if it didn't.
    ''' </returns>
    Public Function InjectDLL(ByVal processname As Primitive, ByVal dlltoinject As Primitive) As Primitive
        Try
            Dim TargetProcess As Process() = Process.GetProcessesByName(processname)
            TargetProcessHandle = OpenProcess(PROCESS_CREATE_THREAD Or PROCESS_VM_OPERATION Or PROCESS_VM_WRITE, False, TargetProcess(0).Id)
            pszLibFileRemote = dlltoinject
            pfnStartAddr = GetProcAddress(GetModuleHandle("Kernel32"), "LoadLibraryA")
            TargetBufferSize = 1 + Len(pszLibFileRemote)
            Dim Rtn As Integer
            Dim LoadLibParamAdr As Integer
            LoadLibParamAdr = VirtualAllocEx(TargetProcessHandle, 0, TargetBufferSize, MEM_COMMIT, PAGE_READWRITE)
            Rtn = WriteProcessMemory(TargetProcessHandle, LoadLibParamAdr, pszLibFileRemote, TargetBufferSize, 0)
            CreateRemoteThread(TargetProcessHandle, 0, 0, pfnStartAddr, LoadLibParamAdr, 0, 0)
            CloseHandle(TargetProcessHandle)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
End Module
''' <summary>
''' Specials characters that cannot be used in code, but now... it can !
''' </summary>
<SmallBasicType()> Public Module SPSpecialChars
    ''' <summary>
    ''' The new line character.
    ''' </summary>
    Public Property NewLine As Primitive = Text.GetCharacter(10)
    ''' <summary>
    ''' The quotation character which is "
    ''' </summary>
    Public Property Quotation As Primitive = Text.GetCharacter(34)
End Module
''' <summary>
''' Almost all the keys from the keyboard are in there.
''' </summary>
<SmallBasicType()> Public Module SPKeys
    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Long) As Integer
    ''' <summary>
    ''' The A key.
    ''' </summary>
    Public Property A As Primitive = "A"
    ''' <summary>
    ''' The Z key.
    ''' </summary>
    Public Property Z As Primitive = "Z"
    ''' <summary>
    ''' The E key.
    ''' </summary>
    Public Property E As Primitive = "E"
    ''' <summary>
    ''' The R key.
    ''' </summary>
    Public Property R As Primitive = "R"
    ''' <summary>
    ''' The T key.
    ''' </summary>
    Public Property T As Primitive = "T"
    ''' <summary>
    ''' The Y key.
    ''' </summary>
    Public Property Y As Primitive = "Y"
    ''' <summary>
    ''' The U key.
    ''' </summary>
    Public Property U As Primitive = "U"
    ''' <summary>
    ''' The I key.
    ''' </summary>
    Public Property I As Primitive = "I"
    ''' <summary>
    ''' The O key.
    ''' </summary>
    Public Property O As Primitive = "O"
    ''' <summary>
    ''' The P key.
    ''' </summary>
    Public Property P As Primitive = "P"
    ''' <summary>
    ''' The Q key.
    ''' </summary>
    Public Property Q As Primitive = "Q"
    ''' <summary>
    ''' The S key.
    ''' </summary>
    Public Property S As Primitive = "S"
    ''' <summary>
    ''' The D key.
    ''' </summary>
    Public Property D As Primitive = "D"
    ''' <summary>
    ''' The F key.
    ''' </summary>
    Public Property F As Primitive = "F"
    ''' <summary>
    ''' The G key.
    ''' </summary>
    Public Property G As Primitive = "G"
    ''' <summary>
    ''' The H key.
    ''' </summary>
    Public Property H As Primitive = "H"
    ''' <summary>
    ''' The J key.
    ''' </summary>
    Public Property J As Primitive = "J"
    ''' <summary>
    ''' The K key.
    ''' </summary>
    Public Property K As Primitive = "K"
    ''' <summary>
    ''' The L key.
    ''' </summary>
    Public Property L As Primitive = "L"
    ''' <summary>
    ''' The M key.
    ''' </summary>
    Public Property M As Primitive = "M"
    ''' <summary>
    ''' The W key.
    ''' </summary>
    Public Property W As Primitive = "W"
    ''' <summary>
    ''' The X key.
    ''' </summary>
    Public Property X As Primitive = "X"
    ''' <summary>
    ''' The C key.
    ''' </summary>
    Public Property C As Primitive = "C"
    ''' <summary>
    ''' The V key.
    ''' </summary>
    Public Property V As Primitive = "V"
    ''' <summary>
    ''' The B key.
    ''' </summary>
    Public Property B As Primitive = "B"
    ''' <summary>
    ''' The N key.
    ''' </summary>
    Public Property N As Primitive = "N"
    ''' <summary>
    ''' The 1 key.
    ''' </summary>
    Public Property D1 As Primitive = "D1"
    ''' <summary>
    ''' The 2 key.
    ''' </summary>
    Public Property D2 As Primitive = "D2"
    ''' <summary>
    ''' The 3 key.
    ''' </summary>
    Public Property D3 As Primitive = "D3"
    ''' <summary>
    ''' The 4 key.
    ''' </summary>
    Public Property D4 As Primitive = "D4"
    ''' <summary>
    ''' The 5 key.
    ''' </summary>
    Public Property D5 As Primitive = "D5"
    ''' <summary>
    ''' The 6 key.
    ''' </summary>
    Public Property D6 As Primitive = "D6"
    ''' <summary>
    ''' The 7 key.
    ''' </summary>
    Public Property D7 As Primitive = "D7"
    ''' <summary>
    ''' The 8 key.
    ''' </summary>
    Public Property D8 As Primitive = "D8"
    ''' <summary>
    ''' The 9 key.
    ''' </summary>
    Public Property D9 As Primitive = "D9"
    ''' <summary>
    ''' The 0 key.
    ''' </summary>
    Public Property D0 As Primitive = "D0"
    ''' <summary>
    ''' The ² Key on AZERTY Keyboards.
    ''' The ~ Key on QWERTY Keyboards.
    ''' </summary>
    Public Property OemQuotes As Primitive = "OemQuotes"
    ''' <summary>
    ''' The 1 key on the numpad.
    ''' </summary>
    Public Property Num1 As Primitive = "NumPad1"
    ''' <summary>
    ''' The 2 key on the numpad.
    ''' </summary>
    Public Property Num2 As Primitive = "NumPad2"
    ''' <summary>
    ''' The 3 key on the numpad.
    ''' </summary>
    Public Property Num3 As Primitive = "NumPad3"
    ''' <summary>
    ''' The 4 key on the numpad.
    ''' </summary>
    Public Property Num4 As Primitive = "NumPad4"
    ''' <summary>
    ''' The 5 key on the numpad.
    ''' </summary>
    Public Property Num5 As Primitive = "NumPad5"
    ''' <summary>
    ''' The 6 key on the numpad.
    ''' </summary>
    Public Property Num6 As Primitive = "NumPad6"
    ''' <summary>
    ''' The 7 key on the numpad.
    ''' </summary>
    Public Property Num7 As Primitive = "NumPad7"
    ''' <summary>
    ''' The 8 key on the numpad.
    ''' </summary>
    Public Property Num8 As Primitive = "NumPad8"
    ''' <summary>
    ''' The 9 key on the numpad.
    ''' </summary>
    Public Property Num9 As Primitive = "NumPad9"
    ''' <summary>
    ''' The 0 key on the numpad.
    ''' </summary>
    Public Property Num0 As Primitive = "NumPad0"
    Public Property Escape As Primitive = "Escape"
    Public Property Tab As Primitive = "Tab"
    Public Property Capital As Primitive = "Capital"
    Public Property LeftShift As Primitive = "LeftShift"
    Public Property LeftCtrl As Primitive = "LeftCtrl"
    Public Property RightShift As Primitive = "RightShift"
    Public Property RightCtrl As Primitive = "RightCtrl"
    Public Property LeftWindows As Primitive = "LWin"
    Public Property RightWindows As Primitive = "RWin"
    Public Property Space As Primitive = "Space"
    Public Property Enter As Primitive = "Return"
    Public Property Backspace As Primitive = "Back"
    Public Property NumDecimal As Primitive = "Decimal"
    Public Property Add As Primitive = "Add"
    Public Property Substract As Primitive = "Substract"
    Public Property Multiply As Primitive = "Multiply"
    Public Property Divide As Primitive = "Divide"
    Public Property NumLock As Primitive = "NumLock"
    Public Property VolumeUp As Primitive = "VolumeUp"
    Public Property VolumeDown As Primitive = "VolumeDown"
    Public Property VolumeMute As Primitive = "VolumeMute"
    Public Property UpArrow As Primitive = "Up"
    Public Property DownArrow As Primitive = "Down"
    Public Property LeftArrow As Primitive = "Left"
    Public Property RightArrow As Primitive = "Right"
    Public Property Insert As Primitive = "Insert"
    Public Property Home As Primitive = "Home"
    Public Property PageUp As Primitive = "PageUp"
    Public Property Delete As Primitive = "Delete"
    Public Property EndKey As Primitive = "End"
    Public Property NextKey As Primitive = "Next"
    Public Property None As Primitive = "None"
    Public Property UnknownChar As Primitive = "DeadCharProcessed"
    Public Property Comma As Primitive = "OemComma"
    Public Property Period As Primitive = "OemPeriod"
    Public Property Question As Primitive = "OemQuestion"
    Public Property Oem8 As Primitive = "Oem8"
    Public Property Oem3 As Primitive = "Oem3"
    Public Property Oem5 As Primitive = "Oem5"
    Public Property Oem6 As Primitive = "Oem6"
    Public Property Oem1 As Primitive = "Oem1"
    Public Property OemPlus As Primitive = "OemPlus"
    Public Property OemOpenBrackets As Primitive = "OemOpenBrackets"
    Public Property OemBackSlash As Primitive = "OemBackslash"
    Public Property F1 As Primitive = "F1"
    Public Property F2 As Primitive = "F2"
    Public Property F3 As Primitive = "F3"
    Public Property F4 As Primitive = "F4"
    Public Property F5 As Primitive = "F5"
    Public Property F6 As Primitive = "F6"
    Public Property F7 As Primitive = "F7"
    Public Property F8 As Primitive = "F8"
    Public Property F9 As Primitive = "F9"
    Public Property F10 As Primitive = "F10"
    Public Property F11 As Primitive = "F11"
    Public Property F12 As Primitive = "F12"
    ''' <summary>
    ''' Gets if the specified key is pressed or not
    ''' It acts like an hotkey yet, it means that even if you don't focus on the program, it will detect the key presses.
    ''' </summary>
    ''' <param name="key">The key, please use the keys properties for easyness.</param>
    ''' <returns>True if the key is pressed, False if it isn't, or FAILED if fails.</returns>
    Public Function GetKeyIsPressed(ByVal key As Primitive) As Primitive
        Try
            Dim hotkey As Boolean
            hotkey = GetAsyncKeyState(Text.GetCharacterCode(key.ToString))
            Return hotkey
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
End Module
''' <summary>
''' Used to manipulate java functions.
''' </summary>
<SmallBasicType()> Public Module SPJava
    Dim javaclass As Primitive = "class"
    Dim jar As Primitive = "jar"
    ''' <summary>
    ''' Compiles a file into a class file.
    ''' </summary>
    ''' <param name="filepath">The filepath</param>
    ''' <returns>
    ''' SUCCESS if it compiles.
    ''' FAILED if it dosen't.
    ''' </returns>
    Public Function Compile(ByVal filepath As Primitive) As Primitive
        Try
            Shell("javac " + filepath)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Runs a java file.
    ''' </summary>
    ''' <param name="filepath">The filepath.</param>
    ''' <param name="method">
    ''' The method to run the java program.
    ''' "jar" to run a .jar file
    ''' "class" to run a .class file
    ''' </param>
    ''' <returns>
    ''' SUCCESS if it runs
    ''' FAILED if it dosen't
    ''' </returns>
    Public Function Run(ByVal filepath As Primitive, ByVal method As Primitive) As Primitive
        If method = jar Then
            Try
                Shell("java -jar " + filepath + ".jar")
                Return "SUCCESS"
            Catch ex As Exception
                Return "FAILED"
                LastError = ex.ToString
            End Try
        ElseIf method = javaclass Then
            Try
                Shell("java " + filepath + ".class")
                Return "SUCCESS"
            Catch ex As Exception
                Return "FAILED"
                LastError = ex.ToString
            End Try
        End If
    End Function
End Module
''' <summary>
''' Used to manipulate dialogs.
''' </summary>
<SmallBasicType()> Public Module SPDialogs
    ''' <summary>
    ''' Shows a open file dialog.
    ''' </summary>
    ''' <param name="title">The title of the open file dialog.</param>
    ''' <param name="filter">
    ''' The filter of the open file dialog :
    ''' Exe Files |*.exe | MP3 |*.mp3
    ''' </param>
    ''' <param name="returnmethod">
    ''' 1 to return the full path.
    ''' 2 to return only the file name.
    ''' </param>
    ''' <returns>
    ''' The full path if the method is 1
    ''' The file name of the method was 2.
    ''' Else it returns FAILED.
    ''' </returns>
    ''' <remarks></remarks>
    Public Function ShowOpenDialog(ByVal title As Primitive, ByVal filter As Primitive, ByVal returnmethod As Primitive) As Primitive
        Dim opendialog1 As New OpenFileDialog
        With opendialog1
            .Title = title
            .Filter = filter
        End With
        opendialog1.ShowDialog()
        Try
            If returnmethod = 1 Then
                Try
                    Return opendialog1.FileName
                Catch ex As Exception
                    Return "FAILED"
                    LastError = ex.ToString
                End Try
            ElseIf returnmethod = 2 Then
                Try
                    Return opendialog1.SafeFileName
                Catch ex As Exception
                    Return "FAILED"
                    LastError = ex.ToString
                End Try
            End If
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Shows the save file dialog.
    ''' </summary>
    ''' <param name="title">The title of the save file dialog.</param>
    ''' <param name="filter">The filter, see openfiledialog for example.</param>
    ''' <returns>The full path or FAILED.</returns>
    Public Function ShowSaveDialog(ByVal title As Primitive, ByVal filter As Primitive) As Primitive
        Dim savedialog1 As New SaveFileDialog
        With savedialog1
            .Title = title
            .Filter = filter
        End With
        savedialog1.ShowDialog()
        Try
            Return savedialog1.FileName
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Shows the color dialog.
    ''' </summary>
    ''' <returns>The chosen color or FAILED.</returns>
    ''' <remarks></remarks>
    Public Function ShowColorDialog() As Primitive
        Dim colordialog1 As New ColorDialog
        With colordialog1
            .SolidColorOnly = True
            .AnyColor = True
            .AllowFullOpen = False
        End With
        colordialog1.ShowDialog()
        Try
            Return colordialog1.Color.ToString
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Shows the folder selection dialog.
    ''' </summary>
    ''' <param name="newfolderbutton">
    ''' Allow to create new folders from the dialog.
    ''' True or False.
    ''' </param>
    ''' <returns>The full path of the selected folder or FAILED.</returns>
    Public Function ShowFolderDialog(ByVal newfolderbutton As Primitive) As Primitive
        Dim folderdialog1 As New FolderBrowserDialog
        With folderdialog1
            Try
                .ShowNewFolderButton = newfolderbutton
                folderdialog1.ShowDialog()
                Try
                    Return folderdialog1.SelectedPath
                Catch ex As Exception
                    Return "FAILED"
                    LastError = ex.ToString
                End Try
            Catch ex As Exception
                Return "FAILED"
                LastError = ex.ToString
            End Try
        End With
    End Function
    ''' <summary>
    ''' Shows the font dialog.
    ''' </summary>
    ''' <returns>The selected font.</returns>
    Public Function ShowFontDialog() As Primitive
        Dim fontdialog1 As New FontDialog
        With fontdialog1
            .ShowEffects = False
            .ShowColor = False
            .ShowApply = False
        End With
        Try
            Return fontdialog1.Font.ToString
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
End Module
''' <summary>
''' Used to manipulate the LUA 5.1 language.
''' Go to : http://www.lua.org/manual/5.1/ for more information
''' </summary>
<SmallBasicType()> Public Module SPLua
    Dim LUAINTERFACE As New LuaInterface.Lua
    Dim LUA As New Lua511.LuaDLL
    Dim LUAERROR As LuaInterface.LuaException
    ''' <summary>
    ''' Runs a line of lua code.
    ''' </summary>
    ''' <param name="luastring">The line to run.</param>
    ''' <returns>
    ''' SUCCESS if the code ran.
    ''' The exception message if fails.
    ''' </returns>
    Public Function ExecuteLUACommand(ByVal luastring As Primitive) As Primitive
        Try
            LUAINTERFACE.DoString(luastring)
            Return "SUCCESS"
        Catch ex As Exception
            Return LUAERROR.Message
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Runs a .lua file.
    ''' </summary>
    ''' <param name="filepath">The filepath of the lua file.</param>
    ''' <returns>
    ''' SUCCESS if it ran the code.
    ''' The exception message if fails.
    ''' </returns>
    Public Function RunLUAFile(ByVal filepath As Primitive) As Primitive
        Try
            LUAINTERFACE.DoFile(filepath)
            Return "SUCCESS"
        Catch ex As Exception
            Return LUAERROR.Message
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Returns the current version of LUA.
    ''' </summary>
    Public Property LUAVersion As Primitive = "5.1"
End Module
''' <summary>
''' Used to manipulate files and directories.
''' </summary>
''' <remarks></remarks>
<SmallBasicType()> Public Module SPFile
    ''' <summary>
    ''' Get the number of directories in a specified directory.
    ''' </summary>
    ''' <param name="path">The path.</param>
    ''' <returns>The number of directories.</returns>
    ''' <remarks></remarks>
    Public Function NumberOfDirectories(ByVal path As Primitive) As Primitive
        Try
            Dim num = 0
            For Each direc In My.Computer.FileSystem.GetDirectories(path)
                num = num + 1
            Next
            Return num
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Get the number of files in a specified directory.
    ''' </summary>
    ''' <param name="path">The path.</param>
    ''' <returns>The number of files.</returns>
    ''' <remarks></remarks>
    Public Function NumberOfFiles(ByVal path As Primitive) As Primitive
        Try
            Dim num = 0
            For Each files In My.Computer.FileSystem.GetFiles(path)
                num = num + 1
            Next
            Return num
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Creates a directory.
    ''' </summary>
    ''' <param name="path">The path</param>
    ''' <returns>SUCCESS or FAILED.</returns>
    ''' <remarks></remarks>
    Public Function CreateDirectory(ByVal path As Primitive) As Primitive
        Try
            My.Computer.FileSystem.CreateDirectory(path)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Creates a new file.
    ''' </summary>
    ''' <param name="path">The path</param>
    ''' <param name="text">The text to write.</param>
    ''' <returns>SUCCESS or FAILED.</returns>
    ''' <remarks></remarks>
    Public Function CreateFile(ByVal path As Primitive, ByVal text As Primitive) As Primitive
        Try
            My.Computer.FileSystem.WriteAllText(path, text, False)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Deletes a file.
    ''' </summary>
    ''' <param name="path">The path.</param>
    ''' <param name="method">
    ''' The method to use :
    ''' "recycle" sends it to the recycle bin.
    ''' "delete" deletes it permanently.
    ''' </param>
    ''' <returns>SUCCESS or FAILED</returns>
    ''' <remarks></remarks>
    Public Function DeleteFile(ByVal path As Primitive, ByVal method As Primitive) As Primitive
        Try
            Dim methodtouse As String = method
            If methodtouse = "recycle" Then
                My.Computer.FileSystem.DeleteFile(path, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            ElseIf methodtouse = "delete" Then
                My.Computer.FileSystem.DeleteFile(path, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            End If
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Deletes a directory.
    ''' </summary>
    ''' <param name="path">The path</param>
    ''' <param name="method">
    ''' The method to use :
    ''' "recycle" sends it to the recycle bin.
    ''' "delete" deletes it permanently.
    ''' </param>
    ''' <returns>SUCCES or FAILED</returns>
    ''' <remarks></remarks>
    Public Function DeleteDirectory(ByVal path As Primitive, ByVal method As Primitive) As Primitive
        Try
            Dim methodtouse As String = method
            If methodtouse = "recycle" Then
                My.Computer.FileSystem.DeleteDirectory(path, FileIO.DeleteDirectoryOption.DeleteAllContents, FileIO.RecycleOption.SendToRecycleBin)
            ElseIf methodtouse = "delete" Then
                My.Computer.FileSystem.DeleteDirectory(path, FileIO.DeleteDirectoryOption.DeleteAllContents, FileIO.RecycleOption.DeletePermanently)
            End If
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Writes text to a file.
    ''' </summary>
    ''' <param name="path">The path</param>
    ''' <param name="text">The text to write</param>
    ''' <param name="append">If you should append or not.</param>
    ''' <returns>SUCCESS or FAILED</returns>
    ''' <remarks></remarks>
    Public Function Write(ByVal path As Primitive, ByVal text As Primitive, ByVal append As Primitive) As Primitive
        Try
            Dim append1 As String = append
            If append1 = "True" Then
                My.Computer.FileSystem.WriteAllText(path, text, True)
            ElseIf append1 = "False" Then
                My.Computer.FileSystem.WriteAllText(path, text, False)
            End If
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Reads a file.
    ''' </summary>
    ''' <param name="path">The path</param>
    ''' <returns>The text or FAILED</returns>
    ''' <remarks></remarks>
    Public Function Read(ByVal path As Primitive) As Primitive
        Try
            Return My.Computer.FileSystem.ReadAllText(path)
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Renames a file.
    ''' </summary>
    ''' <param name="oldname">The path of the file.</param>
    ''' <param name="newname">The new name.</param>
    ''' <returns>SUCCES or FAILED</returns>
    ''' <remarks></remarks>
    Public Function RenameFile(ByVal oldname As Primitive, ByVal newname As Primitive) As Primitive
        Try
            My.Computer.FileSystem.RenameFile(oldname, newname)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Renames a directory.
    ''' </summary>
    ''' <param name="oldname">The path of the directory.</param>
    ''' <param name="newname">The new name.</param>
    ''' <returns>SUCCESS or FAILED</returns>
    ''' <remarks></remarks>
    Public Function RenameDirectory(ByVal oldname As Primitive, ByVal newname As Primitive) As Primitive
        Try
            My.Computer.FileSystem.RenameDirectory(oldname, newname)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
            LastError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Gets the file's size in bytes.
    ''' </summary>
    ''' <param name="filepath">The file path.</param>
    ''' <returns>The size of the file in bytes or FAILED if fails.</returns>
    Public Function GetSize(ByVal filepath As Primitive) As Primitive
        Try
            Dim file1 = My.Computer.FileSystem.ReadAllText(filepath)
            Return Text.GetLength(file1)
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Get the directory's size in bytes with all files contained in there.
    ''' </summary>
    ''' <param name="path">The path</param>
    ''' <returns>The size in bytes or FAILED if fails.</returns>
    Public Function GetDirectorySize(ByVal path As Primitive) As Primitive
        Try
            Dim filesindir = Microsoft.SmallBasic.Library.File.GetFiles(path)
            Dim numoffiles = Microsoft.SmallBasic.Library.Array.GetItemCount(filesindir)
            Dim counter As Integer
            Dim size As Integer = 0
            For counter = 0 To numoffiles
                size = size + Text.GetLength(Microsoft.SmallBasic.Library.File.ReadContents(filesindir(counter)))
            Next
            Return size
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
End Module
''' <summary>
''' Used for security functions.
''' </summary>
<SmallBasicType()> Public Module SPSecurity
    ''' <summary>
    ''' Generates a password.
    ''' </summary>
    ''' <param name="length">The length of the password.</param>
    ''' <returns>The generated password or FAILED.</returns>
    Public Function GeneratePassword(ByVal length As Primitive) As Primitive
        Dim random As Random
        random = New Random
        Dim count As Integer
        Dim str = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
        Dim strlength = Text.GetLength(str)
        Dim newstr = ""
        For count = 1 To length Step 1
            newstr = newstr + Text.GetSubText(str, random.Next(0, strlength + 1), 1)
        Next
        Return newstr
    End Function
    ''' <summary>
    ''' Generates a hard password.
    ''' </summary>
    ''' <param name="length">The length of the password.</param>
    ''' <returns>The generated password or FAILED.</returns>
    Public Function GenerateHardPassword(ByVal length As Primitive) As Primitive
        Dim random As Random
        random = New Random
        Dim count As Integer
        Dim str = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz&é'(-è_çà)=^$ù*,;:!</*-+.~#{[|`\^@]}¨¤^£µ%§?²>"
        Dim strlength = Text.GetLength(str)
        Dim newstr = ""
        For count = 1 To length Step 1
            newstr = newstr + Text.GetSubText(str, random.Next(0, strlength + 1), 1)
        Next
        Return newstr
    End Function
    ''' <summary>
    ''' Generates a custom password.
    ''' </summary>
    ''' <param name="length">The length of the password.</param>
    ''' <param name="input">
    ''' The input of the password
    ''' For example an input with "abc123" will only generate the password with those characters.
    ''' </param>
    ''' <returns>The generated password or FAILED.</returns>
    Public Function GenerateCustomPassword(ByVal length As Primitive, ByVal input As Primitive) As Primitive
        Dim random As Random
        random = New Random
        Dim count As Integer
        Dim str = input.ToString
        Dim strlength = Text.GetLength(str)
        Dim newstr = ""
        For count = 1 To length Step 1
            newstr = newstr + Text.GetSubText(str, Random.Next(0, strlength + 1), 1)
        Next
        Return newstr
    End Function
End Module
''' <summary>
''' Used to get errors.
''' </summary>
''' <remarks></remarks>
<SmallBasicType()> Public Module SPError
    ''' <summary>
    ''' Will return the last error.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The last error.</returns>
    Public Property LastError As Primitive = "Null"
End Module
''' <summary>
''' This is a currency converter.
''' </summary>
''' <remarks></remarks>
<SmallBasicType()> Public Module SPConverter
    ''' <summary>
    ''' Converts Euros to Dollars.
    ''' </summary>
    ''' <param name="euro">The number of euros.</param>
    ''' <returns>The dollar value.</returns>
    Public Function ConvEuroToDollar(ByVal euro As Primitive) As Primitive
        Return euro * 1.34119
    End Function
    ''' <summary>
    ''' Converts Dollars to Euros.
    ''' </summary>
    ''' <param name="dollar">The number of dollars.</param>
    ''' <returns>The euros value.</returns>
    Public Function ConvDollarToEuro(ByVal dollar As Primitive) As Primitive
        Return dollar * 0.745601
    End Function
    ''' <summary>
    ''' Converts a custom currency.
    ''' </summary>
    ''' <param name="currency">The currency you wish to convert.</param>
    ''' <param name="base">
    ''' The base 1 of the currency
    ''' For example, to convert euros to dollar, we know that "euro" = 2, and to convert it to dollars we did euro*1.34119
    ''' </param>
    ''' <returns>The converted currency</returns>
    Public Function ConvCustom(ByVal currency As Primitive, ByVal base As Primitive) As Primitive
        Return currency * base
    End Function
End Module
''' <summary>
''' Used to do things on minecraft servers.
''' </summary>
<SmallBasicType()> Public Module SPMinecraftServer
    Dim minecraftplayer As json.net.player
    Dim minecraftserver As json.net.server
    Dim minecraftgeneric As json.net.generic
    Dim minecraft As json.net.minecraft
    ''' <summary>
    ''' If you want to control a specific server, then call this function.
    ''' </summary>
    ''' <param name="ip">The IP of the server.</param>
    ''' <param name="password">The password, leave blank if there's no password.</param>
    ''' <param name="port">The port of the server, by default 25565</param>
    ''' <param name="authas">Authentificate as a player.</param>
    Public Function Init(ByVal ip As Primitive, ByVal password As Primitive, ByVal port As Primitive, ByVal authas As Primitive) As Primitive
        If port = Nothing Then
            port = 25565
        End If
        Try
            generic.InitJSONAPI(ip, password, port, authas)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Adds a player to the whitelist.
    ''' </summary>
    ''' <param name="player">The player name</param>
    Public Function AddToWhitelist(ByVal player As Primitive) As Primitive
        Try
            server.addToWhitelist(player)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Broadcasts a message to the server.
    ''' </summary>
    ''' <param name="message">The message.</param>
    Public Function BroadcastMessage(ByVal message As Primitive) As Primitive
        Try
            player.broadcastMessage(message)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Disables a plugin.
    ''' </summary>
    ''' <param name="plugin">The plugin name.</param>
    Public Function DisablePlugin(ByVal plugin As Primitive) As Primitive
        Try
            server.disablePlugin(plugin)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Enables a plugin.
    ''' </summary>
    ''' <param name="plugin">The plugin name.</param>
    Public Function EnablePlugin(ByVal plugin As Primitive) As Primitive
        Try
            server.enablePlugin(plugin)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Gets the ID of a block.
    ''' </summary>
    ''' <param name="x">The X Vector</param>
    ''' <param name="y">The Y Vector</param>
    ''' <param name="z">The Z Vector</param>
    ''' <returns>The Block ID at the speficied vector.</returns>
    Public Function GetBlockID(ByVal x As Primitive, ByVal y As Primitive, ByVal z As Primitive) As Primitive
        Try
            Return minecraft.getBlockID(x, y, z)
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Gets the cuboid of a player.
    ''' </summary>
    ''' <param name="player">The player name.</param>
    ''' <returns>The cuboid.</returns>
    Public Function GetCuboid(ByVal player As Primitive) As Primitive
        Try
            Return minecraft.getCuboid(player)
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Gets the MODT of the server (Message of the day.)
    ''' </summary>
    Public Property Modt As Primitive = server.getMotd()
    ''' <summary>
    ''' Gets the player count of the server.
    ''' </summary>
    Public Property PlayerCount As Primitive = server.getPlayerCount()
    ''' <summary>
    ''' Gets the max player count of the server.
    ''' </summary>
    Public Property MaxPlayerCount As Primitive = server.getPlayerLimit()
    ''' <summary>
    ''' Gets the sign of a text.
    ''' </summary>
    ''' <param name="x">The X Vector.</param>
    ''' <param name="y">The Y Vector.</param>
    ''' <param name="z">The Z Vector.</param>
    ''' <param name="line">The line number.</param>
    ''' <returns>The text of the specified line of the specified sign at the specified vector.</returns>
    ''' <remarks></remarks>
    Public Function GetSignText(ByVal x As Primitive, ByVal y As Primitive, ByVal z As Primitive, ByVal line As Primitive) As Primitive
        Try
            Return minecraft.getSignText(x, y, z, line)
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Gets or sets the time of the server.
    ''' </summary>
    ''' <value>
    ''' The time value
    ''' 0 for dawn
    ''' 6000 for day
    ''' 12000 for dusk
    ''' 18000 for night
    ''' 24000 = 0
    ''' </value>
    ''' <returns>The time of the server.</returns>
    Public Property Time As Primitive
        Get
            Try
                Return minecraft.getTime()
            Catch ex As Exception
                Return "FAILED"
            End Try
        End Get
        Set(ByVal value As Primitive)
            minecraft.setTime(value)
        End Set
    End Property
    ''' <summary>
    ''' Kicks a player from the server.
    ''' </summary>
    ''' <param name="player">The player name.</param>
    ''' <param name="reason">The reason.</param>
    Public Function Kick(ByVal player As Primitive, ByVal reason As Primitive) As Primitive
        Try
            json.net.player.kick(player, reason)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' The all the players name in the server.
    ''' </summary>
    Public Property GetPlayers As Primitive = player.getPlayers().ToString
    ''' <summary>
    ''' Gets the vector position of a player.
    ''' </summary>
    ''' <param name="player">The player name.</param>
    ''' <returns>The position vector.</returns>
    Public Function GetPos(ByVal player As Primitive) As Primitive
        Try
            Return json.net.player.getPos(player)
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Gives a block or an item to a player.
    ''' </summary>
    ''' <param name="player">The player name.</param>
    ''' <param name="blockoritemid">The block or item ID</param>
    ''' <param name="amount">The amount 1-64</param>
    Public Function Give(ByVal player As Primitive, ByVal blockoritemID As Primitive, ByVal amount As Primitive) As Primitive
        Try
            json.net.player.giveItem(player, blockoritemID, amount)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Writes a RAINBOW text, this is a debug function, to see if it works.
    ''' </summary>
    Public Function GoRainbow() As Primitive
        Try
            player.goRainbowed()
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Sets the block at the specified position.
    ''' </summary>
    ''' <param name="x">The X Vector</param>
    ''' <param name="y">The Y Vector</param>
    ''' <param name="z">The Z Vector</param>
    ''' <param name="blockid">The block ID</param>
    Public Function SetBlock(ByVal x As Primitive, ByVal y As Primitive, ByVal z As Primitive, ByVal blockid As Primitive) As Primitive
        Try
            minecraft.setBlockID(x, y, z, blockid)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Sets the sign of a text
    ''' </summary>
    ''' <param name="x">The X Vector</param>
    ''' <param name="y">The Y Vector</param>
    ''' <param name="z">The Z Vector</param>
    ''' <param name="line">The line number.</param>
    ''' <param name="text">The text.</param>
    Public Function SetSignText(ByVal x As Primitive, ByVal y As Primitive, ByVal z As Primitive, ByVal line As Primitive, ByVal text As Primitive) As Primitive
        Try
            minecraft.setSignText(x, y, z, line, text)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Shows if Whitelist is enabled or not.
    ''' </summary>
    ''' <returns>TRUE or FALSE</returns>
    Public ReadOnly Property WhitelistEnabled As Primitive
        Get
            Return server.isWhitelistEnabled
        End Get
    End Property
    ''' <summary>
    ''' Runs a console command.
    ''' </summary>
    ''' <param name="command">The command</param>
    Public Function RunConsoleCommand(ByVal command As Primitive) As Primitive
        Try
            server.runConsoleCommand(command)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Runs a server command
    ''' </summary>
    ''' <param name="command">
    ''' The command to run
    ''' Ex : time set 0
    ''' kick Notch
    ''' gamemode Jeb 1
    ''' ...
    ''' </param>
    Public Function RunServerCommand(ByVal command As Primitive) As Primitive
        Try
            server.runServerCommand(command)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Sends a message to a player.
    ''' </summary>
    ''' <param name="player">The player name.</param>
    ''' <param name="message">The message.</param>
    Public Function SendMessage(ByVal player, ByVal message) As Primitive
        Try
            json.net.player.sendMessage(player, message)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Toggles mute for a player.
    ''' </summary>
    ''' <param name="player">The player name.</param>
    Public Function ToggleMute(ByVal player As Primitive) As Primitive
        Try
            json.net.player.toggleMute(player)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
End Module
''' <summary>
''' This is a Stop Watch, used to measure time.
''' </summary>
<SmallBasicType()> Public Module SPStopWatch
    Dim WithEvents sw As New Stopwatch
    Dim WithEvents timer1 As New Timers.Timer
    ''' <summary>
    ''' Starts the Stop Watch.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function StartStopWatch() As Primitive
        sw.Start()
    End Function
    ''' <summary>
    ''' Pauses/Stops the Stop Watch.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PauseStopWatch() As Primitive
        sw.Stop()
    End Function
    Public Property ElapsedMilliseconds As Primitive
        Get
            Return sw.ElapsedMilliseconds
        End Get
        Set(ByVal value As Primitive)

        End Set
    End Property
    Public Property ElapsedTicks As Primitive
        Get
            Return sw.ElapsedTicks
        End Get
        Set(ByVal value As Primitive)

        End Set
    End Property
    Public Property ElapsedSeconds As Primitive
        Get
            Return sw.ElapsedMilliseconds / 1000
        End Get
        Set(ByVal value As Primitive)

        End Set
    End Property
    Public Property ElapsedMinutes As Primitive
        Get
            Return sw.ElapsedMilliseconds / 1000 / 60
        End Get
        Set(ByVal value As Primitive)

        End Set
    End Property
    Public Property ElapsedHours As Primitive
        Get
            Return sw.ElapsedMilliseconds / 1000 / 60 / 60
        End Get
        Set(ByVal value As Primitive)

        End Set
    End Property
    Public Property ElapsedDays As Primitive
        Get
            Return sw.ElapsedMilliseconds / 1000 / 60 / 60 / 24
        End Get
        Set(ByVal value As Primitive)

        End Set
    End Property
    Public Property ElapsedWeeks As Primitive
        Get
            Return sw.ElapsedMilliseconds / 1000 / 60 / 60 / 24 / 7
        End Get
        Set(ByVal value As Primitive)

        End Set
    End Property
    Public Property ElapsedMonths As Primitive
        Get
            Return sw.ElapsedMilliseconds / 1000 / 60 / 60 / 24 / 30
        End Get
        Set(ByVal value As Primitive)

        End Set
    End Property
    Public Property ElapsedYears As Primitive
        Get
            Return sw.ElapsedMilliseconds / 1000 / 60 / 60 / 24 / 30 / 12
        End Get
        Set(ByVal value As Primitive)

        End Set
    End Property
    Public Property ElapsedCenturies As Primitive
        Get
            Return sw.ElapsedMilliseconds / 1000 / 60 / 60 / 24 / 30 / 12 / 100
        End Get
        Set(ByVal value As Primitive)

        End Set
    End Property
    Public Property ElapsedMillenias As Primitive
        Get
            Return sw.ElapsedMilliseconds / 1000 / 60 / 60 / 24 / 30 / 12 / 1000
        End Get
        Set(ByVal value As Primitive)

        End Set
    End Property
    ''' <summary>
    ''' Resets the Stop Watch.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ResetStopWatch() As Primitive
        sw.Reset()
    End Function
End Module
''' <summary>
''' Used to manipulate Windows functions and properties.
''' </summary>
''' <remarks></remarks>
<SmallBasicType()> Public Module SPWindows
    ''' <summary>
    ''' The HWID...
    ''' Your very UNIQUE computer ID, it is unique in each computer in the world.
    ''' This information is in your proccesor, you CANT change it unless you use hack software to change it OR change your computer/proccesor.
    ''' It can be useful for PC authentifications, pretty much an unique login ID.
    ''' </summary>
    ''' <returns>Your HWID.</returns>
    Public Property HWID As Primitive
        Get
            Return System.Security.Principal.WindowsIdentity.GetCurrent.User.Value
        End Get
        Set(ByVal value As Primitive)

        End Set
    End Property
    Public ReadOnly Property TotalRAMinKB As Primitive
        Get
            Return My.Computer.Info.TotalPhysicalMemory / 1000
        End Get
    End Property
    Public ReadOnly Property TotalRAMinMB As Primitive
        Get
            Return Floor(My.Computer.Info.TotalPhysicalMemory / 1000000)
        End Get
    End Property
    Public ReadOnly Property AvaliableRAMinKB As Primitive
        Get
            Return My.Computer.Info.AvailablePhysicalMemory / 1000
        End Get
    End Property
    Public ReadOnly Property AvaliableRAMinMB As Primitive
        Get
            Return Floor(My.Computer.Info.AvailablePhysicalMemory / 1000000)

        End Get
    End Property
    Public ReadOnly Property UsedRAMinKB As Primitive
        Get
            Return (My.Computer.Info.TotalPhysicalMemory / 1000 - My.Computer.Info.AvailablePhysicalMemory / 1000)
        End Get
    End Property
    Public ReadOnly Property UsedRAMinMB As Primitive
        Get
            Return Floor((My.Computer.Info.TotalPhysicalMemory - My.Computer.Info.AvailablePhysicalMemory) / 1000000)
        End Get
    End Property
    Public ReadOnly Property UsedRAMinPercent As Primitive
        Get
            Return (My.Computer.Info.AvailablePhysicalMemory / My.Computer.Info.TotalPhysicalMemory) * 100
        End Get
    End Property
    Public ReadOnly Property AvaliableRAMinPercent As Primitive
        Get
            Return 100 - ((My.Computer.Info.AvailablePhysicalMemory / My.Computer.Info.TotalPhysicalMemory) * 100)
        End Get
    End Property
    Public ReadOnly Property OSName As Primitive
        Get
            Return My.Computer.Info.OSFullName

        End Get
    End Property
    Public ReadOnly Property OSVersion As Primitive
        Get
            Return My.Computer.Info.OSVersion
        End Get
    End Property
    Public ReadOnly Property ScreenDeviceName As Primitive
        Get
            Return My.Computer.Screen.DeviceName.ToString
        End Get
    End Property
    Public ReadOnly Property ScreenBitsPerPixel As Primitive
        Get
            Return My.Computer.Screen.BitsPerPixel
        End Get
    End Property
    Public Function OpenSerialPort(ByVal portname As Primitive) As Primitive
        Try
            My.Computer.Ports.OpenSerialPort(portname)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
End Module
''' <summary>
''' Used to manipulate text functions.
''' </summary>
<SmallBasicType()> Public Module SPText
    ''' <summary>
    ''' Checks if the specified text is empty or not.
    ''' </summary>
    ''' <param name="text">The text.</param>
    ''' <returns>True or False or FAILED if fails.</returns>
    Public Function IsEmpty(ByVal text As Primitive) As Primitive
        Try
            Dim toreturn = String.IsNullOrEmpty(text)
            Return toreturn.ToString
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Checks how many specified chars are in the specified text.
    ''' </summary>
    ''' <param name="data">The text to check.</param>
    ''' <param name="subChar">The char.</param>
    ''' <returns>
    ''' An array of the count of chars and their position
    ''' The count of the chars is indexed by 0 according to the array
    ''' And their position according to the array start by 1 and goes on...
    ''' </returns>
    Public Function NumOfChars(ByVal data As Primitive, ByVal subChar As Primitive) As Primitive
        Try
            Dim length = Text.GetLength(data)
            Dim indexcount As Integer = 0
            Dim toreturn As Primitive = Nothing
            Dim count As Integer = Nothing
            Dim countofchar As Integer = 0
            For count = 0 To length
                Dim chartocheck = Text.GetSubText(data, count, 1)
                If chartocheck.ToString = subChar.ToString Then
                    countofchar = countofchar + 1
                End If
            Next
            toreturn.Item(0) = countofchar
            Dim count2 As Integer = 0
            Dim charposcounter As Integer = 1
            For count2 = 0 To length
                Dim chartocheck = Text.GetSubText(data, count, 1)
                If chartocheck.ToString = subChar.ToString Then
                    toreturn.Item(charposcounter) = Text.GetIndexOf(data, chartocheck)
                    charposcounter = charposcounter + 1
                End If
            Next
            Return toreturn
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Gets the arguments in a text.
    ''' </summary>
    ''' <param name="text">The text.</param>
    ''' <returns>The splited text in an array, started by 1.</returns>
    Public Function GetArguments(ByVal text As Primitive) As Primitive
        Try
            Dim toreturn As Primitive
            Dim text1 = text.ToString.Split(" ")
            For count = 1 To text1.Count
                toreturn(count) = text1(count - 1)
            Next
            Return toreturn
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
End Module
''' <summary>
''' Used to manipulate textwindow functions.
''' It actually manipuate the original TextWindow Object, this is not an external TextWindow.
''' </summary>
<SmallBasicType()> Public Module SPTextWindow
    ''' <summary>
    ''' Draws a pixel at the specified position and color.
    ''' </summary>
    ''' <param name="x">The X value, should be inferior or equal to 79.</param>
    ''' <param name="y">The Y value.</param>
    ''' <param name="color">
    ''' The color
    ''' It can be : "blue,green,yellow,red,gray,black,white"
    ''' The colors are very limited due to the textwindow.
    ''' </param>
    ''' <returns>SUCCESS or FAILED if fails.</returns>
    Public Function DrawPixel(ByVal x As Primitive, ByVal y As Primitive, ByVal color As Primitive) As Primitive
        Try
            If x >= 80 Then
                Throw New Exception("The value X must be inferior or equal than 79 !")
                Return "ERROR"
            Else
                Dim backcolor As String = TextWindow.BackgroundColor.ToString
                Dim cursorx As Integer = TextWindow.CursorLeft
                Dim cursory As Integer = TextWindow.CursorTop
                TextWindow.CursorLeft = x
                TextWindow.CursorTop = y
                TextWindow.BackgroundColor = color
                TextWindow.Write(" ")
                TextWindow.CursorLeft = cursorx
                TextWindow.CursorTop = cursory
                TextWindow.BackgroundColor = backcolor
                Return "SUCCESS"
            End If
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Creates a group of pixels in the text window in the same color.
    ''' </summary>
    ''' <param name="x">The left value to begin.</param>
    ''' <param name="y">The top value to begin</param>
    ''' <param name="width">The width</param>
    ''' <param name="height">The height</param>
    ''' <param name="color">
    ''' The color
    ''' It can be : "blue,green,yellow,red,gray,black,white"
    ''' The colors are very limited due to the textwindow.
    ''' </param>
    ''' <returns>SUCCESS or FAILED if fails.</returns>
    Public Function DrawGroupPixels(ByVal x As Primitive, ByVal y As Primitive, ByVal width As Primitive, ByVal height As Primitive, ByVal color As Primitive) As Primitive
        Try
            Dim lastcolor = TextWindow.BackgroundColor.ToString
            TextWindow.BackgroundColor = color.ToString
            Dim count As Integer
            Dim curx As Integer = x
            Dim cury As Integer = y
            For count = 1 To width * height
                TextWindow.CursorLeft = curx
                TextWindow.CursorTop = cury
                TextWindow.WriteLine(" ")
                curx = curx + 1
                If curx = x + width Then
                    curx = x
                    cury = cury + 1
                End If
            Next
            TextWindow.BackgroundColor = lastcolor
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Prints a text to the textwindow with a delay between each character.
    ''' </summary>
    ''' <param name="text1">The text</param>
    ''' <param name="delay">The delay</param>
    ''' <returns></returns>
    Public Function TextPrint(ByVal text1 As Primitive, ByVal delay As Primitive) As Primitive
        Dim text2 = text1.ToString
        Dim length As Integer = Text.GetLength(text1)
        For count = 1 To length
            TextWindow.Write(Text.GetSubText(text1, count, 1))
            Program.Delay(delay)
        Next
    End Function
    ''' <summary>
    ''' Does a return to the line in the textwindow.
    ''' </summary>
    Public Function ReturnLine() As Primitive
        TextWindow.WriteLine(Nothing)
    End Function
    ''' <summary>
    ''' Writes a text in the center of the current line.
    ''' </summary>
    ''' <param name="data">The data. (Text, numbers, characters)</param>
    ''' <returns>SUCCESS or FAILED if fails.</returns>
    Public Function WriteCenter(ByVal data As Primitive) As Primitive
        Try
            TextWindow.CursorLeft = (80 / 2) - (Text.GetLength(data) / 2)
            TextWindow.WriteLine(data)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Inserts a separator in the current line.
    ''' </summary>
    ''' <returns>SUCCESS or FAILED if fails.</returns>
    Public Function Separator() As Primitive
        Try
            TextWindow.Write("--------------------------------------------------------------------------------")
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Same as doing the WriteCenter("Example") and then Seperator()
    ''' </summary>
    ''' <param name="data">The title.</param>
    ''' <returns>SUCCESS or FAILED if fails.</returns>
    Public Function Title(ByVal data As Primitive) As Primitive
        Try
            TextWindow.CursorLeft = (80 / 2) - (Text.GetLength(data) / 2)
            TextWindow.WriteLine(data)
            TextWindow.Write("--------------------------------------------------------------------------------")
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Clears the specified line in the text window.
    ''' </summary>
    ''' <param name="line">The line number, it can be "Current" for the current line.</param>
    ''' <returns>SUCCESS or FAILED if fails.</returns>
    Public Function ClearLine(ByVal line As Primitive) As Primitive
        Try
            If line.ToString = "Current" Then
                Dim curtop = TextWindow.CursorTop
                TextWindow.CursorTop = curtop
                TextWindow.CursorLeft = 0
                TextWindow.Write("                                                                               ")
                TextWindow.CursorLeft = 0
                TextWindow.CursorTop = curtop
            Else
                Dim curtop = TextWindow.CursorTop
                TextWindow.CursorTop = line
                TextWindow.CursorLeft = 0
                TextWindow.Write("                                                                               ")
                TextWindow.CursorLeft = 0
                TextWindow.CursorTop = curtop
            End If
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Clears the specified selection of the specified line in the text window.
    ''' </summary>
    ''' <param name="line">The line number, it can be "Current" for the current line.</param>
    ''' <param name="start">The start index.</param>
    ''' <param name="length">The length of the selection.</param>
    ''' <returns>SUCCESS or FAILED if fails.</returns>
    Public Function ClearLineSelection(ByVal line As Primitive, ByVal start As Primitive, ByVal length As Primitive) As Primitive
        Try
            If line.ToString = "Current" Then
                Dim count As Integer
                Dim curtop = TextWindow.CursorTop
                TextWindow.CursorTop = curtop
                TextWindow.CursorLeft = start
                For count = 1 To length
                    TextWindow.Write(" ")
                Next
                TextWindow.CursorLeft = 0
                TextWindow.CursorTop = curtop
            Else
                Dim count As Integer
                Dim curtop = TextWindow.CursorTop
                TextWindow.CursorTop = line
                TextWindow.CursorLeft = start
                For count = 1 To length
                    TextWindow.Write(" ")
                Next
                TextWindow.CursorLeft = 0
                TextWindow.CursorTop = curtop
            End If
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
End Module
''' <summary>
''' Used for general functions.
''' </summary>
<SmallBasicType()> Public Module SPProgram

    Dim WithEvents timer1 As New System.Timers.Timer()
    ''' <summary>
    ''' Quits the program.
    ''' </summary>
    Public Function Quit() As Primitive
        Program.End()
    End Function
    ''' <summary>
    ''' The tick event, this is an event that is called at every frame.
    ''' </summary>
    Public Event Tick As SmallBasicCallback
    ''' <summary>
    ''' If it should allow the tick event or not.
    ''' </summary>
    ''' <param name="bool">True or False</param>
    ''' <returns>SUCCESS or FAILED if fails.</returns>
    Public Function AllowTick(ByVal bool As Primitive) As Primitive
        Try
            If bool.ToString = "True" Then
                timer1.Interval = 1
                timer1.Start()
            ElseIf bool.ToString = "False" Then
                timer1.Stop()
            End If
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    Private Sub timer1_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timer1.Elapsed
        RaiseEvent Tick()
    End Sub
End Module
''' <summary>
''' Mainly used for notifier.
''' </summary>
<SmallBasicType()> Public Module SPTaskbar
    Dim WithEvents notifier As New NotifyIcon
    ''' <summary>
    ''' The text of the notifier when you pass the mouse on the icon.
    ''' </summary>
    Public Property NotifierText As Primitive = ""
    ''' <summary>
    ''' If the notifier should be visible or not.
    ''' </summary>
    Public Property Visible As Primitive
        Get
            Return notifier.Visible.ToString
        End Get
        Set(ByVal value As Primitive)
            If value.ToString = "True" Then
                notifier.Visible = True
            ElseIf value.ToString = "False" Then
                notifier.Visible = False
            End If
        End Set
    End Property
    ''' <summary>
    ''' Enables the notifier.
    ''' </summary>
    ''' <param name="icon">The icon to use.</param>
    ''' <returns>SUCCESS or FAILED if fails.</returns>
    Public Function EnableNotifier(ByVal icon As Primitive) As Primitive
        Try
            notifier.Icon = New Icon(icon.ToString)
            notifier.Visible = True
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Disables the notifier.
    ''' </summary>
    ''' <returns>SUCCESS or FAILED if fails.</returns>
    ''' <remarks></remarks>
    Public Function DisableNotifier() As Primitive
        Try
            notifier.Visible = False
            notifier.Icon = Nothing
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Shows a ballon tip notification.
    ''' </summary>
    ''' <param name="text">The text to show.</param>
    ''' <param name="tipicon">
    ''' The icon to use
    ''' It can be : "Warning,Error,Info,None" or anything else for no icon
    ''' </param>
    ''' <param name="title">The title.</param>
    ''' <param name="timeout">The timeout in milliseconds</param>
    ''' <returns>SUCCESS or FAILED if fails.</returns>
    Public Function ShowNotification(ByVal text As Primitive, ByVal tipicon As Primitive, ByVal title As Primitive, ByVal timeout As Primitive) As Primitive
        Try
            With notifier
                If tipicon.ToString = "Error" Then
                    .ShowBalloonTip(timeout, title, text, ToolTipIcon.Error)
                ElseIf tipicon.ToString = "Info" Then
                    .ShowBalloonTip(timeout, title, text, ToolTipIcon.Info)
                ElseIf tipicon.ToString = "Warning" Then
                    .ShowBalloonTip(timeout, title, text, ToolTipIcon.Warning)
                Else
                    .ShowBalloonTip(timeout, title, text, ToolTipIcon.None)
                End If
            End With
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' When the ballon is clicked.
    ''' </summary>
    Public Event BallonClicked As SmallBasicCallback
    ''' <summary>
    ''' Wen the ballon is closed.
    ''' </summary>
    Public Event BallonClosed As SmallBasicCallback
    ''' <summary>
    ''' When the notifier is clicked.
    ''' </summary>
    Public Event NotifierClicked As SmallBasicCallback
    ''' <summary>
    ''' When the notifier is double clicked.
    ''' </summary>
    Public Event NotifierDoubleClicked As SmallBasicCallback

    Private Sub notifier_BalloonTipClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles notifier.BalloonTipClicked
        RaiseEvent BallonClicked()
    End Sub

    Private Sub notifier_BalloonTipClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles notifier.BalloonTipClosed
        RaiseEvent BallonClosed()
    End Sub

    Private Sub notifier_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles notifier.MouseClick
        RaiseEvent NotifierClicked()
    End Sub

    Private Sub notifier_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles notifier.MouseDoubleClick
        RaiseEvent NotifierDoubleClicked()
    End Sub
End Module
''' <summary>
''' Various common resolutions for the graphics window.
''' </summary>
''' <remarks></remarks>
<SmallBasicType()> Public Module SPResolution
    Public Function Change640x480() As Primitive
        GraphicsWindow.Width = 640
        GraphicsWindow.Height = 480
    End Function
    Public Function Change720x480() As Primitive
        GraphicsWindow.Width = 720
        GraphicsWindow.Height = 480
    End Function
    Public Function Change800x600() As Primitive
        GraphicsWindow.Width = 800
        GraphicsWindow.Height = 600
    End Function
    Public Function Change960x720() As Primitive
        GraphicsWindow.Width = 960
        GraphicsWindow.Height = 720
    End Function
    Public Function Change1024x768() As Primitive
        GraphicsWindow.Width = 1024
        GraphicsWindow.Height = 768
    End Function
    Public Function Change1152x864() As Primitive
        GraphicsWindow.Width = 1152
        GraphicsWindow.Height = 864
    End Function
    Public Function Change1280x720() As Primitive
        GraphicsWindow.Width = 1280
        GraphicsWindow.Height = 720
    End Function
    Public Function Change1440x900() As Primitive
        GraphicsWindow.Width = 1440
        GraphicsWindow.Height = 900
    End Function
    Public Function Change1600x1050() As Primitive
        GraphicsWindow.Width = 1600
        GraphicsWindow.Height = 1050
    End Function
    Public Function Change1920x1080() As Primitive
        GraphicsWindow.Width = 1920
        GraphicsWindow.Height = 1080
    End Function
    ''' <summary>
    ''' Faster way to change graphics window's resolution, and only in one line of your code !
    ''' </summary>
    ''' <param name="width">The width</param>
    ''' <param name="height">The height</param>
    ''' <returns>SUCCESS or FAILED if fails.</returns>
    Public Function SetSize(ByVal width As Primitive, ByVal height As Primitive) As Primitive
        Try
            GraphicsWindow.Width = width
            GraphicsWindow.Height = height
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
    ''' <summary>
    ''' Moves the graphics window to the center of the screen.
    ''' </summary>
    ''' <returns>SUCCESS or FAILED if fails.</returns>
    Public Function CenterGraphicsWindow() As Primitive
        Try
            GraphicsWindow.Left = (Desktop.Width / 2) - (GraphicsWindow.Width / 2)
            GraphicsWindow.Top = (Desktop.Height / 2) - (GraphicsWindow.Height / 2)
            Return "SUCCESS"
        Catch ex As Exception
            Return "FAILED"
        End Try
    End Function
End Module