title Capsule Corp Mock DTE-04
set source=%cd%\Source\InvoizR
cd %source%\InvoizR.Client.CapsuleCorp
dotnet run --mock --invoice-type=04 --processing-type=ow --limit=1
pause
