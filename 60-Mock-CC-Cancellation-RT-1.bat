title Capsule Corp Mock Cancellation
set source=%cd%\Source\InvoizR
cd %source%\InvoizR.Client.CapsuleCorp
dotnet run --mock --invoice-type=cancellation --processing-type=rt --limit=1
pause
