using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

public class TokenTix
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
        //SaveToken();

    }
    public bool SaveToken()
    {
        bool result = false;
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                Mob_Token mt = new Mob_Token();
                mt.datetime_exp = DateTime.UtcNow.AddDays(1);
                mt.hashcode = this.hashcode;
                mt.username = this.username;

                //if (!IsTokenAlive(mt))
                {
                    context.Mob_Token.Add(mt);
                    context.SaveChanges();
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            string xx = ex.Message;
        }

        return result;
    }

    private bool IsTokenAlive(Mob_Token t)
    {
        bool result = false;
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                var tcount = context.Mob_Token.Where(x => x.hashcode == t.hashcode
                                            && x.username == t.username).SingleOrDefault();
                if (tcount != null)
                {
                    DateTime dt_now = DateTime.UtcNow;
                    DateTime dt_exp = tcount.datetime_exp.Value;

                    if (DateTime.Compare(dt_now, dt_exp) > 0)
                    {
                        // token was died
                        result = false;
                    }
                    else
                    {
                        // token is alive
                        result = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string xx = ex.Message;
            result = false;
        }
        return result;
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