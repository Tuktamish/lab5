public class Client
{
    public int ClientId { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string Address { get; set; }

    public Client(int clientId, string lastName, string firstName, string middleName, string address)
    {
        ClientId = clientId;
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        Address = address;
    }

    public override string ToString()
    {
        return $"{ClientId}: {LastName} {FirstName} {MiddleName}, {Address}";
    }
}

