using InvoizR.Client.CapsuleCorp.Data;
using InvoizR.Client.CapsuleCorp.Helpers;
using InvoizR.Clients.DataContracts;
using InvoizR.SharedKernel;

namespace InvoizR.Client.CapsuleCorp.Mocks;

static class DTE03
{
    public static CreateDte03InvoiceOWCommand MockOwDte03()
    {
        var data = new
        {
            Branches = Db.Branches.ToList(),
            Products = Db.Products.ToList(),
            Persons = Db.Cards.Where(item => item.CardTypeId == (short)CardType.Person).ToList()
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

        var req = new CreateDte03InvoiceOWCommand
        {
            PosId = (short)Random.Shared.Next(1, data.Branches.Count),
            InvoiceNumber = Convert.ToInt64($"{DateTime.Now:MMddhhmmss}"),
            InvoiceDate = DateTime.Now,
            InvoiceTotal = (decimal)total,
            Lines = lines,
        };

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

        req.Dte = FeCcfv3Helper.Create(req, invoiceLines);

        return req;
    }

    public static CreateDte03InvoiceRTCommand MockRtDte03()
    {
        var data = new
        {
            Branches = Db.Branches.ToList(),
            Products = Db.Products.ToList(),
            Persons = Db.Cards.Where(item => item.CardTypeId == (short)CardType.Person).ToList()
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

        var req = new CreateDte03InvoiceRTCommand
        {
            PosId = (short)Random.Shared.Next(1, data.Branches.Count),
            InvoiceNumber = Convert.ToInt64($"{DateTime.Now:MMddhhmmss}"),
            InvoiceDate = DateTime.Now,
            InvoiceTotal = (decimal)total,
            Lines = lines,
        };

        var persons = data.Persons.ToList();
        var randomPerson = persons[Random.Shared.Next(0, persons.Count - 1)];

        req.Customer.Id = randomPerson.Id;
        req.Customer.DocumentTypeId = randomPerson.DocumentTypeId;
        req.Customer.DocumentNumber = randomPerson.DocumentNumber;
        req.Customer.WtId = randomPerson.WtId;
        req.Customer.Name = randomPerson.Name;
        req.Customer.CountryId = randomPerson.CountryId;
        req.Customer.CountryLevelId = randomPerson.CountryLevelId;
        req.Customer.Address = randomPerson.Address;
        req.Customer.Phone = randomPerson.Phone;
        req.Customer.Email = randomPerson.Email;

        req.Dte = FeCcfv3Helper.Create(req, invoiceLines);

        return req;
    }
}
