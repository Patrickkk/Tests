Param([string]$password)
pscp  -pw $password -r "RestberryPiApi\publish\linux-arm" `
pi@192.168.1.101:/home/pi/webapi
