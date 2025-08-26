title Capsule Corp Mock DTE-14
set source=%cd%\Source\InvoizR
cd %source%\InvoizR.Client.CapsuleCorp
dotnet run --mock --processing-type=rt --limit=1 --invoice-type=14
pause
