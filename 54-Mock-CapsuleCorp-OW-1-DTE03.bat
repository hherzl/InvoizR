title Capsule Corp Mock
set source=%cd%\Source\InvoizR
cd %source%\InvoizR.Client.CapsuleCorp
dotnet run --mock --processing-type=ow --limit=1 --invoice-type=03
pause
