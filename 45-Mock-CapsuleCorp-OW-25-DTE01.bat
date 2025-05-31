title Capsule Corp Mock
set source=%cd%\Source\InvoizR
cd %source%\InvoizR.Client.CapsuleCorp
dotnet run --mock --processing-type=ow --limit=25 --invoice-type=01
pause
