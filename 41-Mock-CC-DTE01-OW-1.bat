title Capsule Corp Mock
set source=%cd%\Source\InvoizR
cd %source%\InvoizR.Client.CapsuleCorp
dotnet run --mock --invoice-type=01 --processing-type=ow --limit=1
pause
