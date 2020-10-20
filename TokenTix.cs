using System;

public partial class TokenTix
{
    public string username { get; set; }
    public string hashcode { get; set; }
    public Nullable<System.DateTime> datetime_exp { get; set; }

    public TokenTix(string _username)
	{
        //1. create hash code
        this.hashcode = ComputeSha256Hash();
        //2. set token properties
        this.username = _username;
        this.datetime_exp = DateTime.UtcNow.AddMinutes(30);
        //3. save token in database
    }
    private string ComputeSha256Hash()
    {
        Random rm = new Random();
        string rawData = (rm.NextDouble() * 100000.0).ToString();
        // Create a SHA256   
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
