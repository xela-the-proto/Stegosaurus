using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

namespace Stegosaurus.Shard.Net;

public class GenerateID
{
    public static Task<string> Generate()
    {
        string ID ="";
        var fieldOptions = new FieldOptionsTextWords();
        fieldOptions.Max = 1;
        var randomWords = RandomizerFactory.GetRandomizer(fieldOptions);
        var randomHex = RandomizerFactory.GetRandomizer(new FieldOptionsIBAN());
        string word1 = randomWords.Generate();
        string word2 = randomWords.Generate();
        string iban = randomHex.Generate();
        ID = word1 + "-" + word2 + "-" + iban;
        return Task.FromResult(ID);
    }
}