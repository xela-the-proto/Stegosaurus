using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

namespace Stegosaurus.shard.Data;

public class IdGenerator
{
    public static async Task<string> GenId()
    {
        /*
         * Id generator didn't need a rewrite so i kept it as is
         */
        string ID ="";
        var fieldOptions = new FieldOptionsTextWords();
        fieldOptions.Max = 1;
        var randomWords = RandomizerFactory.GetRandomizer(fieldOptions);
        var randomHex = RandomizerFactory.GetRandomizer(new FieldOptionsIBAN());
        string word1 = randomWords.Generate();
        string word2 = randomWords.Generate();
        string iban = randomHex.Generate();
        ID = word1 + "-" + word2 + "-" + iban;
        string shuffled;
        do
        {
            shuffled = new string(
                ID
                    .OrderBy(character => Guid.NewGuid())
                    .ToArray()
            );
        } while (shuffled == ID);
        var shuffled_trimmed = shuffled.Remove(35);
       
        return shuffled;
    }
}