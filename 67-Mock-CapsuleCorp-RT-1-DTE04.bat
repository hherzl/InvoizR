title Capsule Corp Mock DTE-04
set source=%cd%\Source\InvoizR
cd %source%\InvoizR.Client.CapsuleCorp
dotnet run --mock --processing-type=rt --limit=1 --invoice-type=04
pause
