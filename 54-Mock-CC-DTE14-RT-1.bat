title Capsule Corp Mock DTE-14
set source=%cd%\Source\InvoizR
cd %source%\InvoizR.Client.CapsuleCorp
dotnet run --mock --invoice-type=14 --processing-type=rt --limit=1
pause
