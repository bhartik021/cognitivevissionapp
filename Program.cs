using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    private const string subscriptionKey = "6152437abd374548bdf59c5410b1fc2b";
    private const string endpoint = "https://cognitivevissionapp.cognitiveservices.azure.com/";

    static async Task Main(string[] args)
    {
        try
        {
            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey))
            {
                Endpoint = endpoint
            };

            // Example image URL
            string imageUrl = "https://upload.wikimedia.org/wikipedia/commons/3/3a/Cat03.jpg";

            var features = new List<VisualFeatureTypes?> { VisualFeatureTypes.Description };
            ImageAnalysis result = await client.AnalyzeImageAsync(imageUrl, features);

            Console.WriteLine($"Request ID: {result.RequestId}");
            Console.WriteLine($"Metadata: {result.Metadata}");
            foreach (var caption in result.Description.Captions)
            {
                Console.WriteLine($"Description: {caption.Text} with confidence {caption.Confidence}");
            }
        }
        catch (ComputerVisionErrorResponseException e)
        {
            Console.WriteLine($"Computer Vision API error: {e.Message}");
            if (e.Body != null && e.Body.Error != null)
            {
                Console.WriteLine($"Error Code: {e.Body.Error.Code}");
                Console.WriteLine($"Error Message: {e.Body.Error.Message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected error: {e.Message}");
        }
    }
}
