using InvoizR.Client.CapsuleCorp;
using InvoizR.Client.CapsuleCorp.Data;
using InvoizR.Client.CapsuleCorp.Mocks;
using InvoizR.Clients;
using InvoizR.Clients.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.SharedKernel.Mh.FeCcf;
using InvoizR.SharedKernel.Mh.FeFc;
using Microsoft.Extensions.Options;

const string BillingEndpoint = "https://localhost:13880";

var clientArgs = new ClientArgs(args);

if (clientArgs.ShowCatalog)
{
    Console.WriteLine("Products:");

    foreach (var item in Db.Products)
    {
        Console.WriteLine($" '{item.Description}': {item.Price:C2}");
    }

    Console.WriteLine();

    Console.WriteLine("Branches:");

    foreach (var item in Db.Branches)
    {
        Console.WriteLine($" '{item.Name}': {item.Address}");
    }

    Console.WriteLine();

    Console.WriteLine("Cards:");

    foreach (var item in Db.Cards)
    {
        if (item.CardTypeId == (short)CardType.Person)
        {
            Console.WriteLine($" '{item.Name}', '{item.DocumentTypeId}': '{item.DocumentNumber}', '{item.Email}'");
        }
        else
        {
            if (item.HasWt)
                Console.WriteLine($" '{item.Name}', '{item.DocumentTypeId}': '{item.DocumentNumber}' ({item.WtId}), '{item.Email}': {item.Address}");
            else
                Console.WriteLine($" '{item.Name}', '{item.DocumentTypeId}': '{item.DocumentNumber}', '{item.Email}': {item.Address}");
        }
    }

    Console.WriteLine();

    return;
}

InvoizRClient client = new(Options.Create(new InvoizRClientSettings
{
    Endpoint = BillingEndpoint
}));

if (clientArgs.Seed)
{
    Console.WriteLine($"Seeding data...");
    Console.WriteLine();

    Console.WriteLine($" Creating company...");

    var company = CompanyMocks.GetCapsuleCorp();

    var createCompanyResponse = await client.CreateCompanyAsync(company);

    Console.WriteLine($"  Generated Company ID: '{createCompanyResponse.Id}'");
    Console.WriteLine();

    Console.WriteLine($" Creating branches...");

    foreach (var item in Db.Branches)
    {
        var branch = new CreateBranchCommand
        {
            CompanyId = createCompanyResponse.Id,
            Name = item.Name,
            EstablishmentPrefix = item.EstablishmentPrefix,
            TaxAuthId = item.TaxAuthId,
            Address = item.Address,
            Phone = item.Phone,
            Email = item.Email
        };

        var createBranchResponse = await client.CreateBranchAsync(branch);

        Console.WriteLine($"   Generated Branch ID: '{createBranchResponse.Id}'");
        Console.WriteLine();

        Console.WriteLine($"    Adding notifications...");

        var addingNotificationToBranchResponse = await client.AddNotificationToBranchAsync(new(createBranchResponse.Id, 1, branch.Email, true));

        Console.WriteLine($"    Generated Branch ID: '{addingNotificationToBranchResponse.Id}'");
        Console.WriteLine();

        Console.WriteLine($"    Creating POS...");

        var pos = new CreatePosCommand
        {
            BranchId = createBranchResponse.Id,
            Name = "POS 1",
            Code = "POS1",
            TaxAuthPos = "01",
            Description = "POS 1"
        };

        var createPosResponse = await client.CreatePosAsync(pos);

        Console.WriteLine($"    Generated Branch ID: '{createPosResponse.Id}'");
        Console.WriteLine();
    }

    Console.WriteLine($" Creating responsibles...");

    foreach (var item in Db.Responsibles)
    {
        var responsible = new CreateResponsibleCommand
        {
            CompanyId = createCompanyResponse.Id,
            Name = item.Name,
            Phone = item.Phone,
            Email = item.Email,
            IdType = item.IdType,
            IdNumber = item.IdNumber,
            AuthorizeCancellation = item.AuthorizeCancellation,
            AuthorizeContingency = item.AuthorizeContingency
        };

        var createResponsibleResponse = await client.CreateResponsibleAsync(responsible);

        Console.WriteLine($"    Generated Responsible ID: '{createResponsibleResponse.Id}'");
        Console.WriteLine();
    }
}

if (clientArgs.Mock)
{
    Console.WriteLine($"Mocking '{clientArgs.Limit}' invoices for invoice type: '{clientArgs.InvoiceType}' in '{clientArgs.ProcessingType}'...");
    Console.WriteLine();

    for (var i = 0; i < clientArgs.Limit; i++)
    {
        try
        {
            if (clientArgs.IsRt)
            {
                if (clientArgs.InvoiceType == FeFcv1.SchemaType)
                    await MockRtDte01(client);
                else if (clientArgs.InvoiceType == FeCcfv3.SchemaType)
                    await MockRtDte03(client);
            }
            else
            {
                if (clientArgs.InvoiceType == FeFcv1.SchemaType)
                    await MockOwDte01(client);
                else if (clientArgs.InvoiceType == FeCcfv3.SchemaType)
                    await MockOwDte03(client);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        await Task.Delay(clientArgs.MockDelay);
    }
}

static async Task MockRtDte01(IInvoizRClient client)
{
    var req = DTE01.MockRtDte01();

    Console.WriteLine($" Syncing invoice '{req.InvoiceNumber}', {req.InvoiceTotal:C2} in RT processing...");

    var response = await client.CreateDte01InvoiceRTAsync(req);

    Console.WriteLine($"  Generated ID: '{response.Id}'");
    Console.WriteLine();
}

static async Task MockRtDte03(IInvoizRClient client)
{
    var req = DTE03.MockRtDte03();

    Console.WriteLine($" Syncing invoice '{req.InvoiceNumber}', {req.InvoiceTotal:C2} in RT processing...");

    var response = await client.CreateDte03InvoiceRTAsync(req);

    Console.WriteLine($"  Generated ID: '{response.Id}'");
    Console.WriteLine();
}

static async Task MockOwDte01(IInvoizRClient client)
{
    var req = DTE01.MockOwDte01();

    Console.WriteLine($" Syncing invoice '{req.InvoiceNumber}', {req.InvoiceTotal:C2} in OW processing...");

    var response = await client.CreateDte01InvoiceOWAsync(req);

    Console.WriteLine($"  Generated ID: '{response.Id}'");
    Console.WriteLine();
}

static async Task MockOwDte03(IInvoizRClient client)
{
    var req = DTE03.MockOwDte03();

    Console.WriteLine($" Syncing invoice '{req.InvoiceNumber}', {req.InvoiceTotal:C2} in OW processing...");

    var response = await client.CreateDte03InvoiceOWAsync(req);

    Console.WriteLine($"  Generated ID: '{response.Id}'");
    Console.WriteLine();
}
