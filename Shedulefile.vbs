Call LogEntry()

Sub LogEntry()
        On Error Resume Next

        'variables
        Dim objRequest
        Dim URL

        Set objRequest = CreateObject("Microsoft.XMLHTTP")

        'URL link 
        URL = "http://localhost:5555/SchedulerToNotify"

        'Open the HTTP request and pass the URL 
        objRequest.open "POST", URL , false

        'Send the HTML Request
        objRequest.Send

        'Set the object to nothing
        Set objRequest = Nothing

End Sub