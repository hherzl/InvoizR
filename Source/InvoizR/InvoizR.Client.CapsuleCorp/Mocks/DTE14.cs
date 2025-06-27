using InvoizR.Client.CapsuleCorp.Data;
using InvoizR.Client.CapsuleCorp.Helpers;
using InvoizR.Clients.DataContracts.Dte14;
using InvoizR.SharedKernel;

namespace InvoizR.Client.CapsuleCorp.Mocks;

static class DTE14
{
    public static CreateDte14OWCommand MockOwDte14()
    {
        var data = new
        {
            Branches = Db.Branches.ToList(),
            Products = Db.Products.ToList(),
            Persons = Db.Cards.Where(item => item.CardTypeId == (short)CardType.Person).ToList()
        };

        var invoiceLines = new List<InvoiceLine>
        {
            new("CC99", "Otros Servicios", Random.Shared.Next(100, 5000), 1)
        };

        var total = Math.Round(invoiceLines.Sum(item => item.Total), 2);

        var req = new CreateDte14OWCommand
        {
            PosId = (short)Random.Shared.Next(1, data.Branches.Count),
            InvoiceNumber = Convert.ToInt64($"{DateTime.Now:MMddhhmmss}"),
            InvoiceDate = DateTime.Now,
            InvoiceTotal = (decimal)total,
            Lines = invoiceLines.Count,
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

        req.Dte = FeFsev1Helper.Create(req, invoiceLines);

        return req;
    }

    public static CreateDte14RTCommand MockRtDte14()
    {
        var data = new
        {
            Branches = Db.Branches.ToList(),
            Products = Db.Products.ToList(),
            Persons = Db.Cards.Where(item => item.CardTypeId == (short)CardType.Person).ToList()
        };

        var invoiceLines = new List<InvoiceLine>
        {
            new("CC99", "Otros Servicios", Random.Shared.Next(100, 5000), 1)
        };

        var total = Math.Round(invoiceLines.Sum(item => item.Total), 2);

        var req = new CreateDte14RTCommand
        {
            PosId = (short)Random.Shared.Next(1, data.Branches.Count),
            InvoiceNumber = Convert.ToInt64($"{DateTime.Now:MMddhhmmss}"),
            InvoiceDate = DateTime.Now,
            InvoiceTotal = (decimal)total,
            Lines = invoiceLines.Count,
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

        req.Dte = FeFsev1Helper.Create(req, invoiceLines);

        return req;
    }
}
