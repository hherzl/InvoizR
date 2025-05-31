title Capsule Corp Mock
set source=%cd%\Source\InvoizR
cd %source%\InvoizR.Client.CapsuleCorp
dotnet run --mock --processing-type=rt --limit=1 --invoice-type=01
pause
