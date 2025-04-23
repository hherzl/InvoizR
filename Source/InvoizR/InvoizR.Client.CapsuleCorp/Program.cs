using InvoizR.Client.CapsuleCorp;
using InvoizR.Client.CapsuleCorp.Data;
using InvoizR.Client.CapsuleCorp.Helpers;
using InvoizR.Client.CapsuleCorp.Mocks;
using InvoizR.Clients;
using InvoizR.Clients.DataContracts;
using InvoizR.SharedKernel;
using Microsoft.Extensions.Options;

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
    Endpoint = "https://localhost:13880"
}));

if (clientArgs.Seed)
{
    Console.WriteLine($"Seeding data...");

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
}

if (clientArgs.Mock)
{
    Console.WriteLine($"Mocking '{clientArgs.Limit}' invoices...");

    for (var i = 0; i < clientArgs.Limit; i++)
    {
        try
        {
            if (clientArgs.IsRt)
            {
                var req = InvoiceMocker.MockRtDte01();

                Console.WriteLine($" Syncing invoice '{req.InvoiceNumber}', {req.InvoiceTotal:C2} in RT processing...");

                var response = await client.CreateDte01InvoiceRTAsync(req);

                Console.WriteLine($"  Generated ID: '{response.Id}'");
                Console.WriteLine();
            }
            else
            {
                var req = InvoiceMocker.MockOwDte01();

                Console.WriteLine($" Syncing invoice '{req.InvoiceNumber}', {req.InvoiceTotal:C2} in OW processing...");

                var response = await client.CreateDte01InvoiceOWAsync(req);

                Console.WriteLine($"  Generated ID: '{response.Id}'");
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        await Task.Delay(3000);
    }
}

class InvoiceMocker
{
    public static CreateDte01InvoiceOWCommand MockOwDte01()
    {
        var data = new
        {
            Branches = Db.Branches.ToList(),
            Products = Db.Products.ToList(),
            Persons = Db.Cards.Where(item => item.CardTypeId == (short)CardType.Person).ToList(),
            WalkIns = Db.Cards.Where(item => item.CardTypeId == (short)CardType.WalkIn).ToList()
        };

        var lines = Random.Shared.Next(1, data.Products.Count);
        var invoiceLines = new List<InvoiceLine>();

        for (var i = 0; i < lines; i++)
        {
            var randomIndex = Random.Shared.Next(0, lines - 1);
            var product = data.Products[randomIndex];
            if (invoiceLines.Any(item => item.Code == product.Code))
                continue;

            invoiceLines.Add(new(product.Code, product.Description, (double)product.Price, 1));
        }

        var total = Math.Round(invoiceLines.Sum(item => item.Total), 2);

        var req = new CreateDte01InvoiceOWCommand
        {
            PosId = (short)Random.Shared.Next(1, data.Branches.Count),
            InvoiceTypeId = 1,
            InvoiceNumber = Convert.ToInt64($"{DateTime.Now:MMddhhmmss}"),
            InvoiceDate = DateTime.Now,
            InvoiceTotal = (decimal)total,
            Lines = lines,
        };

        if (req.InvoiceTotal > CreateDte01InvoiceCommand.MaxAmountForAnonymousCustomers)
        {
            var persons = data.Persons.ToList();
            var randomCard = persons[Random.Shared.Next(0, persons.Count - 1)];

            req.Customer.Id = randomCard.Id;
            req.Customer.DocumentTypeId = randomCard.DocumentTypeId;
            req.Customer.DocumentNumber = randomCard.DocumentNumber;
            req.Customer.WtId = randomCard.WtId;
            req.Customer.Name = randomCard.Name;
            req.Customer.CountryId = randomCard.CountryId;
            req.Customer.CountryLevelId = randomCard.CountryLevelId;
            req.Customer.Address = randomCard.Address;
            req.Customer.Phone = randomCard.Phone;
            req.Customer.Email = randomCard.Email;
        }
        else
        {
            var card = data.WalkIns.First();

            req.Customer.Id = card.Id;
            req.Customer.Name = card.Name;
            req.Customer.CountryId = card.CountryId;
            req.Customer.CountryLevelId = card.CountryLevelId;
            req.Customer.Address = card.Address;
            req.Customer.Phone = card.Phone;
            req.Customer.Email = card.Email;
        }

        req.Dte = FeFcv1Helper.Create(req, invoiceLines);

        return req;
    }

    public static CreateDte01InvoiceRTCommand MockRtDte01()
    {
        var data = new
        {
            Branches = Db.Branches.ToList(),
            Products = Db.Products.ToList(),
            Persons = Db.Cards.Where(item => item.CardTypeId == (short)CardType.Person).ToList(),
            WalkIns = Db.Cards.Where(item => item.CardTypeId == (short)CardType.WalkIn).ToList()
        };

        var lines = Random.Shared.Next(1, data.Products.Count);
        var invoiceLines = new List<InvoiceLine>();

        for (var i = 0; i < lines; i++)
        {
            var randomIndex = Random.Shared.Next(0, lines - 1);
            var product = data.Products[randomIndex];
            if (invoiceLines.Any(item => item.Code == product.Code))
                continue;

            invoiceLines.Add(new(product.Code, product.Description, (double)product.Price, 1));
        }

        var total = Math.Round(invoiceLines.Sum(item => item.Total), 2);

        var req = new CreateDte01InvoiceRTCommand
        {
            PosId = (short)Random.Shared.Next(1, data.Branches.Count),
            InvoiceTypeId = 1,
            InvoiceNumber = Convert.ToInt64($"{DateTime.Now:MMddhhmmss}"),
            InvoiceDate = DateTime.Now,
            InvoiceTotal = (decimal)total,
            Lines = lines,
        };

        if (req.InvoiceTotal > CreateDte01InvoiceCommand.MaxAmountForAnonymousCustomers)
        {
            var persons = data.Persons.ToList();
            var randomCard = persons[Random.Shared.Next(0, persons.Count - 1)];

            req.Customer.Id = randomCard.Id;
            req.Customer.DocumentTypeId = randomCard.DocumentTypeId;
            req.Customer.DocumentNumber = randomCard.DocumentNumber;
            req.Customer.WtId = randomCard.WtId;
            req.Customer.Name = randomCard.Name;
            req.Customer.CountryId = randomCard.CountryId;
            req.Customer.CountryLevelId = randomCard.CountryLevelId;
            req.Customer.Address = randomCard.Address;
            req.Customer.Phone = randomCard.Phone;
            req.Customer.Email = randomCard.Email;
        }
        else
        {
            var card = data.WalkIns.First();

            req.Customer.Id = card.Id;
            req.Customer.Name = card.Name;
            req.Customer.CountryId = card.CountryId;
            req.Customer.CountryLevelId = card.CountryLevelId;
            req.Customer.Address = card.Address;
            req.Customer.Phone = card.Phone;
            req.Customer.Email = card.Email;
        }

        req.Dte = FeFcv1Helper.Create(req, invoiceLines);

        return req;
    }
}
