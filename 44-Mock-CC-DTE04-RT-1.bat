title Capsule Corp Mock DTE-04
set source=%cd%\Source\InvoizR
cd %source%\InvoizR.Client.CapsuleCorp
dotnet run --mock --invoice-type=04 --processing-type=rt --limit=1
pause
