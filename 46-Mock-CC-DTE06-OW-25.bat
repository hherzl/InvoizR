title Capsule Corp Mock DTE-06
set source=%cd%\Source\InvoizR
cd %source%\InvoizR.Client.CapsuleCorp
dotnet run --mock --invoice-type=06 --processing-type=ow --limit=25
pause
