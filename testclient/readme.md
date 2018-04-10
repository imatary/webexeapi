## WebEXE Test Client

This is a test client for the WebEXE API. As of now, it's a bit jenk and it will be until I learn a better language like Python, Java, or VisualBasic.

### Using the Test Client

Clone this repo, and then run `/testclient/smallbasicsource/webexe_testclient.exe`. It will ask you for a URL, you MUST format the URL like this: `http://domain.com`. If the apps are in a subdirectory on the server, you MUST format the URL like this: `http://domain.com/subdirectory`. Then, it will download the list and parse it, and then ask you to select an app. To select an app, enter it's AppID and press ENTER or RETURN. The client will download, extract, and execute the app. Once the app has COMPLETELY shut down, press any key to go back to the launcher. When you press any key, the client will clean up after itself, and refrest the list.

### Info on the Test Client's Code

The test client was written in Microsoft Smallbasic, a little known scripting language. It is dated as heck, so that's why the client currently does not work with HTTPS. So, feel free to rewrite it in a language that works with HTTPS. Also, I will rewrite it in a better language soon, anyway.