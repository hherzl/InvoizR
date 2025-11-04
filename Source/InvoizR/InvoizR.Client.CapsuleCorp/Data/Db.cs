using InvoizR.SharedKernel.Mh;

namespace InvoizR.Client.CapsuleCorp.Data;

public static class Db
{
    private readonly static List<Branch> _branches;
    private readonly static List<Responsible> _responsibles;
    private readonly static List<Product> _products;
    private readonly static List<Card> _cards;

    static Db()
    {
        _branches = new()
        {
            new Branch("CC. Central Branch", "S001", "1 Capsule Corp Drive, West City", "+1-800-555-0001", "central@capsulecorp.com"),
            new Branch("CC. East Branch", "S002", "12 Dragon Street, East City", "+1-800-555-0002", "east@capsulecorp.com"),
            new Branch("CC. North Branch", "S003", "99 Ice Lane, North City", "+1-800-555-0003", "north@capsulecorp.com"),
            new Branch("CC. South Branch", "S004", "77 Ocean Avenue, South City", "+1-800-555-0004", "south@capsulecorp.com"),
            new Branch("CC. Satan City Branch", "S005", "21 Hercule Boulevard, Satan City", "+1-800-555-0005", "satan@capsulecorp.com"),
            new Branch("CC. Ginger Town Branch", "S006", "5 Meadow Road, Ginger Town", "+1-800-555-0006", "gingertown@capsulecorp.com"),
            new Branch("CC. Orange Star Branch", "S007", "17 Gohan Way, Orange Star City", "+1-800-555-0007", "orangestar@capsulecorp.com"),
            new Branch("CC. Penguin Village Branch", "S008", "3 Arale Lane, Penguin Village", "+1-800-555-0008", "penguin@capsulecorp.com")
        };

        _responsibles = new()
        {
            new("Toriyama Akira", "+1-800-999-0001", "toriyama-akira@capsulecorp.com", MhCatalog.Cat022.Dui, "01234567-8")
        };

        _products = new()
        {
            new("CC001", "Capsule for Vehicle Storage", 500.00m, new DateTime(2024, 1, 1), "Storage"),
            new("CC002", "Capsule for House Deployment", 1500.00m, new DateTime(2024, 1, 15), "Residential"),
            new("CC003", "Time Machine Capsule (Replica)", 10000.00m, new DateTime(2024, 2, 1), "Advanced Tech"),
            new("CC004", "Capsule for Emergency Supplies", 200.00m, new DateTime(2024, 2, 10), "Emergency Gear"),
            new("CC005", "Capsule for Weaponry Storage", 750.00m, new DateTime(2024, 2, 20), "Defense Equipment"),
            new("CC006", "Capsule for Laboratory Setup", 3000.00m, new DateTime(2024, 3, 1), "Research"),
            new("CC007", "Capsule for Spaceship Storage", 250000.00m, new DateTime(2024, 3, 15), "Advanced Tech"),
            new("CC008", "Capsule for Farming Equipment", 1200.00m,new DateTime(2024, 4, 1), "Agriculture"),
            new("CC009", "Capsule for Mobile Office", 5000.00m, new DateTime(2024, 4, 20), "Business"),
            new("CC010", "Capsule for Entertainment Gear", 1000.00m, new DateTime(2024, 5, 1), "Lifestyle"),
            new("CC011", "Gravity Training Gear (Vegeta Style)", 15000.00m, new DateTime(2024, 5, 15),"Training"),
            new("CC012", "Advanced Combat Training Simulator", 25000.00m, new DateTime(2024, 6, 1), "Training"),
            new("CC013", "Capsule for Sports Car Storage", 30000.00m, new DateTime(2024, 6, 15), "Vehicles"),
            new("CC014", "Capsule for Motorcycle Storage", 15000.00m, new DateTime(2024, 7, 1), "Vehicles"),
            new("CC015", "Capsule for Jet Storage", 500000.00m, new DateTime(2024, 7, 15), "Vehicles"),
            new("CC016", "Capsule Corp. Tee", 40.00m, new DateTime(2024, 8, 1), "Souvenirs"),
            new("CC017", "Capsule Corp. Keychain", 20.00m, new DateTime(2024, 8, 1), "Souvenirs"),
            new("CC018", "Capsule Corp. Steel Flask", 40.00m, new DateTime(2024, 8, 1), "Souvenirs"),
        };

        _cards = new()
        {
            // Persons
            Card.CreatePerson("C001", "13", "00112233-1", "Son Goku", "JP", 1, "439 East District, Mount Paozu", "+81-555-1234", "goku@capsulecorp.com"),
            Card.CreatePerson("C002", "13", "00112233-2", "Bulma Briefs", "JP", 1, "1 Capsule Corp Drive, West City", "+81-555-5678", "bulma@capsulecorp.com"),
            Card.CreatePerson("C003", "13", "00112233-3", "Chi-Chi", "JP", 1, "Mount Paozu, East District", "+81-555-9999", "chichi@capsulecorp.com"),
            Card.CreatePerson("C005", "13", "00112233-4", "Son Gohan", "JP", 1, "439 East District, Mount Paozu", "+81-555-5555", "gohan@capsulecorp.com"),
            Card.CreatePerson("C006", "13", "00112233-5", "Android 18", "JP", 1, "18 Future Lane, West City", "+81-555-6666", "android18@capsulecorp.com"),
            Card.CreatePerson("C007", "13", "00112233-6", "Trunks Briefs", "JP", 1, "1 Capsule Corp Drive, West City", "+81-555-7777", "trunks@capsulecorp.com"),
            Card.CreatePerson("C008", "13", "00112233-7", "Krillin", "JP", 1, "10 Turtle Way, Kame Island", "+81-555-8888", "krillin@capsulecorp.com"),
            Card.CreatePerson("C009", "13", "00112233-8", "Videl Satan", "JP", 1, "21 Hercule Avenue, Satan City", "+81-555-9990", "videl@capsulecorp.com"),
            Card.CreatePerson("C010", "13", "00112233-9", "Pan", "JP", 1, "21 Hercule Avenue, Satan City", "+81-555-9991", "pan@capsulecorp.com"),

            // Walk-ins
            Card.CreateWalkIn("W001", "Walk-in cust.", "SV", 1, "N/A", "N/A", "walk-incust@capsulecorp.com"),

            // Suppliers
            Card.CreateCompany("S001", "36", "0614-010122-000-1", "Kame House Supplies", "JP", 1, "Kame House, South Sea", "+81-555-3333", "kame@supplies.com", taxpayerId: "101-0", wtId: "WT1"),
            Card.CreateCompany("S002", "36", "0614-010222-000-2", "Saiyan Training Equipment Ltd.", "JP", 1, "123 Vegeta Boulevard, North City", "+81-555-8888", "training@vegeta.com"),
            Card.CreateCompany("S003", "36", "0614-010322-000-3", "Advanced Tech Innovations", "JP", 1, "17 Future Lane, West City", "+81-555-7777", "tech@capsulecorp.com", taxpayerId: "321-0"),
            Card.CreateCompany("S004", "36", "0614-010422-000-4", "Penguin Village Exports", "JP", 1, "5 Arale Lane, Penguin Village", "+81-555-4444", "exports@penguinvillage.com", taxpayerId: "543-0"),
            Card.CreateCompany("S005", "36", "0614-010522-000-5", "Orange Star Logistics", "JP", 1, "21 Gohan Avenue, Orange Star City", "+81-555-2222", "logistics@orangestar.com", taxpayerId: "555-0", wtId: "WT1"),
            Card.CreateCompany("S006", "36", "0614-010622-000-6", "Red Ribbon Tech", "JP", 1, "55 Ribbon Street, West City", "+81-555-1111", "contact@redribbon.com"),
            Card.CreateCompany("S007", "36", "0614-010722-000-7", "Ginyu Force Services", "JP", 1, "99 Space Route, North City", "+81-555-1010", "ginyu@force.com"),
            Card.CreateCompany("S008", "36", "0614-010822-000-8", "Yajirobe Food Supplies", "JP", 1, "7 Korin Way, Sacred Land", "+81-555-2020", "food@yajirobe.com", taxpayerId: "101-0"),
            Card.CreateCompany("S009", "36", "0614-010922-000-9", "Hercule's Gym Equipment", "JP", 1, "1 Martial Arts Plaza, Satan City", "+81-555-3030", "gym@hercule.com"),
            Card.CreateCompany("S010", "36", "0614-011022-100-1", "Future Capsule Logistics", "JP", 1, "7 Trunks Boulevard, West City", "+81-555-4040", "logistics@futurecapsule.com", taxpayerId: "999-8"),
            Card.CreateCompany("S011", "36", "0614-011122-100-2", "King Kai Communication Ltd.", "JP", 1, "Kai Lane, Otherworld", "+81-555-5050", "communication@kingkai.com", taxpayerId: "1111-0", wtId: "WT1")
        };
    }

    public static IEnumerable<Branch> Branches
        => _branches;

    public static IEnumerable<Responsible> Responsibles
        => _responsibles;

    public static IEnumerable<Product> Products
        => _products;

    public static IEnumerable<Card> Cards
        => _cards;
}
