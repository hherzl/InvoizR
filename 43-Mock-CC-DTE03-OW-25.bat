title Capsule Corp Mock DTE-03
set source=%cd%\Source\InvoizR
cd %source%\InvoizR.Client.CapsuleCorp
dotnet run --mock --invoice-type=03 --processing-type=ow --limit=25
pause
