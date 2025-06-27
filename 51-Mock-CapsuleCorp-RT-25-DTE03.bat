title Capsule Corp Mock DTE-03
set source=%cd%\Source\InvoizR
cd %source%\InvoizR.Client.CapsuleCorp
dotnet run --mock --processing-type=rt --limit=25 --invoice-type=03
pause
