using InvoizR.Client.CapsuleCorp.Data;
using InvoizR.Client.CapsuleCorp.Helpers;
using InvoizR.Clients.DataContracts.Dte14;
using InvoizR.SharedKernel;

namespace InvoizR.Client.CapsuleCorp.Mocks;

static class Dte14
{
    public static CreateDte14OWCommand MockOwDte()
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

        var request = new CreateDte14OWCommand
        {
            PosId = (short)Random.Shared.Next(1, data.Branches.Count),
            InvoiceNumber = Convert.ToInt64($"{DateTime.Now:MMddhhmmss}"),
            InvoiceDate = DateTime.Now,
            InvoiceTotal = (decimal)total,
            Lines = invoiceLines.Count,
        };

        var persons = data.Persons.ToList();
        var randomCard = persons[Random.Shared.Next(0, persons.Count - 1)];

        request.Customer.Id = randomCard.Id;
        request.Customer.DocumentTypeId = randomCard.DocumentTypeId;
        request.Customer.DocumentNumber = randomCard.DocumentNumber;
        request.Customer.WtId = randomCard.WtId;
        request.Customer.Name = randomCard.Name;
        request.Customer.CountryId = randomCard.CountryId;
        request.Customer.CountryLevelId = randomCard.CountryLevelId;
        request.Customer.Address = randomCard.Address;
        request.Customer.Phone = randomCard.Phone;
        request.Customer.Email = randomCard.Email;

        request.Dte = FeFsev1Helper.Create(request, invoiceLines);

        return request;
    }

    public static CreateDte14RTCommand MockRtDte()
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

        var request = new CreateDte14RTCommand
        {
            PosId = (short)Random.Shared.Next(1, data.Branches.Count),
            InvoiceNumber = Convert.ToInt64($"{DateTime.Now:MMddhhmmss}"),
            InvoiceDate = DateTime.Now,
            InvoiceTotal = (decimal)total,
            Lines = invoiceLines.Count,
        };

        var persons = data.Persons.ToList();
        var randomPerson = persons[Random.Shared.Next(0, persons.Count - 1)];

        request.Customer.Id = randomPerson.Id;
        request.Customer.DocumentTypeId = randomPerson.DocumentTypeId;
        request.Customer.DocumentNumber = randomPerson.DocumentNumber;
        request.Customer.WtId = randomPerson.WtId;
        request.Customer.Name = randomPerson.Name;
        request.Customer.CountryId = randomPerson.CountryId;
        request.Customer.CountryLevelId = randomPerson.CountryLevelId;
        request.Customer.Address = randomPerson.Address;
        request.Customer.Phone = randomPerson.Phone;
        request.Customer.Email = randomPerson.Email;

        request.Dte = FeFsev1Helper.Create(request, invoiceLines);

        return request;
    }
}
