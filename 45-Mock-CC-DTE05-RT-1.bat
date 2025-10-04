title Capsule Corp Mock DTE-05
set source=%cd%\Source\InvoizR
cd %source%\InvoizR.Client.CapsuleCorp
dotnet run --mock --invoice-type=05 --processing-type=rt --limit=1
pause
