namespace Bacen.Domain.Services;

public static class CardNumberGenerator
{
    public static string[] MASTERCARD_PREFIX_LIST = new[] { "51", "52", "53", "54", "55" };
    
    public static IEnumerable<string> GenerateMasterCardNumbers(int howMany)
    {
        return GetCreditCardNumbers(MASTERCARD_PREFIX_LIST, 16, howMany);
    }

    public static string GenerateMasterCardNumber()
    {
        return GetCreditCardNumbers(MASTERCARD_PREFIX_LIST, 16, 1).First();
    }

    public static IEnumerable<string> GetCreditCardNumbers(string[] prefixList, int length, int howMany)
    {
        var result = new Stack<string>();

        for (int i = 0; i < howMany; i++)
        {
            int randomPrefix = new Random().Next(0, prefixList.Length - 1);
    
            if(randomPrefix>1)
            {
                randomPrefix--;
            }

            string ccnumber = prefixList[randomPrefix];

            result.Push(CreateCardNumber(ccnumber, length));
        }

        return result;
    }

    private static string CreateCardNumber(string prefix, int length)
    {
        string ccnumber = prefix;

        while (ccnumber.Length < (length - 1))
        {
            double rnd = (new Random().NextDouble()*1.0f - 0f);
            ccnumber += Math.Floor(rnd*10);
            //sleep so we get a different seed
            Thread.Sleep(20);
        }

        // reverse number and convert to int
        var reversedCCnumberstring = ccnumber.ToCharArray().Reverse();
        var reversedCCnumberList = reversedCCnumberstring.Select(c => Convert.ToInt32(c.ToString()));

        // calculate sum
        int sum = 0;
        int pos = 0;
        int[] reversedCCnumber = reversedCCnumberList.ToArray();

        while (pos < length - 1)
        {
            int odd = reversedCCnumber[pos]*2;

            if (odd > 9)
                odd -= 9;

            sum += odd;

            if (pos != (length - 2))
                sum += reversedCCnumber[pos + 1];

            pos += 2;
        }

        // calculate check digit
        int checkdigit =
            Convert.ToInt32((Math.Floor((decimal) sum/10) + 1)*10 - sum)%10;

        ccnumber += checkdigit;

        return ccnumber;
    }

    public static bool IsValidCreditCardNumber(string creditCardNumber)
    {
        try
        {
            var reversedNumber = creditCardNumber.ToCharArray().Reverse();
                
            int mod10Count = 0;
            for (int i = 0; i < reversedNumber.Count(); i++)
            {
                int augend = Convert.ToInt32(reversedNumber.ElementAt(i).ToString());

                if (((i + 1)%2) == 0)
                {
                    string productstring = (augend*2).ToString();
                    augend = 0;
                    for (int j = 0; j < productstring.Length; j++)
                    {
                        augend += Convert.ToInt32(productstring.ElementAt(j).ToString());
                    }
                }
                mod10Count += augend;
            }

            if ((mod10Count%10) == 0)
            {
                return true;
            }
        }
        catch
        {
            return false;
        }
        return false;
    }
}